using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Kwadrat : Bloczek // 0 0 0 0
    {                              // 0 1 1 0
                                   // 0 1 1 0
                                   // 0 0 0 0
        public Kwadrat()
        {
            kształtBloczka = new int[rozmiar, rozmiar];
            kształtBloczka[1, 1] = 1;
            kształtBloczka[1, 2] = 1;
            kształtBloczka[2, 1] = 1;
            kształtBloczka[2, 2] = 1;
        }
    }
}