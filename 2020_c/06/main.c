#include <stdio.h>
#include <assert.h>
#include "../common_lib/common.h"

int alphabet_pos(char in)
{
    return in - 97;
}

int get_n_yes(char* input)
{
    char map[26] = {0};
    for (int j = 0; j < strlen(input); j++){
        if (input[j] == '\n') continue;
        map[alphabet_pos(input[j])] = 1;
    }

    int n_yes = 0;
    for (int j = 0; j < 26; j++){
        if (map[j]){
            n_yes++;
        }
    }
    return n_yes;
}

int part_one_v1(char** groups, int n_groups)
{
    int n_yes = get_n_yes(groups[0]);
    for (int i = 1; i < n_groups; i++){
        n_yes += get_n_yes(groups[i]);
    }
    //printf("%d\n", n_yes);
    return n_yes;   
}

int part_one_v2(char** groups, int n_groups)
{
    int total_n_yes = 0;
    for (int i = 0; i < n_groups; i++){
        int n_yes = 0;
        for (int j = 0; j < 26; j++){
            char needle = j+97;
            if (strchr(groups[i], needle)){
                n_yes ++;
            }
        }
        total_n_yes = total_n_yes += n_yes;
    }
    return total_n_yes;
}

int part_two(char ** groups, int n_groups)
{
    int total = 0;
    for (int i = 0; i < n_groups; i++){
        int n_all_answered_y = 0;
        int map[26] = {0};
        char* s = groups[i];
        int group_sz;
        for (group_sz=1; s[group_sz]; s[group_sz]=='\n' ? group_sz++ : *s++);
        
        for (int j = 0; j < strlen(groups[i]); j++){ 
            if (groups[i][j] == '\n') continue;
            map[groups[i][j] - 97]++;
        }

        for (int j = 0; j < 26; j++){
            if (map[j] == group_sz){
                n_all_answered_y++;
            }
        }
        total += n_all_answered_y;
        
    }
    //printf("total: %d\n", total);
    return total;
}

int main (int argc, char* argv[])
{
    printf("==day 6==\n");    
    char* content = file_get_contents("input.txt");
    str_segments_t groups = str_split_multichartok(content, "\n\n", 500);
    free(content);

    assert(part_one_v1(groups.items, groups.len) == 6530);
    timer_start();
    part_one_v1(groups.items, groups.len);
    timer_end(1);

    //v2 is faster, but v1 also has a function call so ehhhh
    assert(part_one_v2(groups.items, groups.len) == 6530);
    timer_start();
    part_one_v2(groups.items, groups.len);
    timer_end(1);

    assert(part_two(groups.items, groups.len) == 3323);



    deinit_str_segments(&groups);
    
    return 0;
}