#include <stdio.h>
#include <assert.h>
#include "../common_lib/common.h"

typedef struct flat_map_t
{
    int w,h;
    char** data;
} flat_map_t;

void print_map(flat_map_t map)
{
    for (int y = 0; y < map.h; y++){
        for (int x = 0; x < map.w; x++){
            printf("%c ", map.data[y][x]);
        }
        printf("\n");
    }
}

char get_from_map(flat_map_t map, int x, int y)
{
    return map.data[y][x];
}

char place_on_map(flat_map_t map, int x, int y, char c)
{
    map.data[y][x] = c;
}

int traverse(flat_map_t map, int step_w, int step_h)
{
    int y = 0, x = 0, n_trees = 0;
    while (1)
    {
        x+=step_w;
        y+=step_h;

        if (y >= map.h){
            break;
        }


        if (x >= map.w){
            x -= map.w;
        }

        char c = get_from_map(map, x, y);
        // printf("%c\n", c);
        if (c == '#'){ 
            n_trees++;
        } 

        // printf("---------------------------------\n");
        // print_map(map);
        // getchar();
    }

    return n_trees;
}

int main (int argc, char* argv[])
{
    printf("==day 3==\n");    
    char* content = file_get_contents("input.txt");

    str_segments_t lines = str_split(content, "\n", 2000);

    flat_map_t map = {.w=strlen(lines.items[0]), .h=lines.len, .data=lines.items};
    free(content);
    
    //part one
    assert(traverse(map, 3, 1) == 156);

    //part two
    unsigned part_two_solution = traverse(map,1,1) * traverse(map,3,1) * traverse(map,5,1) * traverse(map,7,1) * traverse(map,1,2);
    assert(part_two_solution == 3521829480);
    
    deinit_str_segments(&lines);
    return 0;
}