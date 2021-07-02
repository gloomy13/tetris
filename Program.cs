using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Gra gra=new Gra(10,15);
            gra.Play();
            

            Console.ReadLine();
        }
    }
}
