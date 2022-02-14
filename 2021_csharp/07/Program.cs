using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace _07
{
    class Program
    {
        static int PartOne()
        {
            var input = File.ReadAllText("input.txt").Split(",").Select(e=>Int32.Parse(e));
            var nSteps = new List<int>(new int[input.Max()]);
            for (int pos = 0; pos < input.Max(); pos++){
                nSteps[pos] = input.Select(e=>Math.Abs(e-pos)).Aggregate((state,v)=>state+v);
            }
            return nSteps.Min();
        }

        static int PartTwo()
        {
            var input = File.ReadAllText("input.txt").Split(",").Select(e=>Int32.Parse(e));
            var nSteps = new List<int>(new int[input.Max()]);
            for (int pos = 0; pos < input.Max(); pos++){
                nSteps[pos] = input.Select(e=>Math.Abs(e-pos)*(Math.Abs(e-pos)+1)/2).Aggregate((state,n)=>state+n);
            }
            return nSteps.Min();

            // var input = File.ReadAllText("input.txt").Split(",").Select(e=>Int32.Parse(e));
            // var lowestTotal = 999999999;
            // var lowestTotalIndex = -1;
            // for (int pos = 0; pos < input.Max(); pos++){
            //     var total = 0;
            //     foreach(var c in input){
            //         var n = Math.Abs(c-pos);
            //         total +=  n*(n+1)/2;
            //     }
            //     if (lowestTotal > total){
            //         lowestTotal = total;
            //         lowestTotalIndex = pos;

            //     }
            // }
            // return lowestTotal;
        }
        static void Main(string[] args)
        {
            var partOne = PartOne();
            Console.WriteLine($"Part one: {partOne}");
            Debug.Assert(partOne == 340052);
            
            var partTwo = PartTwo();
            Console.WriteLine($"Part two: {partTwo}");
            Debug.Assert(partTwo == 92948968);
        }
    }
}
