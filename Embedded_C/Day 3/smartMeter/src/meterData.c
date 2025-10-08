#include <stdio.h>
#include <string.h>
#include <stdint.h>
#include <time.h>
#include "..\lib\inc\meterReading.h"
#include "..\lib\inc\meterData.h"

#define MAX_DAYS 90
#define TOPIC
#define REFRESH_LOG

static meter_reading_t daily_store[MAX_DAYS];
static uint8_t entry_count = 0;
static uint8_t day_number = 0;
void log_entry_publish_new(){};
void log_entry_refresh(){};
static uint32_t* get_current_time(){
    return (uint32_t*)time(NULL);
}

static size_t serialize_report(uint8_t *out_buf, size_t max_length){
    // Serialize Function 
}

void data_refresh(){
    memset(daily_store,0,sizeof(daily_store));
    entry_count = 0;
    day_number = 0;
    printf("Data Refreshed");
    log_entry_refresh();
}
void data_add_new_entry( meter_reading_t meter_reading ){
    if(entry_count<MAX_DAYS){
        daily_store[entry_count] = meter_reading;
        entry_count++;
        
    }
    else {
        for(int i=1;i<MAX_DAYS;i++){
            daily_store[i-1] = daily_store[i];
        }
        daily_store[MAX_DAYS-1]  = meter_reading;
        entry_count=MAX_DAYS;
        day_number++;

    }
    log_entry_publish_new(entry_count);
}