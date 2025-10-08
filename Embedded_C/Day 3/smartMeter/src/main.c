#include "..\lib\inc\meterData.h"
#include "..\lib\inc\meterInfo.h"
#include "..\lib\inc\meterReading.h"
#include "..\lib\inc\meterStorage.h"

#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include "stm32f3xx.h"

int main(){


    data_refresh();
    meter_info_init();
    meter_reading_init();
    double delta_kWh, power_factor;
    meter_reading_t meter_reading_data;

    while(1){
        // data= fetch_reading(); After DLMS 
        // voltage_value = data.voltage_value_response;
        // current_value = data.current_value_response;
        // power_factor_value
        // frequency_value
        // delta_kWh
        

        meter_reading_update_energy(delta_kWh, power_factor_value);
        meter_reading_update_quality(voltage_value, current_value, power_factor_value, frequency_value);
        storage_save_meter_readings();
        data_add_new_entry(meter_reading_data);
    
    }
    return 0;
}


