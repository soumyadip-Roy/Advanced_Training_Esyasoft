#include "..\lib\inc\meterLoh.h"

#include <stdio.h>
#include <time.h>
#include <string.h>

void log_init(void){
    FILE *log_file;
    log_file = fopen("meter_log.log");
    if(log_file==NULL){
        fprintf(stderr,"Error opening log file!\n");
    }
}

void log_record(const char *message){
    FILE *log_file;
    char timestamp[80];
    time rawtime;

    log_file = fopen("meter_log.log","a+");
    if(log_file==NULL){
        fprintf(stderr,"Error opening log file!\n");
    }

    time(&rawtime);
    struct tm *info;
    info = localtime(&rawtime);
    strftime(timestamp, 80, "%Y-%m-%d %H:%M:%S", info);

    fprintf(log_file, "[%s] -- message recieved and beginning log.\n", timestamp);

    fprintf(log_file,"[%s] -- %s", timestamp, message);
    fprintf(log_file,"[%s] -- log closed", timestamp);

    fclose(log_file)

}