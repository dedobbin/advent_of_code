using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            // var one = new List<dynamic>(){
            //     new {x=1,y=2},
            //     new {x=2,y=4},
            //     new {x=3,y=5},
            // };
            // var l = new List<dynamic>(one).Concat(new List<dynamic>(){
            //     new {x=9,y=9},
            // }).ToList();
            
            // Console.WriteLine(l[3].x);



            var partOne = File.ReadAllLines("input.txt").ToArray()
                .Select(v => new {command=v.Split(" ")[0], arg=Int32.Parse(v.Split(" ")[1])})
                .Select(v => new {
                    x=v.command =="forward" ? v.arg : (v.command=="backward" ? -v.arg : 0),
                    y=v.command =="down" ? v.arg : (v.command=="up" ? -v.arg : 0) 

                })
                .Aggregate(new {x=0,y=0,res=0}, (state, next) => new {x=state.x+next.x ,y=state.y+next.y, res= (state.x+next.x) * (state.y+next.y)})
                .res
            ;
            Console.WriteLine(partOne);

            var partTwo = File.ReadAllLines("input.txt")
                .Select((v,i) => new {index=i, command=v.Split(" ")[0], arg=Int32.Parse(v.Split(" ")[1])})
                .Aggregate(new List<dynamic>(), (state, v) =>new List<dynamic>(state).Concat(new List<dynamic>(){ new {
                    increase_aim = v.command == "down" ? v.arg : (v.command=="up" ? -v.arg : 0),
                    increase_x = v.command == "forward" ? v.arg : 0,
                    index = v.index,
                    command = v.command,
                    arg = v.arg,
                }}).ToList())
                .Aggregate(new List<dynamic>(), (state, v) =>new List<dynamic>(state).Concat(new List<dynamic>(){ new {
                    x = (v.index > 0 ? state[v.index-1].x : 0) + v.increase_x,
                    aim = (v.index > 0 ? state[v.index-1].aim : 0) + v.increase_aim,
                    command = v.command,
                    arg = v.arg,
                    index = v.index
                }}).ToList())
                .Aggregate(new List<dynamic>(), (state, v) =>new List<dynamic>(state).Concat(new List<dynamic>(){ new {
                    x = v.x,
                    aim = v.aim,
                    increase_y = v.command == "forward" ? v.aim * v.arg : 0,
                    index = v.index
                }}).ToList())
                .Aggregate(new List<dynamic>(), (state, v) =>new List<dynamic>(state).Concat(new List<dynamic>(){ new {
                    x = v.x,
                    aim = v.aim,
                    y = (v.index > 0 ? state[v.index-1].y : 0) + v.increase_y,
                }}).ToList())
                .Select(v=>v.x * v.y)
                .Last()
            ;
            Console.WriteLine(partTwo);
        }
    }
}
