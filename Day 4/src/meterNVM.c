#include "..\lib\inc\meterNVM.h"
#include "stm32f3xx_hal.h"
#include 

void nvm_init(void){

}

void nvm_save_gps(const location_t *data){

    HAL_Flash_Unlock();
    uint32_t *ptr = (uint32_t*) NVM_BASE_ADDR;
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, (uint32_t)ptr, *(uint32_t*)&data->latitude);
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, (uint32_t)(ptr+1), *(uint32_t*)&data->longitude);
    HAL_FLASH_Lock();
}