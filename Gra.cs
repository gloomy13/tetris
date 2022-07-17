using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Gra
    {
        public Plansza Plansza { get; set; }
        public Bloczek Bloczek { get; set; }
        public int Cols { get; } = 10;
        public int Rows { get; } = 15;
        public Random Random { get; set; } = new Random();
        public List<Bloczek> WszystkieBloczki { get; set; }
        public int Punkty { get; set; } = 0;
        public IEnumerable<Bloczek> Aux { get; set; }

        public Gra(int cols, int rows)
        {
            Cols = cols;
            Rows = rows;
            Plansza = new Plansza(Cols, Rows);
            Aux = typeof(Bloczek).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Bloczek))).Select(t => (Bloczek)Activator.CreateInstance(t));
            NowyBloczek();
        }

        private void KoniecGry()
        {
            Console.Clear();
            Console.WriteLine("KONIEC GRY!");
            Console.WriteLine("WYNIK: " + Punkty);
        }

        private bool SprawdźCzyKoniec()
        {
            if (Bloczek.PosY == 0) return true;
            else return false;
        }

        private void DrukPts()
        { Console.WriteLine("PUNKTY: " + Punkty); }

        public void NowyBloczek()
        {
            WszystkieBloczki = Aux.Cast<Bloczek>().ToList();
            Bloczek = WszystkieBloczki[Random.Next(WszystkieBloczki.Count)];
            Bloczek.PozycjaStartowa(Random.Next(Cols - Bloczek.Szerokość));
        }

        public void Play()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            DrukPts();
            Plansza.Druk();
            while (true)
            {
                //--------------------------------controls
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo k = Console.ReadKey(true);
                    switch (k.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (!Plansza.KolizjaPrawo(Bloczek))
                                Bloczek.MoveRight();
                            break;

                        case ConsoleKey.LeftArrow:
                            if (!Plansza.KolizjaLewo(Bloczek))
                                Bloczek.MoveLeft();
                            break;

                        case ConsoleKey.UpArrow:
                            Plansza.KolizjaObrót(Bloczek);
                            break;

                        case ConsoleKey.DownArrow:
                            if (!Plansza.KolizjaDół(Bloczek))
                                Bloczek.MoveDown();
                            break;
                    }
                }
                //----------------------------------------
                if (stopWatch.ElapsedMilliseconds > 1000)
                {
                    stopWatch.Restart();
                    Plansza.PłytkieCzyszczenie();
                    if (!Plansza.KolizjaDół(Bloczek))
                    {
                        Bloczek.MoveDown();
                        Plansza.DodajBloczek(Bloczek);
                    }
                    else
                    {
                        if (SprawdźCzyKoniec())
                        {
                            KoniecGry();
                            break;
                        }
                        Bloczek.ZamróźBloczek();
                        Plansza.DodajBloczek(Bloczek);
                        NowyBloczek();
                        int ilośćLinii = Plansza.Linia();
                        switch (ilośćLinii)
                        {
                            case 1:
                                Punkty += 5;
                                break;

                            case 2:
                                Punkty += 15;
                                break;

                            case 3:
                                Punkty += 45;
                                break;

                            case 4:
                                Punkty += 75;
                                break;
                        }
                    }
                    Console.Clear();
                    DrukPts();
                    Plansza.Druk();
                }
            }
        }
    }
}