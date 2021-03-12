using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Controller;
using LoadMap;

namespace Game
{
    
    public class Program
    {
        
        public static int Width, Height;
        public static int xp = 12;
        public static int yp = 7;
        static int chestx;
        static int chesty;
        static int traderx,tradery;
        static int Hp = 100;
        static int Lvl = 1;
        static int Score = 1;
        static int Money = 0;
        static bool TakeChest = true;
        static char[][] Map;
        const int CurX = 30;
        static string[] Loot = {"Пусто", "Ножны", "Обломки брони","Футболка","Свежие трусы","Золотые зубы","Безобразное ожирелье","Игрушка обезьянка с дисками","Безвкусная ваза","Убийственнокрасивый меч" };
        static string[] FTrader = { "Что сегодня хочешь продать мне?", "Салам Пету...кхе, что хотите продать?", "Я сегодня щедрый, давай сюда свой хлам", "О, снова ты, молчаливый искатель" };
        public static int[] Inventary = new int[12];
        //-----------------------------------------------------------------------------------------------\\
        
        //-----------------------------------------------------------------------------------------------\\
        public static void CurPos(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        static void CreateChest()
        {
            Random rnd = new Random();
            chestx = rnd.Next(2, Width-1);
            chesty = rnd.Next(2, Height-1);
            TakeChest = true;
        }
        static void InvSort()
        {
            for (int i =0; i<Inventary.Length; i++)
            {
                for (int j = 0; j < Inventary.Length-1; j++)
                {
                    if (Inventary[j] < Inventary[j + 1])
                    {
                        int temp = Inventary[j];
                        Inventary[j] = Inventary[j + 1];
                        Inventary[j + 1] = temp;
                    }
                }
            }
        }
        static int Rand(int min, int max)
        {
            Random rnd = new Random();
            int a = rnd.Next(min, max);
            return a;
        }

        public static void Trader()
        {
            Console.Clear();
            CurPos(0, 18);
            int Sum=0, code;
            string Text = FTrader[Rand(0,4)];
            int[] mas = new int[12];
            Random rnd = new Random();
            for (int i = 0; i<Inventary.Length; i++)
            {
                if (Inventary[i] != 0)
                {
                    int a = rnd.Next(Lvl, 2*Inventary[i]);
                    mas[i] = a;
                    Sum += mas[i];
                }
            }
            Control.pun = 1; Control.code = 0; Control.kur[0] = '>';
            do
            {
                Console.Clear();
                Console.WriteLine("--------------Лавка Торговца--------------\nТорговец:{0}", Text);
                Console.Write("{0}Продать весь хлам({2} монет)\n{1}Молча уйти", Control.kur[0], Control.kur[1], Sum);
                code = Control.ChatKur(2);
            } while (code == 0);
            if (code == 1)
            {
                Money += Sum;
                for (int i = 0; i < Inventary.Length; i++)
                {
                    Inventary[i] = 0; 
                }
                Draw();
            }
            else
            {
                xp++;
                Console.Clear();
            }
        }
        static string InvName(int i)
        {
            return Loot[Inventary[i]];
        }
        static void FullInv()
        {
            if (Inventary[11] != 0)
            {
                CurPos(0, 16);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Инвентарь полон!");

            }
            else Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        static void SetSize()
        {
            Height = Map.Length;
            Width = Map[0].Length;
        }
        static void DrawInv()
        {
            InvSort();
            CurPos(CurX, 0);
            Console.Write("Инвентарь");
            CurPos(CurX, 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            if (Inventary[0] == 0)
            {
                Console.Write("Сейчас у вас ничего нет");
            }
            else
            {
                int a = 0;
                int inc = 1;
                for (int i = 0; i < Inventary.Length; i++)
                {
                    CurPos(CurX, 1 + a);
                    if (i <= 10)
                    {
                        if (Inventary[i] == Inventary[i + 1])
                        {
                            inc++;        
                        }
                        else if (inc>1)
                        {
                            Console.Write("{0}x[{1}]",inc,InvName(i));
                            a++;
                            inc = 1;
                        }
                        else
                        {
                            Console.Write("[{0}]", InvName(i));
                            a++;
                        }
                    }
                    else if (i==11 && Inventary[i] != Inventary[i-1])
                    {
                        if (Inventary[i] != 0)
                        {
                            Console.Write("[{0}]", InvName(i));
                            a++;
                        }
                    }
                    else if (Inventary[i] !=0)
                    {
                        Console.Write("{0}x[{1}]", inc, InvName(i));
                        inc = 1;
                    }

                }                               
            }
        }

        static void DrawText()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Hp:{0},Money:{4}, Level:{1}({2}/{3})",Hp, Lvl,Score, Score*3, Money);            

        }

        static void Draw()
        {
            while (true)
            {
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                
                for (int y = 0; y < Map.Length; y++)
                {
                    Console.SetCursorPosition(0, y);
                    for (int x = 0; x < Map[0].Length; x++)
                    {
                        var map = Map[y][x];
                        if (x == xp && y == yp)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.SetCursorPosition(x, y);
                            Console.Write("1");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if(xp == chestx && yp == chesty && TakeChest)
                        {
                            RandLoot(2);
                        }               
                        else if(xp == traderx && yp == tradery)
                        {
                            Trader();
                        }
                        else if ('1' == map)
                        {
                            CurPos(x, y);
                            Console.Write("#");
                        }else if ('T' == map)
                        {                            
                            CurPos(x, y);
                            traderx = x;
                            tradery = y;
                            Console.Write("T");
                            
                        }
                        else if (x == chestx && y == chesty && TakeChest)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write("C");
                        }
                    }
                    Console.WriteLine();
                }               
                DrawText();
                FullInv();
                DrawInv();
                Control.KeyPlayer();
            }
        }

        static void RandLoot(int Type)
        {
            Random rnd = new Random();
            InvSort();
            if (Type == 1)//Монстры
            {
                int a = rnd.Next(4);
                for (int i = 0; i < Inventary.Length; i++)
                {
                    if (Inventary[i] == 0 && a != 0)
                    {
                        Inventary[i] = rnd.Next(1, 9);
                        a--;
                    }
                    if (Inventary[11] != 0)
                    {
                        break;
                    }
                }
                
            }

            else if (Type == 2)//Сундук
            {
                int a = rnd.Next(1, 6);
                int[] arr = new int[a];
                for (int i = 0; i<arr.Length; i++)
                {
                    arr[i] = rnd.Next(1, 9);
                }
                for (int j = 0; j < Inventary.Length; j++)
                {
                    if (Inventary[j] == 0 && a != 0)
                    {
                        Inventary[j] = arr[a - 1];
                        a--;

                    }
                    else if (Inventary[11] != 0)
                    {
                        break;
                    }
                    
                }
                TakeChest = false;
            }
        }
                
        static void Load()
        {         
            //LoadMap   
            Map = LoadMap.Map.Start.Select(r => r.ToCharArray()).ToArray();
        }

        static void Main(string[] args)
        {
            Console.SetBufferSize(100, Console.BufferHeight);
            Console.SetWindowSize(100, Console.WindowHeight);
            Load();
            SetSize();
            CreateChest();
            Draw();
        }
    }
}
