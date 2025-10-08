#ifndef H_METER_READING
#define H_METER_READING

#include <stdint.h>
#include <time.h>

typedef struct {
// Power Reading
double kWh_import;
double kWh_export;
double kVAh;
double kVArh_import;
double kVArh_export;

// Demand Request
double max_demand_kw;
double max_demand_kva;
time_t max_demand_timestamp;

// Power Quality
float voltage_avg;
float voltage_min;
float voltage_max;

float current_avg;
float current_peak;

float power_factor_avg;
float power_factor_min;
float power_factor_max;

float frequency;

// Event flags
uint8_t tamper_detected;
uint8_t power_fail_count;
uint8_t cover_open;
} meter_reading_t;

extern meter_reading_t g_meter_reading;

void meter_reading_init(void);
void meter_reading_update_energy(double delta_kWh, double power_factor);
void meter_reading_update_quality(float voltage_value, float current_value, float power_factor_value, float frequency_value);


#endif