using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace _14
{
    class Program
    {
        public static int PartOne()
        {
            string template = File.ReadLines("input.txt").First();
            var rules = File.ReadAllLines("input.txt").Skip(2).Select(e=>e.Split("->").Select(e=>e.Trim()))
                .Select(e=>new {input=e.First(), inbetween=e.Skip(1).First()}).ToList();
            var cur = template;

            const int N_STEPS = 10;
            for (int step = 0; step < N_STEPS; step++){
                //Console.WriteLine($"step: {step}");
                var inserts = new List<dynamic>();
                foreach(var r in rules){
                    for (var i = 0; i < cur.Length-1; i++){
                        var sub = cur.Substring(i,2);
                        if (sub == r.input){
                            inserts.Add(new{i=i,val=r.inbetween});
                        }
                        
                    }
                }

                var nInserts = 0;
                inserts.Sort((a,b)=>a.i - b.i);
                foreach (var insert in inserts){
                    cur = cur.Insert(insert.i+1 + nInserts, insert.val);
                    nInserts ++;
                }
            }

            var h=0;
            var l=-1;
            foreach(var c in cur){
                var n = cur.Count(f => f == c);
                if (n > h){
                    h = n;
                } 
                if (n < l || l == -1){
                    l = n;
                }
            }
            Console.WriteLine($"counts: {h}-{l}={h-l}");
            return h-l;
        }

        static void PartOneAlt()
        {
            var template = File.ReadLines("input.txt").First().Select(e=>new Node(){Val=e}).ToList();
            var rules = File.ReadAllLines("input.txt").Skip(2).Select(e=>e.Split("->").Select(e=>e.Trim()))
                .Select(e=>new {input=e.First(), inbetween=e.Skip(1).First()[0]}).ToList();
            var cur = template;

            const int N_STEPS = 10;
            for (int step = 0; step < N_STEPS; step++){
                //Console.WriteLine($"step: {step}");

                foreach(var r in rules){
                    for (var i = 0; i < cur.Count()-1; i++){
                        if ("" + cur[i].Val + cur[i+1].Val == r.input){
                            cur[i].Child = new Node(){Val=r.inbetween};
                        }
                        
                    }
                }

                var output = new List<Node>();
                foreach(var c in cur){
                    output.Add(c);
                    if (c.Child != null){
                        output.Add(c.Child);
                        c.Child = null;
                    }
                }
                cur = output;

                //Console.WriteLine(cur.Count());
                // foreach(var c in cur){
                //     Console.Write(c.Val);
                // }
                // Console.WriteLine();
            }

            var h=0;
            var l=-1;
            foreach(var c in cur){
                var n = cur.Count(f => f.Val == c.Val);
                if (n > h){
                    h = n;
                } 
                if (n < l || l == -1){
                    l = n;
                }
            }
            Console.WriteLine($"counts: {h}-{l}={h-l}");

        }

        static void PartTwo()
        {
            var template = File.ReadLines("input.txt").First();
            var rules = File.ReadAllLines("input.txt").Skip(2);

            //using dictionary instead of list isn't too useful here...oh well
            var ruleMap = new Dictionary<string, char>();
            foreach(var rule in rules){
                var segments = rule.Split("->").Select(e=>e.Trim());
                ruleMap.Add(segments.First(), segments.Skip(1).First()[0]);
            }

            //initial setting
            var charMap = new Dictionary<char, List<int>>();
            for (char c = 'A'; c <= 'Z'; c++){
                charMap.Add(c, new List<int>());

                var indexOf = template.IndexOf(c);
                while(indexOf >= 0){
                    charMap[c].Add(indexOf);
                    indexOf = template.IndexOf(c, indexOf + 1);
                }
            }

            // foreach(var c in charMap){
            //     if (c.Value.Count() > 0){
            //         Console.WriteLine($"{c.Key} occurs {c.Value.Count()} times:  {string.Join(",", c.Value)}");
            //     }
            // }

            const int N_STEPS = 11;
            for (int step = 0; step < N_STEPS; step++){
                var newMap = charMap.ToDictionary(x => x.Key, x => x.Value.ToList());
                Console.WriteLine($"step {step+1}");
                foreach(var rule in ruleMap){
                    var c1 = charMap[rule.Key[0]];
                    var c2 = charMap[rule.Key[1]];

                    for(var i = 0; i < c1.Count(); i++){
                        int c = c1[i];
                        var indexOf = c2.IndexOf(c+1);
                        if (indexOf >= 0){
                            var insertPoint = c2[indexOf];
                            //Console.WriteLine($"Found match for {rule.Key} - inserting {rule.Value} at {insertPoint}");
                            foreach(var v in newMap){
                                for (int j = 0; j < v.Value.Count(); j++){
                                    if (v.Value[j] >= insertPoint){
                                        v.Value[j] += 1;
                                    }
                                }
                            }
                            newMap[rule.Value].Add(insertPoint);
                            //Todo: could update search to binary search if sort list
                            //newMap[rule.Value].Sort();
                        }
                    }
                }
                charMap = newMap.ToDictionary(x => x.Key, x => x.Value.ToList());
                var count = charMap.Aggregate(0, (state,v)=>state + v.Value.Count());
                Console.WriteLine($"Length after {step + 1} steps: {count}");

                // foreach(var i in charMap){
                //     if (i.Value.Count() > 0){
                //         Console.WriteLine($"{i.Key}: {string.Join(",", i.Value)}");
                //     }
                // }

            }
            foreach(var c in charMap){
                if (c.Value.Count() > 0){
                    Console.WriteLine($"{c.Key} occurs {c.Value.Count()} times");
                }
            }
            

            //Console.WriteLine($"{hi}-{lo}={hi-lo}");

        }

        static void Main(string[] args)
        {
            //var partOne = PartOne();
            // Console.WriteLine($"Part one {partOne}");
            // Debug.Assert(partOne == 2937);
            PartTwo();
        }

        class Node
        {
            public char Val;
            public Node Child = null;
        }
    }
}
