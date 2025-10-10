#include "..\lib\inc\meterReading.h"
#include <string.h>

//Again, not initilaizing the user defined variable here, but need to do in the field as wont be initialized in the header files. 
extern meter_reading_t g_meter_reading;

void meter_reading_init(){
    memset(&g_meter_reading,0,sizeof(g_meter_reading));
}

void meter_reading_update_energy(double delta_kWh,double power_factor){
    g_meter_reading.kWh_import += delta_kWh;
    g_meter_reading.kVAh += (delta_kWh/power_factor);
}

void meter_reading_update_quality(float voltage_value, float current_value, float power_factor_value, float frequency_value){

    // Update frequency
    g_meter_reading.frequency = frequency_value;

    // Update voltage
    g_meter_reading.voltage_avg = (g_meter_reading.voltage_avg+voltage_value)/2.00;
    g_meter_reading.voltage_max = (g_meter_reading.voltage_max>voltage_value)? g_meter_reading.voltage_max: voltage_value;
    g_meter_reading.voltage_min = (g_meter_reading.voltage_min<voltage_value)? g_meter_reading.voltage_min: voltage_value;

    // Update power factor
    g_meter_reading.power_factor_avg = (g_meter_reading.power_factor_avg+power_factor_value)/2.00;
    g_meter_reading.power_factor_max = (g_meter_reading.power_factor_max>power_factor_value)? g_meter_reading.power_factor_max: power_factor_value;
    g_meter_reading.power_factor_min = (g_meter_reading.power_factor_min<power_factor_value)? g_meter_reading.power_factor_min: power_factor_value;

    // Update current
    g_meter_reading.current_avg = (g_meter_reading.current_avg+current_value)/2.00;
    g_meter_reading.current_peak = (g_meter_reading.current_peak>current_value)?g_meter_reading.current_peak:current_value;

}