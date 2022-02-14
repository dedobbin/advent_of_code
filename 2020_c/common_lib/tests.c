#include "common.h"
#include <assert.h>
#include <stdio.h>

void test_file_get_contents()
{
    char* content = file_get_contents("test_file.txt");
    int res = strcmp(content, "aa\nbb\ncc\n"); 
    free(content);
    assert(res == 0);
}

void test_str_split()
{
    char input_str[100];
    memcpy(input_str, "aaaaxbab_xabc\0", 14); //Can't use string literal
    str_segments_t res = str_split(input_str, "x", 10);

    assert(res.len == 3);

    char expected[100];
    memcpy(expected, input_str, 2);

    assert (strcmp(res.items[0], "aaaa") == 0);
    assert (strcmp(res.items[1], "bab_") == 0);
    assert (strcmp(res.items[2], "abc") == 0); 

    deinit_str_segments(&res);
}

void test_str_split_with_cap()
{
    char input_str[100];
    memcpy(input_str, "aaaa,bab_,abc\0", 35); //Can't use string literal
    str_segments_t res = str_split(input_str, ",", 2);
    
    assert(res.len == 2);
    assert (strcmp(res.items[0], "aaaa") == 0);
    assert (strcmp(res.items[1], "bab_") == 0);

    deinit_str_segments(&res);
}

void test_str_split_fail()
{
    char input_str[100];
    memcpy(input_str, "aaaa,bab_,abc\0", 35); //Can't use string literal
    str_segments_t res = str_split(input_str, "x", 2);
    
    assert(res.len == 1);
    assert (strcmp(res.items[0], "aaaa,bab_,abc") == 0);

    deinit_str_segments(&res); 
}

void test_str_split_ints()
{
    char input_str[100]; 
    memcpy(input_str, "123-0231-456000-5320\0", 21); //Can't use string literal
    str_segments_ints_t res = str_split_ints(input_str, "-", 10);
    
    assert(res.len == 4);
    assert (res.items[0] == 123);
    assert (res.items[1] == 231);
    assert (res.items[2] == 456000);
    assert (res.items[3] == 5320);

    deinit_str_segments_ints(&res);
}

void test_str_split_ints_with_cap()
{
    char input_str[100];
    memcpy(input_str, "123-0231-456000-5320\0", 21); //Can't use string literal
    str_segments_ints_t res = str_split_ints(input_str, "-", 3);
    assert(res.len == 3);

    assert (res.items[0] == 123);
    assert (res.items[1] == 231);
    assert (res.items[2] == 456000);

    deinit_str_segments_ints(&res);
}

void test_str_split_ints_fail1()
{
    char input_str[100];
    memcpy(input_str, "123-0231-456000-5320\0", 21);     //Can't use string literal
    str_segments_ints_t res = str_split_ints(input_str, "x", 10);
    
    assert(res.len == 1);
    assert (res.items[0] == 123); //hidden 'feature' because uses atoi under the hood

    deinit_str_segments_ints(&res);

}

void test_str_split_ints_fail2()
{
    //Quite unintuative behavior, but we will roll with it
    char input_str2[100];
    memcpy(input_str2, "www-123", 21);     //Can't use string literal
    str_segments_ints_t res2 = str_split_ints(input_str2, "x", 10);
    
    assert(res2.len == 1);
    assert (res2.items[0] == 0);

    deinit_str_segments_ints(&res2);

}

void test_str_split_multichartok()
{
    char input_str[100] = "aaaaxxbaaab_xxabxcxx";
    str_segments_t res = str_split_multichartok(input_str, "xx", 100);
    
    assert(res.len == 3);
    assert(strcmp(res.items[0],"aaaa") == 0);
    assert(strcmp(res.items[1],"baaab_") == 0);
    assert(strcmp(res.items[2],"abxc") == 0);
    
    deinit_str_segments(&res);
}


void test_str_split_multichartok_with_cap()
{
    char input_str[100] = "aaaaxxbaaab_xxabxcxx";
    str_segments_t res = str_split_multichartok(input_str, "xx", 2);
    
    assert(res.len == 2);
    assert(strcmp(res.items[0],"aaaa") == 0);
    assert(strcmp(res.items[1],"baaab_") == 0);
    
    deinit_str_segments(&res);
}

void test_str_split_multichartok_with_tail()
{
    char input_str[100] = "aaaaxwbaaab_xwabxc";
    str_segments_t res = str_split_multichartok(input_str, "xw", 100);
    
    assert(res.len == 3);
    assert(strcmp(res.items[0],"aaaa") == 0);
    assert(strcmp(res.items[1],"baaab_") == 0);
    assert(strcmp(res.items[2],"abxc") == 0);
    
    deinit_str_segments(&res);
}

