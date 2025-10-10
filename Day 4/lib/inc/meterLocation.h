#ifndef H_METER_LOCATION
#define H_METER_LOCATION

#include <stdint.h>
#include <stdio.h>
#include <stdbool.h>

typedef struct {
    float latitude;
    float longitude;
} location_t;

void location_gps_init(void);
bool location_gps_get_data(location_t* data);

#endif