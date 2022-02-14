using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;

namespace _13
{
    class Instruction
    {
        public char Dir;
        public int N;
    }
    
    class Program
    {
        public static void Dump(int w, int h, List<Point> points)
        {
            for (int y = 0; y < h ;y++){
                for (int x = 0; x < w ; x++){
                    if (points.Where(e=>e.X == x && e.Y == y).Count() > 0){
                        Console.Write("# ");
                    } else {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            var instructions = File.ReadAllText("input.txt").Split("\n\n").Skip(1).First().Split("\n").
                Select(e=>new Instruction(){Dir=e.Split("=")[0].Last(), N=Int32.Parse(e.Split("=")[1])});

            var points = File.ReadAllText("input.txt").Split("\n\n").First().Split("\n").
                Select(e=>new Point(Int32.Parse(e.Split(",")[0]), Int32.Parse(e.Split(",")[1]))).ToList();

            var w = points.Aggregate(0, (state, v)=>state > v.X ? state :v.X ) +1;
            var h = points.Aggregate(0, (state, v)=>state > v.Y ? state :v.Y ) + 1;
            
            bool partOneDone = false;
            foreach(var i in instructions){
                if (i.Dir == 'y'){
                    points = points.Select(e=>e.Y > i.N ? new Point(e.X, e.Y - (e.Y-i.N) * 2) : e).ToList();
                    h /= 2;
                } else if (i.Dir == 'x'){
                    points = points.Select(e=>e.X > i.N ? new Point(e.X - (e.X-i.N) * 2, e.Y) : e).ToList();
                    w /= 2;
                }
                points = points.Distinct().ToList();
                if (!partOneDone){
                    var partOne = points.Count();
                    Console.WriteLine($"Part one: {partOne}");
                    Debug.Assert(partOne == 693);
                    partOneDone = true;

                }
            }
            

            Console.WriteLine("Part two:");
            Dump(w, h, points);
        }
    }
}
