#include "..\lib\inc\meterStorage.h"
#include "..\lib\inc\meterInfo.h"
#include "..\lib\inc\meterReading.h"
#include <string.h>

#define READING_ADR FLASH_BASE_ADDRESS
#define INFO_ADR (READING_ADR+0x500000)

uint32_t crc32_compute(const void *data, size_t length)
{
	uint32_t crc = 0xFFFFFFFF;
	if (data == NULL) { return crc; }
	for (size_t i = 0; i < length; i++) {
		crc ^= ((uint8_t*)data)[i] << 24;
		for (int j = 0; j < 8; j++) {
			if (crc & 1) {
				crc = (crc << 1) ^ 0x04C11DB7;
			} else {
				crc <<= 1;
			}
		}
	}
	return crc ^ 0xFFFFFFFF;
}

void storage_init(void)
{
	memset(&g_meter_reading, 0, sizeof(g_meter_reading));
	memset(&g_consumer_info, 0, sizeof(g_consumer_info));
}

int storage_save_meter_readings(const meter_reading_t *data)
{
	if (!data) return -1;
	storage_readings_memory_block_t block;
	memcpy(&block.reading, data, sizeof(block.reading));
	block.crc = crc32_compute(&block.reading, sizeof(block.reading));
	memcpy((void*)READING_ADR, &block, sizeof(block));
	return 0;
}

int storage_load_meter_readings(meter_reading_t *data)
{
	if (!data) return -1;
	storage_readings_memory_block_t *block = (storage_readings_memory_block_t*)READING_ADR;
	uint32_t calc = crc32_compute(&block->reading, sizeof(block->reading));
	if (calc != block->crc) return -2;
	memcpy(data, &block->reading, sizeof(block->reading));
	return 0;
}

int storage_save_meter_info(const meter_hardware_info_t *hardware_info, const consumer_info_t *consumer_info)
{
	if (!hardware_info || !consumer_info) return -1;
	storage_info_memory_block_t block;
	memcpy(&block.hardware_info, hardware_info, sizeof(meter_hardware_info_t));
	memcpy(&block.customer_info, consumer_info, sizeof(consumer_info_t));

	unsigned char buf[sizeof(block.hardware_info) + sizeof(block.customer_info)];
	memcpy(buf, &block.hardware_info, sizeof(block.hardware_info));
	memcpy(buf + sizeof(block.hardware_info), &block.customer_info, sizeof(block.customer_info));
	block.crc = crc32_compute(buf, sizeof(buf));

	// Write to flash memory at INFO_ADR
	memcpy((void*)INFO_ADR, &block, sizeof(block));
	return 0;
}

int storage_load_meter_info(meter_hardware_info_t *hardware_info, consumer_info_t *consumer_info)
{
	if (!hardware_info || !consumer_info) return -1;
	storage_info_memory_block_t *block = (storage_info_memory_block_t*)INFO_ADR;

	unsigned char buf[sizeof(block->hardware_info) + sizeof(block->customer_info)];
	memcpy(buf, &block->hardware_info, sizeof(block->hardware_info));
	memcpy(buf + sizeof(block->hardware_info), &block->customer_info, sizeof(block->customer_info));
	uint32_t calc = crc32_compute(buf, sizeof(buf));
	if (calc != block->crc) return -2;

	memcpy(hardware_info, &block->hardware_info, sizeof(block->hardware_info));
	memcpy(consumer_info, &block->customer_info, sizeof(block->customer_info));
	return 0;
}

uint32_t storage_calculate_crc(const meter_reading_t *data, uint32_t string_length)
{
	if (!data) return 0;
	uint32_t length = (string_length == 0) ? sizeof(*data) : string_length;
	if (length > sizeof(*data)) length = sizeof(*data);
	return crc32_compute((const void *)data, (size_t)length);
}
