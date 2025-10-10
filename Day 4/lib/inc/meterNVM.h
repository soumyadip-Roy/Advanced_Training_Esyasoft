#ifndef H_METER_NVM
#define H_METER_NVM

#include <stdio.h>
#include "meterLocation.h"

void nvm_init(void);
void nvm_save_gps(const location_t *data);

#endif

