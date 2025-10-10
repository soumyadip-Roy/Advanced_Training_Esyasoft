#ifndef H_METER_INFO
#define H_METER_INFO

#include <stdint.h>

typedef struct {
    char consumer_id[16];
    char consumer_name[32];
    char tariff_plan[16];
    char address[64];
} consumer_info_t;

typedef struct {
    char meter_serial[20];
    char firmware_version[12];
    char hardware_version[12];
    float voltage_calibration;
    float current_calibration;
    float power_calibration;
    } meter_hardware_info_t ;

extern consumer_info_t g_consumer_info;
extern meter_hardware_info_t g_meter_hardware_info;

void meter_info_init(void);

#endif