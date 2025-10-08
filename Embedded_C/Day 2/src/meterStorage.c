#include "inc\meterStorage.h"
#include <string.h>

// Simulated flash area in RAM for unit testing / host build.
// Real embedded code would use HAL flash APIs and actual flash addresses.
static unsigned char flash_sim[STORAGE_SIZE];
static int flash_sim_initialized = 0;

// Provide definitions for globals declared as extern in headers so the
// project links for host testing. On an embedded system these may be
// placed elsewhere; for this training repository it's convenient here.
meter_reading_t g_meter_reading;
consumer_info_t g_consumer_info;
meter_hardware_info_t g_meter_hardware_info;

// Simple CRC32 implementation (polynomial 0xEDB88320)
static uint32_t crc32_compute(const void *data, size_t len)
{
	const uint8_t *p = (const uint8_t *)data;
	uint32_t crc = 0xFFFFFFFFu;
	for (size_t i = 0; i < len; ++i) {
		crc ^= p[i];
		for (int j = 0; j < 8; ++j) {
			if (crc & 1u)
				crc = (crc >> 1) ^ 0xEDB88320u;
			else
				crc = (crc >> 1);
		}
	}
	return crc ^ 0xFFFFFFFFu;
}

void storage_init(void)
{
	if (!flash_sim_initialized) {
		memset(flash_sim, 0xFF, sizeof(flash_sim)); // erased flash = 0xFF
		flash_sim_initialized = 1;
	}
	// Initialize globals used by other modules
	memset(&g_meter_reading, 0, sizeof(g_meter_reading));
	memset(&g_consumer_info, 0, sizeof(g_consumer_info));
	memset(&g_meter_hardware_info, 0, sizeof(g_meter_hardware_info));
}

int storage_save_meter_readings(const meter_reading_t *data)
{
	if (!data) return -1;
	storage_readings_memory_block_t block;
	memcpy(&block.reading, data, sizeof(block.reading));
	block.crc = crc32_compute(&block.reading, sizeof(block.reading));

	// Write to simulated flash at start of area
	if (sizeof(block) > sizeof(flash_sim)) return -1;
	memcpy(flash_sim, &block, sizeof(block));
	return 0;
}

int storage_load_meter_readings(meter_reading_t *data)
{
	if (!data) return -1;
	storage_readings_memory_block_t block;
	memcpy(&block, flash_sim, sizeof(block));

	// If flash is erased (all 0xFF) treat as empty
	unsigned char *b = (unsigned char *)&block;
	int all_ff = 1;
	for (size_t i = 0; i < sizeof(block); ++i) {
		if (b[i] != 0xFF) { all_ff = 0; break; }
	}
	if (all_ff) return -1; // nothing stored

	uint32_t calc = crc32_compute(&block.reading, sizeof(block.reading));
	if (calc != block.crc) return -2; // CRC mismatch

	memcpy(data, &block.reading, sizeof(block.reading));
	return 0;
}

int storage_save_meter_info(const meter_hardware_info_t *hardware_info, const consumer_info_t *consumer_info)
{
	if (!hardware_info || !consumer_info) return -1;
	storage_info_memory_block_t block;
	memcpy(&block.hardware_info, hardware_info, sizeof(block.hardware_info));
	memcpy(&block.customer_info, consumer_info, sizeof(block.customer_info));

	// Compute CRC over both structures
	// We'll compute by concatenating in-memory
	uint32_t crc = 0;
	crc = crc32_compute(&block.hardware_info, sizeof(block.hardware_info));
	// combine: compute CRC over customer_info by feeding its bytes
	uint32_t crc2 = crc32_compute(&block.customer_info, sizeof(block.customer_info));
	// A simple way to combine is to compute CRC over the concatenation; recompute properly:
	{
		unsigned char buf[sizeof(block.hardware_info) + sizeof(block.customer_info)];
		memcpy(buf, &block.hardware_info, sizeof(block.hardware_info));
		memcpy(buf + sizeof(block.hardware_info), &block.customer_info, sizeof(block.customer_info));
		block.crc = crc32_compute(buf, sizeof(buf));
	}

	// Store after readings block to avoid overlap
	size_t offset = sizeof(storage_readings_memory_block_t);
	if (offset + sizeof(block) > sizeof(flash_sim)) return -1;
	memcpy(flash_sim + offset, &block, sizeof(block));
	return 0;
}

int storage_load_meter_info(meter_hardware_info_t *hardware_info, consumer_info_t *consumer_info)
{
	if (!hardware_info || !consumer_info) return -1;
	storage_info_memory_block_t block;
	size_t offset = sizeof(storage_readings_memory_block_t);
	if (offset + sizeof(block) > sizeof(flash_sim)) return -1;
	memcpy(&block, flash_sim + offset, sizeof(block));

	// check erased
	unsigned char *b = (unsigned char *)&block;
	int all_ff = 1;
	for (size_t i = 0; i < sizeof(block); ++i) {
		if (b[i] != 0xFF) { all_ff = 0; break; }
	}
	if (all_ff) return -1;

	unsigned char buf[sizeof(block.hardware_info) + sizeof(block.customer_info)];
	memcpy(buf, &block.hardware_info, sizeof(block.hardware_info));
	memcpy(buf + sizeof(block.hardware_info), &block.customer_info, sizeof(block.customer_info));
	uint32_t calc = crc32_compute(buf, sizeof(buf));
	if (calc != block.crc) return -2;

	memcpy(hardware_info, &block.hardware_info, sizeof(block.hardware_info));
	memcpy(consumer_info, &block.customer_info, sizeof(block.customer_info));
	return 0;
}

uint32_t storage_calculate_crc(const meter_reading_t *data, uint32_t string_length)
{
	if (!data) return 0;
	// string_length parameter is treated as number of bytes to include; clamp to struct size
	uint32_t len = (string_length == 0) ? sizeof(*data) : string_length;
	if (len > sizeof(*data)) len = sizeof(*data);
	return crc32_compute((const void *)data, (size_t)len);
}
