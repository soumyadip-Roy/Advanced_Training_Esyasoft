#include "..\lib\inc\meterOpticalPort.h"
#include <stdint.h>
#include <stdbool.h>
#include <stddef.h>

#define OPTICAL_BAUDRATE 9600
#define I2C_HANDSHAKE_ADDR 0x50
#define HANDSHAKE_Sign 0xA5
#define RX_BUFFER_LENGTH 128


static UART_HandleTypeDef huart1;
static I2C_HandleTypeDef hi2c1;
static GPIO_TypeDef *detect_port = NULL;
static uint16_t detect_pin = 0;
static bool initialized = false;

static uint8_t rx_buffer[RX_BUFFER_LENGTH];
static volatile size_t rx_head_ptr = 0;
static volatile size_t rx_tail_ptr = 0;
static volatile bool rx_overflow_flag = false;

static bool optical_wait_for_handshake(uin32_t timeout);
static bool optical_send_data(const uint8_t *data, size_t length);
static bool optical_recieve_data(uint8_t *buffer, size_t length, uint32_t time);


static void gpio_init(void){
    GPIO_InitTypeDef GPIO_InitStruct = {0};;

    __HAL_RCC_GPIOA_CLK_ENABLE();
    __HAL_RCC_GPIOB_CLK_ENABLE();

    detect_port = GPIOA;
    detect_pin = GPIO_PIN_1;

    GPIO_InitStruct.Pin = detect_pin;
    GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
    GPIO_InitStruct.Pull = GPIO_PULLUP;
    GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
    HAL_GPIO_Init(detect_port, &GPIO_InitStruct);
}

static void uart1_init(void){
    __HAL_RCC_UART1_CLK_ENABLE();

    huart1.Instance = USART1;
    huart1.Init.BaudRate = OPTICAL_BAUDRATE;
    huart1.Init.WordLength = UART_WORDLENGTH_8B;
    huart1.Init.StopBits = UART_STOPBITS_1;
    huart1.Init.Parity = UART_PARITY_NONE;
    huart1.Init.Mode = UART_MODE_TX_RX;
    huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
    huart1.Init.OverSampling = UART_OVERSAMPLING_16;

    GPIO_InitTypeDef GPIO_InitStruct = {0};
    GPIO_InitStruct.Pin = GPIO_PIN_9 | GPIO_PIN_10;
    GPIO_InitStruct.Mode = GPIO_MODE_AF_PP;
    GPIO_InitStruct.Pull = GPIO_NOPULL;
    GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH;
    GPIO_InitStruct.Alternate = GPIO_AF7_USART1;
    HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

    HAL_UART_Init(&huart1);
}

static void i2c1_init(void){

    __HAL_RCC_I2C1_CLK_ENABLE();

    hi2c1.Instance = I2C1;
    hi2c1.Init.Timing = ;
    hi2c1.Init.OwnAdress1 = 0;
    hi2c1.Init.AdressingMode = I2C_ADDRESSINGMODE_7BIT;
    hi2c1.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
    hi2c1.Init.OwnAddress2 = 0;
    hi2c1.Init.OwnAddress2Masks = I2C_OA2_NOMASK;
    hi2c1.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
    hi2c1.Init.NoStretchMode = I2C_NOSTRETCH_DISABLE;

    GPIO_InitTypeDef GPIO_InitStruct = {0};
    GPIO_InitStruct.Pin = GPIO_PIN_9 | GPIO_PIN_10;
    GPIO_InitStruct.Mode = GPIO_MODE_AF_OD;
    GPIO_InitStruct.Pull = GPIO_PULLUP;
    GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH;
    GPIO_InitStruct.Alternate = GPIO_AF4_I2C1;
    HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

    HAL_I2C_Init(&hi2c1);

}

void optical_init(void){
    if (initialized) return;
    uart1_init();
    i2c1_init();
    gpio_init();
    initialized = true;

}

void optical_uart_irq_handler(){HAL_UART_IRQHandler(&huart1);}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart){
    if(huart->Instance == USART1){
        size_t next_head_ptr = (rx_head_ptr+1) % RX_BUFFER_LENGTH;
        if(next_head_ptr != rx_tail_ptr){rx_head_ptr = next_head_ptr;}
        else{rx_overflow_flag = true;}
    }

    HAL_UART_Receive_IT(&huart1, &rx_buffer[rx_head_ptr], 1);
}

