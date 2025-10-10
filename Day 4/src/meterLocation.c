/* meterLocation.c
 * Simple GPS UART transaction and NMEA parsing (GGA/RMC) using STM32 HAL_UART.
 * This implementation is intentionally small and conservative: it reads lines
 * from the GPS module and extracts latitude/longitude when available.
 */

#include "..\lib\inc\meterLocation.h"
#include "..\lib\inc\meterLog.h"
#include "stm32f3xx_hal.h"
#include <string.h>
#include <stdlib.h>

extern UART_HandleTypeDef huart; /* Provide this from your platform startup */

/* Helper: read one byte from UART with timeout (ms) */
static int uart_read_byte(uint8_t *b, uint32_t timeout_ms)
{
    if (HAL_UART_Receive(&huart, b, 1, timeout_ms) == HAL_OK) return 1;
    return 0;
}

/* Helper: read up to buf_len-1 bytes into buf until newline or timeout per byte.
 * Returns number of bytes read (excluding terminating null). */
static int uart_read_line(char *buf, size_t buf_len, uint32_t per_byte_timeout_ms)
{
    if (!buf || buf_len == 0) return 0;
    size_t idx = 0;
    uint8_t ch;
    while (idx + 1 < buf_len) {
        if (!uart_read_byte(&ch, per_byte_timeout_ms)) break;
        buf[idx++] = (char)ch;
        if (ch == '\n') break;
    }
    buf[idx] = '\0';
    return (int)idx;
}

/* Convert NMEA lat/lon field (ddmm.mmmm or dddmm.mmmm) and direction into float degrees */
static int nmea_to_degrees(const char *field, char direction, float *out)
{
    if (!field || !*field || !out) return 0;
    double val = atof(field);
    /* degrees are the integer leading digits: 2 for lat, 3 for lon */
    int deg = 0;
    if (val >= 10000.0) {
        /* lon: dddmm.mmmm */
        deg = (int)(val / 100.0);
    } else {
        /* lat: ddmm.mmmm */
        deg = (int)(val / 100.0);
    }
    double minutes = val - (deg * 100.0);
    double degrees = deg + minutes / 60.0;
    if (direction == 'S' || direction == 'W') degrees = -degrees;
    *out = (float)degrees;
    return 1;
}

/* Simple parser: extract lat/lon from NMEA GGA or RMC sentence. Returns 1 if parsed. */
static int parse_nmea_for_location(const char *line, location_t *loc)
{
    if (!line || !loc) return 0;
    /* Check for GGA or RMC sentence */
    if (strncmp(line, "$GPGGA", 6) == 0 || strncmp(line, "$GNGGA", 6) == 0) {
        /* GGA: $GPGGA,time,lat,NS,lon,EW,fix,... */
        char copy[128]; strncpy(copy, line, sizeof(copy)-1); copy[sizeof(copy)-1]=0;
        char *tokens[16]; size_t t=0;
        char *p = strtok(copy, ",");
        while (p && t < sizeof(tokens)/sizeof(tokens[0])) { tokens[t++] = p; p = strtok(NULL, ","); }
        if (t >= 6 && tokens[2][0] != '\0' && tokens[4][0] != '\0') {
            if (nmea_to_degrees(tokens[2], tokens[3][0], &loc->latitude) && nmea_to_degrees(tokens[4], tokens[5][0], &loc->longitude)) {
                return 1;
            }
        }
    }
    if (strncmp(line, "$GPRMC", 6) == 0 || strncmp(line, "$GNRMC", 6) == 0) {
        /* RMC: $GPRMC,time,status,lat,NS,lon,EW,speed,course,date,... */
        char copy[128]; strncpy(copy, line, sizeof(copy)-1); copy[sizeof(copy)-1]=0;
        char *tokens[16]; size_t t=0;
        char *p = strtok(copy, ",");
        while (p && t < sizeof(tokens)/sizeof(tokens[0])) { tokens[t++] = p; p = strtok(NULL, ","); }
        if (t >= 7 && tokens[2] && tokens[2][0] == 'A' && tokens[3][0] != '\0' && tokens[5][0] != '\0') {
            if (nmea_to_degrees(tokens[3], tokens[4][0], &loc->latitude) && nmea_to_degrees(tokens[5], tokens[6][0], &loc->longitude)) {
                return 1;
            }
        }
    }
    return 0;
}

void location_gps_init(void)
{
    /* Optionally configure/init UART from huart if needed. Assume huart is
     * configured elsewhere (CubeMX or platform init). We can test by sending
     * a short command or simply clear input buffer. */
    uint8_t flush;
    while (HAL_UART_Receive(&huart, &flush, 1, 1) == HAL_OK) { /* drain */ }
}

bool location_gps_get_data(location_t* data)
{
    if (!data) return false;
    char line[128];
    const uint32_t per_byte_timeout_ms = 200; /* tune as needed */

    /* Read up to several NMEA lines searching for a valid fix */
    for (int i = 0; i < 5; ++i) {
        int len = uart_read_line(line, sizeof(line), per_byte_timeout_ms);
        if (len <= 0) continue;
        if (parse_nmea_for_location(line, data)) {
            log_record("GPS DATA RECEIVED: %f %f", data->latitude, data->longitude);
            return true;
        }
    }
    return false;
}
