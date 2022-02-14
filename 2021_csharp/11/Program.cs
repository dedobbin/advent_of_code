using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _11
{
    class Program
    {
        public static void DumpStatus(List<List<int>> octopi)
        {
            foreach(var line in octopi){
                foreach(var o in line){
                    if (o>9){
                        Console.Write("X ");
                    } else {
                        Console.Write(o + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static List<List<int>> Increase(int x, int y, List<List<int>> octopi)
        {
            if (y < 0 || y > octopi.Count()-1 || x < 0 || x> octopi[y].Count()-1){
                return octopi;
            }
            octopi[y][x]++;
            if (octopi[y][x] == 10){
                octopi = IncreaseNeighbors(x, y, octopi);
            }
            return octopi;
        }

        public static List<List<int>> IncreaseNeighbors(int x, int y, List<List<int>> octopi)
        {
            octopi = Increase(x+1, y, octopi);
            octopi = Increase(x, y+1, octopi);
            octopi = Increase(x-1, y, octopi);
            octopi = Increase(x, y-1, octopi);

            octopi = Increase(x-1, y-1, octopi);
            octopi = Increase(x+1, y-1, octopi);
            octopi = Increase(x+1, y+1, octopi);
            octopi = Increase(x-1, y+1, octopi);

            return octopi;
        }

        static int PartOne()
        {   
            var octopi = File.ReadAllLines("input.txt").Select(e=>System.Text.Encoding.ASCII.GetBytes(e).Select(e=>e-'0').ToList()).ToList();
            const int N_STEPS = 100;
            var nFlashes = 0;
            for (var step = 0; step < N_STEPS; step++){
                for (var y = 0; y < octopi.Count(); y++){
                    for (var x = 0; x < octopi[y].Count(); x++){
                        octopi = Increase(x,y, octopi);
                    }
                }
                nFlashes += octopi.Aggregate(0,(state, v)=>v.Where(v=>v>9).Count() + state);
                octopi = octopi.Select(e=> e.Select(e=>e > 9 ? 0 : e).ToList()).ToList();
            }
            return nFlashes;
        }

        static int PartTwo()
        {   
            var octopi = File.ReadAllLines("input.txt").Select(e=>System.Text.Encoding.ASCII.GetBytes(e).Select(e=>e-'0').ToList()).ToList();
            const int MAX_STEPS = 1000;
            var step =0;
            for (step = 0; step < MAX_STEPS; step++){
                for (var y = 0; y < octopi.Count(); y++){
                    for (var x = 0; x < octopi[y].Count(); x++){
                        octopi = Increase(x,y, octopi);
                    }
                }
                var nFlashes = octopi.Aggregate(0,(state, v)=>v.Where(v=>v>9).Count() + state);
                if (nFlashes == octopi.Count() * octopi[0].Count()){
                    break;
                }
                octopi = octopi.Select(e=> e.Select(e=>e > 9 ? 0 : e).ToList()).ToList();
            }

            if (step == MAX_STEPS){
                throw new Exception("Part two: MAX_STEPS too low to reach first full blink");
            }
            return step+1;
        }


        static void Main(string[] args)
        {            
            var partOne = PartOne();
            Console.WriteLine($"Part one: {partOne}");
            Debug.Assert(partOne == 1719);

            var partTwo = PartTwo();
            Console.WriteLine($"Part two: {partTwo}");
            Debug.Assert(partTwo == 232);


        }
    }
}