size_t optical_rx_buffer_available(){
    return (rx_head_ptr >= rs_tail_ptr)? (rx_head_ptr-rx_tail_ptr):(RX_BUFFER_LENGTH-rx_tail_ptr+rx_head_ptr);

}

static size_t optical_rx_buffer_read(uint8_t *rx_buffer, size_t length){
    size_t count_bytes_read_from_buffer = 0;
    while(count_bytes_read_from_buffer < length && optical_rx_buffer_available()>0){
        rx_buffer[count_bytes_read_from_buffer]=rx_buffer[rx_tail_ptr];
        rx_tail_ptr = (rx_tail_ptr+1)%RX_BUFFER_LENGTH;
        count_bytes_read_from_buffer++;
    }
    return count_bytes_read_from_buffer;
}
bool optical_task(){
    if(!initialized) return false;
    if(!optical_probe_connected()) return false;
    if(!optical_wait_for_handshake(5000)) return false;
}

bool optical_probe_connected(){
    if(!detect_port || detect_pin ==0) return false;
    return(HAL_GPIO_ReadPin(detect_port, detect_pin) == GPIO_PIN_RESET)
}

int optical_send_snapshot(){
    const char *snapshot_JSON = "{\"status\":\"okRightNow\"}\r\n";
    return optical_send_data((uint8_t*)snapshot_JSON, strlen(snapshot_JSON));
}

static int optical_send_data(const uint8_t *data, size_t length){
    HAL_StatusTypeDef status = HAL_UART_Transmit(&huart1, (uint8_t*)data, length, 1000);
    return (status==HAL_OK)?0:-1;
}

int optical_send_param(const char *key){
    char response[64];
    snprintf(response, sizeof(response),"{\"param\":\"%s\"}\r\n", key);
    return optical_send_data((uint8_t*)response, strlen(response));
}

static void process_received_command(const uint8_t *data, size_t length)
{
    if (length == 1) {
        switch(data[0]) {
            case 'S': 
                optical_send_snapshot();
                break;
            case 'P':
                optical_send_param("temperature");
                break;
            default:
                optical_send_data((uint8_t*)"{\"error\":\"unknown_cmd\"}\r\n", 23);
                break;
        }
    } else if (length > 1) {
        optical_send_data((uint8_t*)"{\"error\":\"invalid_cmd\"}\r\n", 24);
    }
}

static bool optical_wait_for_handshake(uint32_t timeout)
{
    uint8_t handshake_byte;
    uint32_t start_time = HAL_GetTick();
    
    while ((HAL_GetTick() - start_time) < timeout) {
        if (HAL_I2C_Slave_Receive(&hi2c1, &handshake_byte, 1, 10) == HAL_OK) {
            if (handshake_byte == HANDSHAKE_SIGNATURE) {
                uint8_t ack = 0x06;
                HAL_I2C_Slave_Transmit(&hi2c1, &ack, 1, 100);
                return true;
            }
        }
        HAL_Delay(10);
    }
    return false;
}

bool optical_process_one_command(uint32_t timeout_ms){
    uint8_t temp_buffer[2];
    size_t start_time = HAL_GetTick();

    while((HAL_GetTick()-start_time)<timeout_ms){
        size_t bytes_available = optical_rx_buffer_available();
        if(bytes_available>0){
            size_t bytes_read = optical_rx_buffer_read(temp_buffer, sizeof(temp_buffer));
            for(size_t i=0; i<bytes_read; i++){
                if(temp_buffer[i]=='\n'){
                    if(optical_rx_buffer_read(temp_buffer, 1)==1 && temp_buffer[0]=='\r'){
                        process_received_command(temp_buffer, i+1);
                        return true;
                }
                else if(temp_buffer[i]=='\r'){
                    if(optical_rx_buffer_read(temp_buffer, 1)==1 && temp_buffer[0]=='\n'){
                        process_received_command(temp_buffer, i+1);
                        return true;
                    }
                
                else{
                    optical_send_data((uint8_t*)"ERR\r\n", 5);
                    return false;
                }
                }
            }

        }
        HAL_Delay(1);
    }
    return false;
}

