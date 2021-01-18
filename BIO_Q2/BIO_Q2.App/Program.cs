using BIO_Q2_GRID.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIO_Q2.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid sample = new Grid(4, 10, 14, 23, 47);
           
            sample.Play();
            Console.WriteLine("\n"+sample.player_1.finalNode());
            Console.ReadKey();
        }
    }
}
