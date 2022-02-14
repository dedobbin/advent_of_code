using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace _10
{

    class Program
    {
        static Dictionary<char,int> ScoreMap1 = new Dictionary<char, int>(){{')',3}, {']',57}, {'}',1197}, {'>', 25137},};
        static Dictionary<char,int> ScoreMap2 = new Dictionary<char, int>(){{')',1}, {']',2}, {'}',3}, {'>', 4},};
        static Dictionary<char,char> BlockBoundries = new Dictionary<char, char>(){{'(',')'}, {'[',']'}, {'{','}'}, {'<','>'},};

        public static long GetMissingScore(string input)
        {
            var stack = new Stack<char>();
            foreach(var c in input){
                if (BlockBoundries.ContainsKey(c)) {
                    stack.Push(c);
                } else if (BlockBoundries.ContainsValue(c)){
                    var opener = stack.Pop();
                } 
            }
            var score = stack.Select(v=>BlockBoundries[v]).Aggregate((long)0, (state, v)=> state*5 + ScoreMap2[v]);
            
            //Janky way to fix overflow - since list will be sorted these should clutter at the top and don't form a problem with given input
            return score < 0 ? long.MaxValue : score;
        }

        public static int GetSyntaxErrorScore(string input)
        {
            var syntaxErrorScore = 0;
            var stack = new Stack<char>();
            foreach(var c in input){
                if (BlockBoundries.ContainsKey(c)) {
                    stack.Push(c);
                } else if (BlockBoundries.ContainsValue(c)){
                    var opener = stack.Pop();
                    if (BlockBoundries[opener] != c){
                        //Console.WriteLine(line + " -- Corrupted");
                        syntaxErrorScore += ScoreMap1[c];
                        break;
                    }
                } 
            }
            return syntaxErrorScore;
        }

        static void Main(string[] args)
        {
            var partOne = File.ReadAllLines("input.txt").Aggregate(0, (state,v)=>state + GetSyntaxErrorScore(v));;
            Console.WriteLine($"Part one: {partOne}");
            Debug.Assert(partOne == 436497);

            var missingScore = File.ReadAllLines("input.txt").Where(e=>GetSyntaxErrorScore(e) == 0)
                .Select(e=>GetMissingScore(e)).OrderBy(e=>e).ToList();
            var partTwo = missingScore[missingScore.Count()/2];
            Console.WriteLine($"Part two: {partTwo}");
            Debug.Assert(partTwo == 2377613374);

        }
    }
}