void test_str_split_multichartok_with_tail_and_cap()
{
    char input_str[100] = "qqwxxbaa4ab_xxabxc\0";
    str_segments_t res = str_split_multichartok(input_str, "xx", 2);
    
    assert(res.len == 2);
    assert(strcmp(res.items[0],"qqw") == 0);
    assert(strcmp(res.items[1],"baa4ab_") == 0);
    
    deinit_str_segments(&res);
}

void test_str_split_multichartok_fail()
{
    char input_str[100] = "abc\0";
    str_segments_t res = str_split_multichartok(input_str, "w", 2);
    
    assert(res.len == 1);
    assert(strcmp(res.items[0],"abc") == 0);

    deinit_str_segments(&res);
}

void test_map()
{
    map_t map = {.len=0};

    map_insert(&map, "aaa", "value1");
    map_insert(&map, "bbb", "value2");

    assert(map.len == 2);
    assert(strcmp(map_find(&map, "aaa"), "value1") == 0);
    assert(strcmp(map_find(&map, "bbb"), "value2") == 0);
    assert(map_find(&map, "sdf") == NULL);
}

void test_arr_map_str_to_int()
{
    char* input[] = {"123", "34", "9999", "-673"};
    int* res = arr_map_str_to_int(input, 4, NULL);
    
    assert(res[0] == 123);
    assert(res[1] == 34);
    assert(res[2] == 9999);
    assert(res[3] == -673);
    
    free(res);
}

int arr_map_str_to_int_callback(char* input, char** total, int total_len)
{
    return atoi(input) * atoi(total[0]);
}

void test_arr_map_str_to_int_with_callback()
{
    char* input[] = {"2", "10", "24", "-24"};
    int* res = arr_map_str_to_int(input, 4, arr_map_str_to_int_callback);
    
    assert(res[0] == 4);
    assert(res[1] == 20);
    assert(res[2] == 48);
    assert(res[3] == -48);
    
    free(res);
}

void test_regex_groups()
{
    char * source = "ab13bc2de";
    char * regex_str = "([a-z]*)([0-9]*)([a-z0-9]*)";
    
    size_t max_groups = 20;
    str_segments_t* result = regex_groups(source, regex_str, max_groups);

    assert(result->len == 4); //Amount of groups based on regex, not amount found
    assert(strcmp(result->items[0], "ab13bc2de") == 0);
    assert(strcmp(result->items[1],"ab") == 0);
    assert(strcmp(result->items[2],"13") == 0);
    assert(strcmp(result->items[3],"bc2de") == 0);

    deinit_str_segments(result);
    free(result);
}

void test_regex_groups_with_cap()
{
    char * source = "ab13bc2de";
    char * regex_str = "([a-z]*)([0-9]*)([a-z0-9]*)";
    
    size_t max_groups = 2;
    str_segments_t* result = regex_groups(source, regex_str, max_groups);
    assert(result->len == 2); 
    assert(strcmp(result->items[0], "ab13bc2de") == 0);
    assert(strcmp(result->items[1],"ab") == 0);

    deinit_str_segments(result);
    free(result);
}

void test_regex_groups_fail()
{
    char * source = "_____";
    char * regex_str = "([a-z]*)([0-9]*)([a-z0-9]*)";
    
    size_t max_groups = 20;
    str_segments_t* result = regex_groups(source, regex_str, max_groups);

    assert(result->len == 4); //Amount of groups based on regex, not amount found
    assert(strcmp(result->items[0], "") == 0);
    assert(strcmp(result->items[1], "") == 0);
    assert(strcmp(result->items[2], "") == 0);
    assert(strcmp(result->items[3], "") == 0);

    deinit_str_segments(result);
    free(result);
}

int main (int argc, char* argv[])
{
    test_file_get_contents();

    test_str_split();
    test_str_split_with_cap();
    test_str_split_fail();
    
    test_str_split_ints();
    test_str_split_ints_with_cap();
    test_str_split_ints_fail1();
    test_str_split_ints_fail2();
    
    test_str_split_multichartok();
    test_str_split_multichartok_with_cap();
    test_str_split_multichartok_with_tail();
    test_str_split_multichartok_with_tail_and_cap();
    test_str_split_multichartok_fail();

    test_map();

    test_arr_map_str_to_int();
    test_arr_map_str_to_int_with_callback();

    test_regex_groups();
    test_regex_groups_with_cap();
    test_regex_groups_fail();

    return 0;
}