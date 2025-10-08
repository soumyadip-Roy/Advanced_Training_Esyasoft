#ifndef H_METER_STORAGE_MANAGEMENT
#define H_METER_STORAGE_MANAGEMENT

#include <stdint.h>
#include "meterInfo.h"
#include "meterReading.h"

#define FLASH_BASE_ADDRESS 0x08060000
#define STORAGE_SIZE 0x1000000

typedef struct {
    meter_reading_t reading;
    uint32_t crc;
} storage_readings_memory_block_t;

typedef struct {
    meter_hardware_info_t hardware_info;
    consumer_info_t customer_info;
    uint32_t crc;
} storage_info_memory_block_t;

void storage_init(void);
int storage_save_meter_readings(const meter_reading_t *data);
int storage_load_meter_readings(meter_reading_t *data);
int storage_save_meter_info(const meter_hardware_info_t *hardware_info, const consumer_info_t *consumer_info);
int storage_load_meter_info(meter_hardware_info_t *hardware_info, consumer_info_t *consumer_info);

uint32_t storage_calculate_crc(const meter_reading_t *data, uint32_t string_length);


#endif