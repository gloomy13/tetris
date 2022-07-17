using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Kreska : Bloczek  // 0 0 0 0
    {                               // 1 1 1 1
                                    // 0 0 0 0
                                    // 0 0 0 0
        public Kreska()
        {
            kształtBloczka = new int[rozmiar, rozmiar];
            for (int i = 0; i < rozmiar; i++)
                kształtBloczka[i, 1] = 1;
        }
    }
}