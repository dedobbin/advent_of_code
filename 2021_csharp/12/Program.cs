using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _12
{
    class Node
    {
        public string Label;
        public List<Node> Children = new List<Node>();
    }
    class Program
    {
        public static Node ConnectNodes(List<string> input)
        {
            List<Node> nodes = new List<Node>();
            foreach(var line in input){
                var lLabel = line.Split("-")[0];
                var sNode = nodes.Find(e => e.Label == lLabel);
                if (sNode == null){
                    sNode = new Node(){Label = lLabel};
                    nodes.Add(sNode);
                }

                var rLabel = line.Split("-")[1];
                var eNode = nodes.Find(e => e.Label == rLabel);
                if (eNode == null){
                    eNode = new Node(){Label = rLabel};
                    nodes.Add(eNode);
                }

                sNode.Children.Add(eNode);
                Console.WriteLine($"Connected {sNode.Label} to {eNode.Label}");

                if (eNode.Label != "start" && sNode.Label.Length == 1 && Char.IsLower(sNode.Label[0])){
                    eNode.Children.Add(sNode);
                    Console.WriteLine($"Connected {eNode.Label} to {sNode.Label}");
                }
            }
            return nodes.Find(e=>e.Label == "start");
        }

        public static void Traverse(Node sNode, List<Tuple<Node,Node>> memory)
        {
            Console.Write(sNode.Label + " ");
            if (sNode.Label == "end"){
                Console.WriteLine("Found end");
                return;
            }
            foreach(var c in sNode.Children){
                if (memory.Where(e=>e.Item1.Label == sNode.Label && e.Item2.Label == c.Label).Count() == 0){
                    memory.Add(new Tuple<Node, Node>(sNode, c));
                    Traverse(c, memory);
                }
            }
        }

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").ToList();
            var sNode = ConnectNodes(input);
            Traverse(sNode, new List<Tuple<Node,Node>>());
            
        }
    }
}
