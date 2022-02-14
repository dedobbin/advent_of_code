using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace _16
{
    enum PacketType
    {
        LITERAL = 4,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Select(v=>Convert.ToByte(v+"", 16)).ToList();
            //Console.WriteLine(string.Join(",",input));

            //Packet version:
            var packetVersion = input[0] >> 1;
            Console.WriteLine($"Packet Version: {packetVersion} - {Convert.ToString(packetVersion, 2).PadLeft(8, '0')}");

            //Packet type
            var t1 = (input[0] & 0b0001) << 2;
            var t2 = input[1] >> 3;
            var packetType = t1 | t2;
            Console.WriteLine($"Packet type: {packetType} - {Convert.ToString(packetType, 2).PadLeft(8, '0')}");

            if (packetType == (int)PacketType.LITERAL){
                Console.WriteLine($"Literal");
                var stopIndicator = 1;
                var i = 2;
                while (stopIndicator != 0){
                    stopIndicator = (input[i] >> 3) & 1;
                    var val = input[i+1];
                    Console.WriteLine($"{Convert.ToString(val, 2).PadLeft(8, '0')}");
                    i++;
                }




                
            }





        }
    }
}
