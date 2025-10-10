#ifndef H_METER_DLMS_HANDLER
#define H_METER_DLMS_HANDLER

#include <stdint.h>

typedef enum {

    DLMS_GET,
    DLMS_SET,
    DLMS_ACTION,
    DLMS_READ,
    DLMS_WRITE

} dlms_request_type_t;

typedef struct{
    dlms_request_type_t type;
    uint32_t obis_code;
    uint8_t *dlms_payload;
    uint16_t dlms_payload_length; } dlms_response_t;

void dlms_handler_init();
int dlms_process_request(dlms_request_type_t *req, dlms_response_t *resp);
bool dlms_decrypt_function(const uint8_t *input_OBIS, size_t input_len, uint8_t *outout, size_t max_output_len);
int dlms_encrypt_response(uint8_t *data, uint16_t data_len);

#endif