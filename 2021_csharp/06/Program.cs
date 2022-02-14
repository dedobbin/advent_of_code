using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _06
{
    class Program
    {
        static int PartOne(bool print = false)
        {
            var input = File.ReadAllText("input.txt").Split(",").Select(e=>Int32.Parse(e)).ToList();
            const int nDays = 80;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for(int i = 0;i < nDays;i++){
                //Console.WriteLine("day " + i);
                int nBorn = 0;
                for(int j = 0;j < input.Count();j++){
                    if (input[j]-- == 0){
                        //Console.WriteLine("born");
                        input[j] = 6;
                        nBorn++;
                    }
                }
                for (int j = 0;j < nBorn; j++){
                    input.Add(8);
                }
                //Console.WriteLine(string.Join(",", input));
            }

            watch.Stop();
            if (print){
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                Console.WriteLine("part one: " + input.Count());
            }
            return input.Count();
        }
        static long PartTwo(bool print = false)
        {
            const int N_DAYS = 256;
            var input = File.ReadAllText("input.txt").Split(",").Select(e=>byte.Parse(e)).ToList();

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            var fish = new List<long>(new long[9]);
            foreach (var i in input){
                fish[i] ++ ;
            }

            for (int day = 0; day < N_DAYS; day++){
                // Console.WriteLine($"day {day}");
                // for (int i = 0; i < 9; i++){
                //     Console.WriteLine($"{fish[i]}");
                // }
                // Console.WriteLine();

                var nFish0 = fish[0];
                for (int j = 0; j < 8; j++){
                    fish[j] = fish[j+1];
                }
                
                fish[6] += nFish0;
                fish[8] = nFish0;
            }

            var res = fish.Aggregate((state,v)=>state+v);

            watch.Stop();
            if (print){
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                Console.WriteLine($"part two: {res}");
            }
            return res;
        }
        static void Main(string[] args)
        {
            PartOne(true);
            PartTwo(true);
            
        }
    }


}
