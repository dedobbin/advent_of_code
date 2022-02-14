using System;
using System.IO;
using System.Linq;

namespace _01
{
    class Program
    {
       static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(v => Int32.Parse(v)).ToArray();
            
            var partOne = input
                .Select((v,i)=>new {value=v, higher = i == 0 ? 0 : v > input[i-1] ? 1 : -1 })
                .Count(e => e.higher == 1)
            ;
            Console.WriteLine(partOne);

            var partTwo = input
                .Select((v,i) => new {value = v, viable = i + 3 < input.Length})
                .Where(v => v.viable)
                .Select((v,i) => new {
                    value = v.value, 
                    sum = input[i] + input[i+1] + input[i+2] < input[i+1] + input[i+2] + input[i+3] 
                }).Count(e=>e.sum)
            ;

            Console.WriteLine(partTwo);
        }
    }
}
