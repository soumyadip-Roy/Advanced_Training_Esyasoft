#ifndef OPTICAL_H
#define OPTICAL_H

#include <stdint.h>
#include <stdbool.h>
#include <stddef.h>

void optical_init(void);

void optical_task(void *argument);

bool optical_probe_connected(void);

int optical_send_snapshot(void);

int optical_send_param(const char *key);

bool optical_process_one_command(uint32_t timeout_ms);

#endif 