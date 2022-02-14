using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;

namespace _15
{
    class Node
    {
        public int Val;
    }

    class Program
    {
        public static Stack<Point> path = new Stack<Point>();
        public static void Traverse(List<List<Node>> map, int x, int y)
        {
            DumpMap(map, path);
            Console.ReadLine();
            path.Push(new Point(x,y));

            if (x == map[0].Count-1 && y ==map.Count()-1){
                Console.WriteLine($"Found THE end:{x},{y}");
                path = new Stack<Point>();
                return;
            }

            if (x + 1 < map[0].Count() && !WasVisited(path, x+1,y)){
                Traverse(map, x+1, y);
            }
            if (y + 1 < map.Count() && !WasVisited(path,x, y+1)){
                Traverse(map, x, y+1);
            }
            if (x-1 > 0 && !WasVisited(path, x-1,y)){
                Traverse(map, x-1, y);
            }
            if (x-1 > 0 && !WasVisited(path, x,y-1)){
                Traverse(map, x, y-1);
            }
        }

        public static bool WasVisited(Stack<Point> points, int x, int y)
        {
            foreach(var v in points){
                if (v.X == x && v.Y == y){
                    return true;
                }
            }
            return false;
        }
        public static void DumpMap(List<List<Node>> map, Stack<Point> path)
        {
            for (int y = 0; y < map.Count; y++){
                for(int x = 0; x < map[y].Count; x++){
                    if (WasVisited(path, x, y)){
                        Console.Write("x");
                    } else {
                        Console.Write(map[y][x].Val + "");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("================");
        }

        static void Main(string[] args)
        {
            var map = File.ReadAllLines("input.txt").Select(e=>e.Select(e=>new Node(){Val = e - '0'}).ToList()).ToList();
            Traverse(map, 0,0);
            //DumpMap(map, new Stack<Point>());
        }
    }
}
