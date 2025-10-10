#ifndef H_METER_DATA
#define H_METER_DATA
#include <stdint.h>
#include "meterReading.h"

static uint32_t* get_current_time();
static size_t serialize_report(uint8_t *out_buf, size_t max_length);
void data_refresh();
void data_add_new_entry( meter_reading_t meter_reading );

#endif