#include "inc\meterInfo.h"
#include <string.h>

// If The .h file doesnt initialize the values, then we must explicitly declare them here.

void meter_info_init(){
    strcpy(g_consumer_info.consumer_id, "CUST12345");
    strcpy(g_consumer_info.consumer_name, "Soumyadip Roy");
    strcpy(g_consumer_info.tariff_plan, "Domestic");
    strcpy(g_consumer_info.address, "43,FD-III, Salt Lake, Kolkata");

    strcpy(g_meter_hardware_info.meter_serial, "STM32L1X1040PK20P10");
    strcpy(g_meter_hardware_info.firmware_version, "cost125.0.1");
    strcpy(g_meter_hardware_info.hardware_version, "reg1.2.9.10");
    g_meter_hardware_info.voltage_calibration = 220.13;
    g_meter_hardware_info.current_calibration = 20.44;
    g_meter_hardware_info.power_calibration = 4000.55;
}