#ifndef H_METER_DIAGNOSTIC
#define H_METER_DIAGNOSTIC

typedef enum {
    TAMPER_NONE,
    TAMPER_MAGNETIC,
    TAPER_COVER_OPEN,
    TAMPER_REVERSE_CURRENT
} diagnostic_event_t;

void diagnostic_init(void);
void diagnostic_event_isr_call(void);
int diagnostic_event_reverse_current(void);
int diagnostic_event_cover_open(void);
int diagnostic_event_magnetic(void);
int diagnostic_event_forced(diagnostic_event_t trigger);

#endif