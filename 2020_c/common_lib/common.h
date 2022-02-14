#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <unistd.h>
#include <regex.h> 

#ifdef _WIN32
const char* EOL  = "\n\r";
#else
const char* EOL  = "\n";
#endif

char* file_get_contents(char* file_path)
{
    FILE* fp = fopen(file_path, "r");
    if (fp == NULL){
        fprintf( stderr, "Could not open file %s.\n", file_path);
        return NULL;
    }

    fseek(fp, 0, SEEK_END);
    size_t sz = ftell(fp) + 1;
    rewind(fp);
    char* buffer = malloc(sz);
    fread(buffer, 1, sz, fp);
    buffer[sz-1] = '\0';
    
    fclose(fp);
    return buffer;
}

typedef struct str_segments_t
{
    char** items;
    size_t len;
} str_segments_t;

/**
 * Mutates input. Use a copy if you want to keep it intact.
 * This also implies string literals will cause a segfault, for they are read-only (yet c doesn't make them const char*)
 **/
str_segments_t str_split(char* input, const char* delimiter, int max)
{
    str_segments_t res = {.len = 0};
    res.items = malloc(sizeof(char*) * max);
    memset (res.items, 0, max * sizeof(char*));

    char* tok = strtok(input, delimiter);
    while(tok != NULL){
        int i = res.len;
        
        if (i >= max){
            fprintf( stderr, "Too many string segments to split, max is %d.\n", max);
            break;
        }

        res.len++;
        res.items[i] = malloc(strlen(tok)+1);
        strcpy(res.items[i], tok);
        tok = strtok(NULL, delimiter);
    }
    return res;
}

void deinit_str_segments(str_segments_t* input)
{
    for (int i = 0; i < input->len; i++)
    {
        free(input->items[i]);
        input->items[i] = NULL;
    }
    free(input->items);
    input->items = NULL;
    input->len = 0;
}

typedef struct str_segments_ints_t
{
    int* items;
    size_t len;
} str_segments_ints_t;


/**
 * Mutates input. Use a copy if you want to keep it intact.
 * This also implies string literals will cause a segfault, for they are read-only (yet c doesn't make them const char*)
 * TODO: combine this with str_split?
 **/
str_segments_ints_t str_split_ints(char* input, const char* delimiter, int max)
{
    str_segments_ints_t res = {.len = 0};
    res.items = malloc(sizeof(char*) * max);
    memset (res.items, 0, max * sizeof(char*));

    char* tok = strtok(input, delimiter);
    while(tok != NULL){
        int i = res.len;
        
        if (i >= max){
            fprintf( stderr, "Too many string segments to split, max is %d.\n", max);
            break;
        }

        res.len++;
        res.items[i] = atoi(tok);
        tok = strtok(NULL, delimiter);
    }
    return res;
}

void deinit_str_segments_ints(str_segments_ints_t *input)
{
    free(input->items);
    input->len = 0;
}

str_segments_t str_split_multichartok(char* input, const char* delimiter, int max)
{
    str_segments_t res = {.len=0};
    res.items = malloc(sizeof(char*)*max);

    char *ptr=input;
    while (1){    
        if (res.len == max){
            fprintf( stderr, "Too many string segments to split, max is %d.\n", max);
            break;
        }

        char *substr = strstr(ptr, delimiter);
        if (substr == NULL) break;
        
        int len = substr-ptr;
        res.items[res.len] = malloc(len+1);
        memcpy(res.items[res.len], ptr, len);
        res.items[res.len][len]='\0';
        res.len ++;
        ptr += len + strlen(delimiter);
        substr = strstr(ptr, delimiter);
        if (substr == NULL) break;
    }

    if (strlen(ptr) > 0 && res.len != max){
        res.items[res.len] = malloc(strlen(ptr)+1);
        strcpy(res.items[res.len], ptr);
        res.len ++;
    }

    return res;
}

/**
 * As common with regex grouping, first item is entire match
 */
str_segments_t* regex_groups(char* source, char* regex_str, int max_groups)
{
    regex_t regex_compiled;
    regmatch_t groupArray[max_groups];

    if (regcomp(&regex_compiled, regex_str, REG_EXTENDED)){
            fprintf( stderr, "Could not compile regular expression.\n");
        return NULL;
    }

    str_segments_t* groups = malloc(sizeof(str_segments_t));
    groups->len = 0;
    groups->items = malloc(max_groups * sizeof(char*));
    if (regexec(&regex_compiled, source, max_groups, groupArray, 0) == 0){
        unsigned int g = 0;
        for (g = 0; g < max_groups; g++){
            if (groupArray[g].rm_so == (size_t)-1){
                break;  // No more groups
            }

            char sourceCopy[strlen(source) + 1];
            strcpy(sourceCopy, source);
            char* match = sourceCopy + groupArray[g].rm_so;
            sourceCopy[groupArray[g].rm_eo] = 0;
            //TODO: also return pos
            //printf("Group %u: [%2u-%2u]: %s\n", g, groupArray[g].rm_so, groupArray[g].rm_eo, match);
            groups->items[g] = malloc(strlen(source) + 1);
            strcpy(groups->items[g], match);
            groups->len++;
        }
    }
    regfree(&regex_compiled);
    return groups;
}


#define MAP_LEN 20
#define MAP_KEY_LEN 40
#define MAP_VALUE_LEN 100

//todo: make dynamic len
typedef struct map_t
{
    char keys[MAP_LEN][MAP_KEY_LEN];
    char values[MAP_LEN][MAP_VALUE_LEN];
    int len;
} map_t;


void map_insert(map_t* map, char* key, char* value)
{    
    //todo: input validation?
    int i = map->len;
    strcpy(map->keys[i], key);
    strcpy(map->values[i], value);

    map->len++;
}

char* map_find(map_t* map, char* key)
{
    for (int i = 0; i < map->len; i++)
    {
        if (strcmp(map->keys[i], key) == 0){
            return map->values[i];
        }
    }
    return NULL;
}

int* arr_map_str_to_int(char** input, int input_len, int (*callback)(char*,char**, int))
{
    int* res = malloc(input_len*sizeof(int));
    for (int i = 0; i < input_len; i++){
        if (!callback)
            res[i] = atoi(input[i]);
        else 
            res[i] = callback(input[i], input, input_len);
    }
    return res;
}

typedef struct timer_data_t
{
    clock_t start;
    float last_measured;
} timer_data_t;

timer_data_t timer_data = {.last_measured = 0};

void timer_start()
{
    timer_data.start = clock();
}

float timer_end(int print)
{   
    clock_t end = clock();
    timer_data.last_measured = (double)(end - timer_data.start) / CLOCKS_PER_SEC;
    if (print)
        printf("Time taken: %f seconds\n", timer_data.last_measured);
    return timer_data.last_measured;
}