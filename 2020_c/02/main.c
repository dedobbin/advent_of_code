#include <stdio.h>
#include <assert.h>
#include "common.h"


int part_one(str_segments_t input)
{
    int n_valid = 0;
    int i;
    for (i = 0; i < input.len; i++){
        int min, max;
        char letter;
        char pw[50];

        sscanf(input.items[i], "%d-%d %c: %s", &min, &max, &letter, pw );
        char* s = pw;
        int j=0;
        for (j=0; s[j]; s[j]==letter ? j++ : *s++);
        if (j >= min && j <= max){
            n_valid++;
        } 
    }

    //printf("n valid: %d, %d total\n", n_valid,i);
    return n_valid;
}

int part_two(str_segments_t input)
{
    int n_valid = 0;
    int i;
    for (i = 0; i < input.len; i++){
        int min, max;
        char letter;
        char pw[50];

        sscanf(input.items[i], "%d-%d %c: %s", &min, &max, &letter, pw );
        char* s = pw;
        int j=0;
        for (j=0; s[j]; s[j]==letter ? j++ : *s++);
        if ((pw[min-1] == letter) ^ (pw[max-1] == letter)){
            n_valid++;
        } 
    }

    //printf("n valid: %d, %d total\n", n_valid,i);
    return n_valid;
}




int main (int argc, char* argv[])
{
    printf("==day 2==\n");    
    char* content = file_get_contents("input.txt");
    str_segments_t lines = str_split(content, "\n", 1000);

    assert(part_one(lines) == 460);
    assert(part_two(lines) == 251);
    

    deinit_str_segments(&lines);
    free(content);
    return 0;
}