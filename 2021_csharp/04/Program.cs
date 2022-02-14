using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _04
{
    class Program
    {
        const int CARD_W = 5;
        const int CARD_H = 5;
        static bool CheckBingo(List<int> card)
        {
            int j = 0;
            //horizontal
            while(j <= CARD_H * CARD_W){
                if (card.Skip(j).Take(CARD_W).Where(e=>e==-1).Count() == 5){
                    return true;
                }
                j+= CARD_W;
            }

            j = 0;
            
            //vertical
            while (j < CARD_W){
                bool hasBingo = true;
                for (int i = 0; i < CARD_H*CARD_W;i+=CARD_W){
                    if(card[i+j] != -1){
                        hasBingo = false;
                    }
                }
                if (hasBingo){
                    return true;
                }

                j++;
            }
            return false;
        }

        static int PartOne(List<List<int>> cards, IEnumerable<int> numbers)
        {
            foreach (int n in numbers){
                for (int i = 0; i < cards.Count(); i++){
                    cards[i] = cards[i].Select(e=>e==n ? -1 : e).ToList();
                    if (CheckBingo(cards[i])){
                        var res = cards[i].Where(e=>e!=-1).Aggregate((state,v)=>state+v);
                        return res * n;
                    }
                }
            }
            return -1;
        }

        static int PartTwo(List<List<int>> cards, IEnumerable<int> numbers)
        {
            foreach (int n in numbers){
                for (int i = 0; i < cards.Count(); i++){
                    cards[i] = cards[i].Select(e=>e==n ? -1 : e).ToList();
                    if (CheckBingo(cards[i])){
                        if (cards.Count() == 1){
                            var res = cards[i].Where(e=>e!=-1).Aggregate((state,v)=>state+v);
                            return res * n;
                        }
                        cards.RemoveAt(i--);
                    } 
                }
            }
            return -1;
        }
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split("\n\n");
            var numbers = input[0].Split(",").Select(e=>Int32.Parse(e)); 
            var cards = input.Skip(1).Select(e=>e.Replace("\n", " ").Split(" ").Where(e=>e!= "").Select(e=>Int32.Parse(e)).ToList()).ToList();

            Console.WriteLine("Part one: " + PartOne(cards, numbers));
            Console.WriteLine("Part two: " + PartTwo(cards, numbers));

        }
    }
}
