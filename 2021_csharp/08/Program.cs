using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _08
{
    class Program
    {
        static int ToDigitSimple(string input)
        {
            //Console.WriteLine($"ToDigitSimple({input})");
            
            switch(input.Length){
                case 2:
                    return 1;
                case 4:
                    return 4;
                case 3:
                    return 7;
                case 7:
                    return 8;
                default:
                    break;
                
            }
            return -1;
        }

        static int[] ToPossibleDigits(string input)
        {
            switch(input.Length){
                case 2:
                    return new int[]{1};
                case 3:
                    return new int[]{7};
                case 4:
                    return new int[]{4};
                case 5:
                    return new int[]{2,3,5};
                case 6:
                    return new int[]{0,6,9};
                case 7:
                    return new int[]{8};
                default:
                    return new int[]{};
            }
        }

        // static int ToDigit(string input, Dictionary<char, char> mapping)
        // {

        // }

        static public int PartOneInline()
        {
            var partOne = File.ReadAllLines("input.txt").Select(e=>e.Split("|").Select(e=>e.Trim().Split(" ")).Skip(1).First()
                .Where(e=>ToDigitSimple(e)!=-1).Count()).Aggregate((state,v)=>state+v);
            
            //Console.WriteLine($"nSimple: {partOne}");
            return partOne;
        }
        static public int PartOneSimple()
        {
            var input = File.ReadAllLines("input.txt").Select(e=>e.Split("|").Select(e=>e.Split(" ").Select(e=>e.Trim())).ToList());
            int nSimple = 0;
            foreach (var i in input){
                nSimple += i[1].Where(e=>ToDigitSimple(e) != -1).Count();
            }
            //Console.WriteLine($"nSimple: {nSimple}");
            return nSimple;
        }

        static void PartTwo()
        {
            var input = File.ReadAllLines("input.txt").Select(e=>e.Split("|").Select(e=>e.Trim().Split(" ")).ToList()).ToList();

            var possibleMappings = new List<Dictionary<char,char>>();
            for (char s = 'a'; s < 'g'; s++){
                for (char d = 'a'; d < 'g'; d++){
                    
                }
            }

            foreach(var p in possibleMappings){
                foreach(var e in p){
                    Console.WriteLine($"{e.Key}: {e.Value}");
                }
                Console.WriteLine();
            }
        }
 
        static void Main(string[] args)
        {
            //294
            // var watch = System.Diagnostics.Stopwatch.StartNew();
            // PartOneSimple();
            // watch.Stop();
            // Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
            
            // watch = System.Diagnostics.Stopwatch.StartNew();
            // PartOneInline();
            // watch.Stop();
            // Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

            PartTwo();
        }
    }
}
