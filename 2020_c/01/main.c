#include <stdio.h>
#include <assert.h>
#include <unistd.h>
#include "common.h"

int find_two(int* input, int input_len, int target)
{
    int a, b;
    for (int i = 0; i < input_len; i++){
        for (int j = 0; j < input_len; j++){
            if (i == j) continue;
            a = input[i];
            b = input[j];
            if (a + b == target){
                return a * b;
            }

        }
    }
    fprintf( stderr, "Could not find 2 numbers summing %d\n", target );
    return -1;
}

int find_two_mapped(int* input, int input_len, int target)
{
    #define FIND_TWO_MAX 5000
    int values[FIND_TWO_MAX] = {0};

    int min = FIND_TWO_MAX;
    int max = 0;

    for (int i = 0; i < input_len; i++){
        int index = input[i];
        if (index > FIND_TWO_MAX){
            fprintf( stderr, "Number %d is too big\n", index );
            return -1;
        }
        if (index < min){
            min = index;
        } else if (index > max){
            max = index;
        }
        values[index] = 1;
    }

    for (int i = min; i < max; i++){
        if (values[i] == 1){
            if (values[target - i] == 1 && i != target-i)
                return i * (target-i);
        }
    }

    fprintf( stderr, "Could not find 2 numbers summing %d\n", target );
    return -1;
}

int find_three(int* input, int input_len, int target)
{
    int a, b, c;
    for (int i = 0; i < input_len; i++){
        for (int j = 0; j < input_len; j++){
            for (int k = 0; k < input_len; k++){
                if (i == j || i == k || j == k) continue;
                a = input[i];
                b = input[j];
                c = input[k];
                if (a + b + c == target){
                    //printf(" %d * %d * %d = %d ", a,b,c,a*b*c);
                    return a * b * c;
                }
            }
        }
    }
    fprintf( stderr, "Could not find 3 numbers summing %d\n", target );
    return -1;
}

int find_three_mapped(int* input, int input_len, int target)
{
    #define FIND_THREE_MAX 5000
    int values[FIND_THREE_MAX] = {0};
    
    int min = FIND_THREE_MAX;
    int max = 0;
    for (int i = 0; i < input_len; i++){
        int index = input[i];;
        if (index > FIND_THREE_MAX){
            fprintf( stderr, "Number %d is too big\n", index );
            return -1;
        }

        if (index < min){
            min = index;
        } else if (index > max){
            max = index;
        }

        values[index] = 1;
    }

    for (int i = min; i < max; i++){
        if (values[i] == 1){
            for (int j = min; j < max; j++){
                if (i + j >= target) continue;
                if (values[j] == 1){
                    if (i == j) continue;
                    int cur_value = i+j;
                    int need = target - cur_value;
                    if (values[need]==1 && need != i && need != j){
                        return i*j*need;
                    }
                }
            }
        }
    }
    fprintf( stderr, "Could not find 3 numbers summing %d\n", target );
    return -1;
}

int main (int argc, char* argv[])
{
    //Note: it actually seems faster to split the input into char arrays and atoi them in the processing function(s) 
    // instead of using int array directly ???????
    printf("day 1\n");

    printf("splitting strings - ");
    timer_start();
    char* content = file_get_contents("input.txt");
    str_segments_ints_t int_segments = str_split_ints(content, EOL, 200);
    timer_end(1);

    printf("find_two - ");
    timer_start();
    assert(find_two(int_segments.items, int_segments.len, 2020) == 744475);
    timer_end(1);

    printf("find_two_mapped - ");
    timer_start();
    assert(find_two_mapped(int_segments.items, int_segments.len, 2020) == 744475);
    timer_end(1);

    printf("find_three - ");
    timer_start();
    assert(find_three(int_segments.items, int_segments.len, 2020) == 70276940);
    timer_end(1);

    printf("find_three_mapped - ");
    timer_start();
    assert(find_three_mapped(int_segments.items, int_segments.len, 2020) == 70276940);
    timer_end(1);

    deinit_str_segments_ints(&int_segments);
    free(content);
    return 0;
}