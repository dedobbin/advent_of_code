#include <stdio.h>
#include <assert.h>
#include "../common_lib/common.h"


void print_as_binary(char input, int new_line)
{
    int i;
    for (i = 0; i < 8; i++) {
        printf("%d", !!((input << i) & 0x80));
    }
    if (new_line)
        printf("\n");
}

/**
* returns ptr to 2 ints 
* 0 = row, 1 = seat
* either value is -1 if not found with input
**/
int* get_seat(char* str)
{
    //printf("%s, %d\n", str, strlen(str));

    int* res = malloc(2 * sizeof(int));
    int lower_row = 0;
    int upper_row = 127;
    int lower_seat = 0;
    int upper_seat = 7;
    for (int j = 0; j < strlen(str);j++){
        int row_size = upper_row-lower_row;
        int seat_size = upper_seat-lower_seat;
        if (str[j] == 'F'){
            if (row_size == 1){
                res[0] = lower_row;
                continue;
            }

            upper_row -= row_size / 2;
            if (upper_row % 2 == 0){
                upper_row -=1;
            }
        } else if (str[j] == 'B'){
            if (row_size == 1){
                res[0] = upper_row;
                continue;
            }
            
            lower_row += row_size / 2;
            if (lower_row % 2 != 0){
                lower_row +=1;
            }
        } else if (str[j] == 'R'){
            if (seat_size ==1){
                res[1] = upper_seat;
                continue;
            }
            
            lower_seat += seat_size / 2;
            if (lower_seat % 2 != 0){
                lower_seat +=1;
            }

        } else if (str[j] == 'L'){
            if (seat_size ==1){
                res[1] = lower_seat;
                continue;
            }
    
            upper_seat -= seat_size / 2;
            if (upper_seat % 2 == 0){
                upper_seat -=1;
            }
        }
    }
    //int seat_id = res[0] * 8 + res[1];
    return res;
}

int get_seat_id(int row, int seat)
{
    return row * 8 + seat;
}

int part_one(str_segments_t input)
{
    int highest_id = -1;
    for (int i = 0; i < input.len;i++){
        int* seat = get_seat(input.items[i]);
        int seat_id = get_seat_id(seat[0], seat[1]);
        if (seat_id > highest_id){
            highest_id = seat_id;
        }
        free(seat);
    }
    return highest_id;
}

void part_two(str_segments_t input)
{
    printf("TODO: FINISH ME!!!!\n");
    assert(1==2);
    char occupied[128] = {0};
    for (int i = 0; i < input.len;i++){
        int* seat = get_seat(input.items[i]);
        //printf("row: %d, seat: %d\n", seat[0], seat[1]);
        occupied[seat[0]] |= (1 << seat[1]); 
    } 
    int is_start_row = 1; //first sequence of empty rows don't exist.
    int previous_row_was_empty = 1;
    for (int i = 0; i < 128;i++){
        // printf("%u: ", occupied[i] & 0xFF);
        // print_as_binary(occupied[i], 1);
        
        if ((occupied[i] & 0xFF) != 0xFF){
            printf("seat at row %d: ",i);
            print_as_binary(occupied[i], 1);
            //printf("\t%d\n", occupied[i] & 0xFF);
            //TODO: check if seat with target+1 and target-1 exist.
        } 
    }
}

int main (int argc, char* argv[])
{
    printf("==day 5==\n");    

    char* contents = file_get_contents("input.txt");
    str_segments_t lines = str_split(contents, "\n", 900);
    free(contents);

    assert(part_one(lines) == 906);
    //part_two(lines);

    deinit_str_segments(&lines);

    return 0;
}