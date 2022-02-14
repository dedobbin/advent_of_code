#include <stdio.h>
#include <assert.h>
#include <string.h>
#include <ctype.h>
#include "../common_lib/common.h"

int is_hex_str(char* input)
{
    for (int j = 0; j < strlen(input); j++){
        if (!isxdigit(input[j])){
            return 0;
        }
    }
    return 1;
}

int is_num_str(char* input)
{
    for (int j = 0; j < strlen(input); j++){
    if (!isdigit(input[j])){
        return 0;
    }
    }
    return 1;
}

int has_valid_byr(char* input)
{
    char* ptr = strstr(input, "byr");
    if (!ptr) return 0;
    
    ptr+=4;
    int value = 0;
    char junk[100];
    sscanf(ptr, "%d %s", &value, junk);
    return !(value < 1920 || value > 2002);
}

int has_valid_iyr(char* input)
{
    char* ptr = strstr(input, "iyr");
    if (!ptr) return 0;
    ptr+=4;
    int value = 0;
    char junk[100];
    sscanf(ptr, "%d %s", &value, junk);
    return !(value < 2010 || value > 2020);
}

int has_valid_eyr(char* input)
{
    char* ptr = strstr(input, "eyr");
    if (!ptr) return 0;
    ptr+=4;
    int value = 0;
    char junk[100];
    sscanf(ptr, "%d %s", &value, junk);
    return !(value < 2020 || value > 2030);
}

int has_valid_hgt(char* input)
{
    char* ptr = strstr(input, "hgt");
    if (!ptr) return 0;
    ptr+=4;
    // printf("%s\n", ptr);
    int value = 0;
    char res[100];
    sscanf(ptr, "%d%s", &value, res);
    //printf("%d  -   %s", value, res);
    if (memcmp(res, "cm", 2) == 0){
        if (value < 150 || value > 193){
            //printf("invalid hgt in cm: %d\n", value);
            return 0;
        }
    } else if (memcmp(res, "in", 2)==0){
        if (value < 59 || value > 76){
            //printf("invalid hgt in inch: %d\n", value);
            return 0;
        }
    } else {
        //printf("invalid hgt type\n");
        return 0;
    }
    return 1;
}

int has_valid_hcl(char* input)
{
    char* ptr = strstr(input, "hcl");
    if (!ptr) return 0;
    ptr+=4;
    if (*ptr != '#'){
        //printf("invalid hcl format, doesn't start with #\n");
        return 0;
    }

    ptr++;
    char hex_str[40];
    char junk[100];
    sscanf(ptr, "%s %s", hex_str, junk);
    return is_hex_str(hex_str);
}

int has_valid_ecl(char* input)
{
    char* ptr = strstr(input, "ecl");
    if (!ptr) return 0;
    ptr+=4;

    #define N_VALID_COLORS 7
    char valid_colors[N_VALID_COLORS][4] = {
        [0] = "amb",
        [1] = "blu",
        [2] = "brn",
        [3] = "gry",
        [4] = "grn",
        [5] = "hzl",
        [6] = "oth",
    };

    for (int j = 0; j < N_VALID_COLORS;j++){
        if (memcmp(ptr, valid_colors[j], 3) == 0){
            return 1;
        }
    }

    return 0;
}

int has_valid_pid(char* input)
{
    //pid
    char* ptr = strstr(input, "pid");
    if (!ptr) return 0;
    ptr+=4;
    char junk[100];
    char phone_number[20];
    sscanf(ptr, "%s %s", phone_number, junk);

    return !(strlen(phone_number) != 9 || !is_num_str(phone_number));
}

int part_one(str_segments_t passports_data)
{
    int n_valid = 0;
    for (int i = 0; i < passports_data.len; i++){
        char pp[100];
        strcpy(pp, passports_data.items[i]);
        if (
            strstr(pp, "byr") &&
            strstr(pp, "iyr") &&
            strstr(pp, "eyr") &&
            strstr(pp, "hgt") &&
            strstr(pp, "hcl") &&
            strstr(pp, "ecl") &&
            strstr(pp, "pid")
        ){
            n_valid++;
        }
    }
    return n_valid;
}

int part_two(str_segments_t passports_data)
{
    int n_valid = 0;
    for (int i = 0; i < passports_data.len; i++){
        if (!has_valid_byr(passports_data.items[i])) continue;
        if (!has_valid_iyr(passports_data.items[i])) continue;
        if (!has_valid_eyr(passports_data.items[i])) continue;
        if (!has_valid_hgt(passports_data.items[i])) continue;
        if (!has_valid_hcl(passports_data.items[i])) continue;
        if (!has_valid_ecl(passports_data.items[i])) continue;
        if (!has_valid_pid(passports_data.items[i])) continue;

        n_valid++;
    }
    return n_valid;
}


int main (int argc, char* argv[])
{
    printf("==day 4==\n");    

    char* contents = file_get_contents("input.txt");
    str_segments_t passports_data = str_split_multichartok(contents, "\n\n", 500);

    assert(part_one(passports_data) == 216);
    assert(part_two(passports_data) == 150);


    deinit_str_segments(&passports_data);
    free(contents);

    return 0;
}