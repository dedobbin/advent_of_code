using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics; 

namespace _05
{
    class Program
    {   
        static IEnumerable<Point> Covers(Point start, Point end, bool diagonal = false)
        {
            //Should clean this copy-paste party
            //Console.WriteLine($"Convers({start}, ({end})");
            if (start == end){
                return new List<Point>(){new Point(start.X, start.Y)};
            } else if (start.X == end.X){
                if (start.Y > end.Y){
                    var tmp = start;
                    start = end;
                    end = tmp;
                }
                return Enumerable.Range(start.Y, Math.Abs(end.Y-start.Y)+1).Select(y=>new Point(start.X, y));      
            } else if (start.Y == end.Y){
                if (start.X > end.X){
                    var tmp = start;
                    start = end;
                    end = tmp;
                }
                //Console.WriteLine($"{start.X} to {end.X}, {string.Join(",", Enumerable.Range(start.X, Math.Abs(end.X-start.X)+1))}");
                return Enumerable.Range(start.X, Math.Abs(end.X-start.X)+1).Select(x=>new Point(x, start.Y));   
            } else if (diagonal && (Math.Abs(start.Y - end.Y) == Math.Abs(start.X - end.X))){
                if (start.X > end.X){
                    var tmp = start;
                    start = end;
                    end = tmp;
                }
                //Console.WriteLine($"{start.X} to {end.X}, {string.Join(",", Enumerable.Range(start.X, Math.Abs(end.X-start.X)+1))}");
                return Enumerable.Range(start.X, Math.Abs(end.X-start.X)+1).Select(x=>new Point(x, start.Y < end.Y ? start.Y + (x - start.X) : start.Y - (x - start.X)));
            }
            throw new Exception($"Invalid line: {start}->{end}");
        }

        static int Solve(bool partTwo = false)
        {
            var lines = File.ReadAllLines("input.txt").Select(e=>new {start=e.Split("->")[0], end=e.Split("->")[1]})
            .Select(e=>new {
                start = new Point(Int32.Parse(e.start.Split(",")[0]), Int32.Parse(e.start.Split(",")[1])),
                end = new Point(Int32.Parse(e.end.Split(",")[0]), Int32.Parse(e.end.Split(",")[1])),
            })
            .Where(e=>partTwo || e.start.X == e.end.X || e.start.Y == e.end.Y)
            .Select(e=>Covers(e.start, e.end, partTwo).ToList())
            .ToList()
            ;

            //This is VERY slow
            var overlaps = new List<Point>();
            foreach(var line in lines){
                foreach (var line2 in lines){
                    if (line == line2) continue;
                    foreach(var co in line){
                        var dups = line2.FindAll(e=>e==co);
                        foreach (var dup in dups){
                            overlaps.Add(new Point(dup.X, dup.Y));
                        }
                    }
                }
            }
            // Console.WriteLine(string.Join(",", overlaps.Distinct()));
            // Console.WriteLine( overlaps.Distinct().Count());
            return  overlaps.Distinct().Count();

        }
        
        static void Main(string[] args)
        {
            var partOne = Solve(false);
            Debug.Assert(partOne == 6841);
            
            var partTwo = Solve(true);
            Debug.Assert(partTwo == 19258);
        }
    }
}
