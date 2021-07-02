using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Gra
    {
        int cols=10, rows=15;
        public Plansza pl;
        public Bloczek b;
        Random r = new Random();
        List<Bloczek> wszystkieBloczki;
        private int punkty=0;
        IEnumerable<Bloczek> aux;
        public Gra(int c,int r){
            cols = c;
            rows = r;
            pl = new Plansza(cols, rows);
            aux = typeof(Bloczek).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Bloczek))).Select(t => (Bloczek)Activator.CreateInstance(t));
            NowyBloczek();
        }
        private void KoniecGry() {
            Console.Clear();
            Console.WriteLine("KONIEC GRY!");
            Console.WriteLine("WYNIK: "+punkty);
        }
        private bool SprawdźCzyKoniec() {
            if (b.PosY == 0) return true;
            else return false;
        }
        private void DrukPts() { Console.WriteLine("PUNKTY: " + punkty); }
        public void NowyBloczek()
        {
            wszystkieBloczki = aux.Cast<Bloczek>().ToList();
            b = wszystkieBloczki[r.Next(wszystkieBloczki.Count)];
            b.PozycjaStartowa(r.Next(cols - b.Sz));
        } 

        public void Play()
        {
            
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            DrukPts();
            pl.Druk();
            while (true)
            {
                //--------------------------------controls
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo k = Console.ReadKey(true);
                    switch (k.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (pl.KolizjaPrawo(b) == false)
                                b.MoveRight();
                            break;
                        case ConsoleKey.LeftArrow:
                            if (pl.KolizjaLewo(b) == false)
                                b.MoveLeft();
                            break;
                        case ConsoleKey.UpArrow:
                            if(pl.KolizjaObrót(b,true)==false)
                                b.Obrót(true);
                            break;
                        case ConsoleKey.DownArrow:
                            if (pl.KolizjaDół(b) == false)
                                b.MoveDown();
                            break;
                    }
                }
                //----------------------------------------
                if (stopWatch.ElapsedMilliseconds > 1000)
                {
                    stopWatch.Restart();
                    pl.PłytkieCzyszczenie();
                    if (pl.KolizjaDół(b) == false)
                    {
                        b.MoveDown();
                        pl.DodajBloczek(b);
                    }
                    else
                    {
                        if (SprawdźCzyKoniec())
                        {
                            KoniecGry();
                            break;
                        }
                        b.ZamróźBloczek();
                        pl.DodajBloczek(b);
                        NowyBloczek();
                        int ilośćLinii = pl.Linia();
                        switch (ilośćLinii)
                        {
                            case 1:
                                punkty += 5;
                                break;
                            case 2:
                                punkty += 15;
                                break;
                            case 3:
                                punkty += 45;
                                break;
                            case 4:
                                punkty += 75;
                                break;
                        }
                    }
                    Console.Clear();
                    DrukPts();
                    pl.Druk();
                }
            }
        }
    }
}
