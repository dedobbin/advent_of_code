#include <stdio.h>
#include <assert.h>   
#include "../common_lib/common.h"

int cur_bag = 0;
map_t bag_type_to_int;  //todo: can't i just use array and int as key...
//maps containees
static int bag_map[100][100];
void parse_rule_into_bag_map(char* rule)
{
    //printf("parsing rule: %s\n", rule);
    
    int tail_len;
    char* s = rule;
    for (tail_len=1; s[tail_len]; s[tail_len]==',' ? tail_len++ : *s++);
    //int tail_len = comma_count + 1;
    
    char regex[300];

    char base[] = "([a-z ]*) bags contain";
    char tail[] =  "[,]? ([a-zA-Z0-9]*) ([a-z ]*) bag[s]?";
    
    strcpy(regex, base);
    
    if (!strstr(rule, " no ")){
        for (int i=0; i<tail_len; i++){
            strcpy(regex + strlen(regex), tail);
        }
    }
    //printf("%s\n", regex);

    str_segments_t* groups = regex_groups(rule, regex, 20);

    for (int i = 1; i < groups->len; i+=2){
         if (!map_find(&bag_type_to_int, groups->items[i])){
            //pretty janky way of setting up dynamic enum type thing. will store value as string because most convient with how map works but ehhh
            char str[10];
            sprintf(str, "%d", cur_bag++);
            map_insert(&bag_type_to_int, groups->items[i], str);
            //printf("%s: %d\n", groups->items[i], atoi(map_find(&bag_type_to_int, groups->items[i])));
        }  
    }

    //set current rule in bag map
    for (int j = 3; j < groups->len; j+=2){
        //printf("container bag: %s\n", groups->items[1]);
        int container = atoi(map_find(&bag_type_to_int, groups->items[1]));
        //printf("\tcontainee bag: %s\n", groups->items[j]);
        int containee = atoi(map_find(&bag_type_to_int, groups->items[j]));
        int n_contain = atoi(groups->items[j-1]);
        bag_map[container][containee] = n_contain;
    }

    //test print
    // for (int container_i = 0; container_i < bag_type_to_int.len; container_i++){
    //     char* container = bag_type_to_int.keys[container_i];
    //     printf("%s contains: ", container);
    //     for (int containee_i = 0; containee_i < bag_type_to_int.len; containee_i++){
    //         char* containee = bag_type_to_int.keys[containee_i];
    //         int n_contains = bag_map[container_i][containee_i];
    //         if (n_contains > 0){
    //             printf("%d %s ", n_contains, containee);
    //         }
    //     }
    //     printf("\n");
    // }
    // printf("\n");

    deinit_str_segments(groups);
    free(groups);
}

int open_bag(int container_i, int target_i)
{
    if (container_i == target_i){
        return 1;
    }
    char* container_name = bag_type_to_int.keys[container_i];
    for (int containee_i = 0; containee_i < bag_type_to_int.len; containee_i++){
        int n_containers = bag_map[container_i][containee_i];
        if (n_containers > 0){
            char* containee_name = bag_type_to_int.keys[containee_i];
            printf("%s contains %d %s\n", container_name, n_containers, containee_name);
            if (open_bag(containee_i, target_i)){
                return 1;
            }
        }
    }
    return 0;

}

int main (int argc, char* argv[])
{
    printf("==day 7==\n");        
    char* contents = file_get_contents("input.txt");
    str_segments_t rules = str_split(contents, "\n", 4);

    for (int i = 0; i < rules.len;i++){
        parse_rule_into_bag_map(rules.items[i]);
    }

    //open_bag(0, 2);
    // char* src = "light red";
    // char* target = "shiny gold";
    // if (open_bag(atoi(map_find(&bag_type_to_int, src)), atoi(map_find(&bag_type_to_int, target)))){
    //     printf("found\n");
    // }

    for (int i = 0; i < bag_type_to_int.len; i++){
        if (open_bag(i ,atoi(map_find(&bag_type_to_int, "shiny gold")))){
            printf("yes\n");
        } else {
            printf("no\n");
        }
    }

    deinit_str_segments(&rules);
    free(contents);
    
    return 0;
}