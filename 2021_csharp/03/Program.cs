using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _03
{
    class Program
    {
        const int NUM_BIT = 12;
        static List<string> Eliminate(List<string> canidates, List<char> mostCommon)
        {
            for (int i = 0; i < NUM_BIT; i++)
            if (canidates.Count() > 1){
                canidates = canidates.Where(e=>e[i] == mostCommon[i]).ToList();
            }
            return canidates;
        }

        static List<char> getMostCommonBits(List<string> input)
        {
            return input
                .Aggregate(new List<int>(new int[NUM_BIT]), (state,v)=>new List<int>().Concat((new List<int>(new int[NUM_BIT])).
                    Select((w,i)=> v[i] == '1' ? state[i] + 1 : state[i]-1)
                ).ToList())
                .Select(e=>e >= 0 ? '1' : '0')
                .ToList();
        }
        static void Main(string[] args)
        {
            var partOne = File.ReadAllLines("input.txt")
                .Aggregate(new List<int>(new int[NUM_BIT]), (state,v)=>new List<int>().Concat((new List<int>(new int[NUM_BIT])).
                    Select((w,i)=> v[i] == '1' ? state[i] + 1 : state[i]-1)
                ).ToList())
                .Aggregate(new List<string>(new string[2]), (state, value) => new List<string>(){
                    state[0] + (value > 0 ? "1" : "0"),
                    state[1] + (value < 0 ? "1" : "0"),
                }).Select(e=>Convert.ToInt32(e,2))
                .Aggregate((result,v)=>result*v)
            ;
            Console.WriteLine(partOne);

            // var partTwos = File.ReadAllLines("input.txt")
            //     .Aggregate(new {bit_score=new List<int>(new int[NUM_BIT]), input=new List<string>()}, 
            //         (state,v) => new {
            //             bit_score = (new List<int>(new int[NUM_BIT]).Select((w,i)=> v[i] == '1' ? state.bit_score[i] + 1 : state.bit_score[i]-1).ToList())
            //             ,input = new List<string>(state.input).Concat((new List<string>(){v})).ToList()
            //         }
            //     )
            // ;

            // part two
            var input = File.ReadAllLines("input.txt");
            var mostCommon = input
                .Aggregate(new List<int>(new int[NUM_BIT]), (state,v)=>new List<int>().Concat((new List<int>(new int[NUM_BIT])).
                    Select((w,i)=> v[i] == '1' ? state[i] + 1 : state[i]-1)
                ).ToList())
                .Select(e=>e >= 0 ? '1' : '0')
                .ToList()
            ;

            var oxygenCanidates = input.ToList();
            var scrubberCanidates = input.ToList();
            var mostCommonO = mostCommon;
            var mostCommonS = mostCommon;
            
            //This is awful, but fun
            for (int i = 0; i < NUM_BIT; i++){
                if (oxygenCanidates.Count() > 1){
                    oxygenCanidates = oxygenCanidates.Where(e=>e[i] == mostCommonO[i]).ToList();
                    mostCommonO = oxygenCanidates
                        .Aggregate(new List<int>(new int[NUM_BIT]), (state,v)=>new List<int>().Concat((new List<int>(new int[NUM_BIT])).
                            Select((w,i)=> v[i] == '1' ? state[i] + 1 : state[i]-1)
                        ).ToList())
                        .Select(e=>e >= 0 ? '1' : '0')
                        .ToList();
                }
                //TODO: clean up copy pasted stuff
                if (scrubberCanidates.Count() > 1){
                    scrubberCanidates = scrubberCanidates.Where(e=>e[i] != mostCommonS[i]).ToList();
                    mostCommonS = scrubberCanidates
                        .Aggregate(new List<int>(new int[NUM_BIT]), (state,v)=>new List<int>().Concat((new List<int>(new int[NUM_BIT])).
                            Select((w,i)=> v[i] == '1' ? state[i] + 1 : state[i]-1)
                        ).ToList())
                        .Select(e=>e >= 0 ? '1' : '0')
                        .ToList();
                }
            }
            //Console.WriteLine(scrubberCanidates[0] + "x" + oxygenCanidates[0]);
            var partTwo = Convert.ToInt32(oxygenCanidates[0],2) * Convert.ToInt32(scrubberCanidates[0],2);
            Console.WriteLine(partTwo);


        }
    }
}
