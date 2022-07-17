namespace Tetris
{
    public abstract class Bloczek
    {
        protected int[,] kształtBloczka = null;
        protected int rozmiar = 4;

        public Bloczek()
        { }

        public Bloczek(Bloczek b)
        {
            PosX = b.PosX;
            PosY = b.PosY;
            kształtBloczka = b.kształtBloczka.Clone() as int[,];
            rozmiar = b.rozmiar;
        }

        public int PosX { get; private set; } = 0;

        public int PosY { get; private set; } = 0;

        public int Długość
        { get { return kształtBloczka.GetLength(0); } }

        public int Szerokość
        { get { return kształtBloczka.GetLength(1); } }

        public int this[int i, int j]
        {
            get { return kształtBloczka[i, j]; }
        }

        public void PozycjaStartowa(int i)
        {
            PosX = i;
            PosY = 0;
        }

        public void MoveDown()
        {
            PosY++;
        }

        public void MoveLeft()
        {
            PosX--;
        }

        public void MoveRight()
        {
            PosX++;
        }

        public void ZamróźBloczek()
        {
            for (int j = 0; j < kształtBloczka.GetLength(1); j++)
            {
                for (int i = 0; i < kształtBloczka.GetLength(0); i++)
                {
                    if (kształtBloczka[i, j] == 1)
                    {
                        kształtBloczka[i, j] = 2;
                    }
                }
            }
        }

        public void Obrót(bool kierunekZegara)
        {
            int n = kształtBloczka.GetLength(0);
            int[,] StanPoObrocie = new int[n, n];
            int iterator = 0;
            if (kierunekZegara)
            {
                iterator = n - 1;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        StanPoObrocie[iterator, i] = kształtBloczka[i, j];
                        iterator--;
                    }
                    iterator = n - 1;
                }
            }
            else
            {
                iterator = 0;
                for (int i = n - 1; i >= 0; i--)
                {
                    for (int j = 0; j < n; j++)
                    {
                        StanPoObrocie[j, iterator] = kształtBloczka[i, j];
                    }
                    iterator++;
                }
            }
            kształtBloczka = StanPoObrocie;
        }
    }
}