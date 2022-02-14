using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace _09
{
    class Basin
    {
        public int size = 0;
        public int val = -1;
    }
    class Coordinate
    {
        public int x;
        public int y;
        public int v;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").Select((e,y)=>e.ToCharArray().Select((e,x)=>new Coordinate(){v=e-'0', x=x, y=y}).ToList()).ToList();
          
            var partOne = lines.Select(e=>e.Select(e=>GetDangerLevel(e.x, e.y, lines)).Aggregate((state,v)=>state + v)).Aggregate((state,v)=>state + v);
            Console.WriteLine($"Part one: {partOne}");
            Debug.Assert(partOne == 532);
             
            var partTwo = PartTwo(lines);
            Console.WriteLine($"Part two: {partTwo}");
            Debug.Assert(partTwo == 1110780);
        }

        static int PartTwo(List<List<Coordinate>> lines)
        {
            var lowestPoints =
            from inner in lines.Select(e=>e.Where(e=>GetDangerLevel(e.x, e.y, lines) > 0))
            from value in inner
            select value;

            var bMap = new int[lines.Count(), lines[0].Count()];

            var curBn = 1;
            var bs = new List<Basin>();
            foreach (var lp in lowestPoints){
                var b = new Basin(){val=curBn++};
                bMap = Flud(lp.x, lp.y, bMap, lines, b);
                bs.Add(b);
                
            }
            return bs.OrderBy(v=>v.size).Reverse().Take(3).Aggregate(1, (state,v)=>state*(v.size));
        }


        public static int GetDangerLevel(int x, int y, List<List<Coordinate>> map)
        {
            //Console.WriteLine($"Checking {map[y][x]} at {x},{y}");
            if (x > 0 && map[y][x].v >= map[y][x-1].v){
                return 0;
            } else if (x < map[0].Count()-1 && map[y][x].v >= map[y][x+1].v){
                return 0;
            } else if (y > 0 && map[y][x].v >= map[y-1][x].v){
                return 0;
            } else if (y < map.Count()-1 && map[y][x].v >= map[y+1][x].v){
                return 0;
            }

            return 1+map[y][x].v;
        }

        public static int[,] Flud(int x, int y, int[,] bMap, List<List<Coordinate>> map, Basin b)
        {
            if (bMap[y,x] != 0) return bMap;

            bMap[y,x] = b.val;
            b.size ++;
            if (CanFlud(x-1, y, map)){
                bMap = Flud(x-1,y, bMap, map, b);
            } 
            if (CanFlud(x+1, y, map)){
                bMap = Flud(x+1,y, bMap, map, b);
            } 
            if (CanFlud(x, y-1, map)){
                bMap = Flud(x, y-1,bMap, map, b);
            } 
            if (CanFlud(x, y+1, map)){
                bMap = Flud(x, y+1,bMap, map, b);
            }
            return bMap;
        }

        public static bool CanFlud(int x, int y, List<List<Coordinate>> map)
        {
            if (y < map.Count() && y >= 0){
                if (x < map[y].Count() && x >= 0){
                    return map[y][x].v!=9;
                }
            }
            return false;
        }

        public static void DumpBasins(int[,] input)
        {
            for (int y = 0; y < input.GetLength(0); y++){
                for (int x = 0; x < input.GetLength(1); x++){
                    Console.Write($"{input[y,x]}");
                }
                Console.WriteLine();
            }
        }
    }
}
