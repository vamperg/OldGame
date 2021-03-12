using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace LearnGame
{
    class Program
    {
        
        static int code, pun, money, ore;
        static string mydocu = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static int[] pickaxes = new int[3] {0, 0, 0 };
        static int[] woodpix = new int[10] { 100, 200, 300, 400, 600, 800, 1000, 1400, 1800, 2200 };
        static int[] ironpix = new int[10] { 3000, 3500, 4100, 5000, 6000, 8000, 10000, 11400, 12800, 14000 };
        static int[] goldpix = new int[10] { 15500, 17000, 20300, 22400, 24600, 26800, 28100, 31400, 33800, 39990 };
        static int[] pixcount = new int[3];
        static int[] pixcountsave = new int[3] { 10, 30, 50 };
        static double[] pixtime = new double[3] { 0.2, 0.6, 1.2 };
        static char[] kur = new char[255];
        public static string nickname;
        static Random rnd = new Random();

        static void Sleep(double persec)
        {
            double per = persec * 1000;
            Thread.Sleep((int)per);
        }

        static void Clear()
        {
            Console.Clear();
        }
        static void Exit()
        {
            Clear();
            Console.Write("Удачи...");
            Sleep(0.5);
            Environment.Exit(0);
        }
        static void Stats()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Имя:{0}, Денег:{1}, Руды:{2}", nickname, money, ore);
            Console.ResetColor();
        }
        static void Help()
        {
            Clear();
            Stats();
            Console.Write("(Помощь)\n\nЭто подобие кликера, но при этом на консоле.\nКак вы могли видеть в меню есть пункт 'Копать'.\nТам вы копаете руду и уже в магазине эту руду можно продать.\nТам же можно купить более сильное оружие.\nРазработчик Линар Галяув.\nНажмите любую кнопку, чтобы вернуться в меню...");
            Console.ReadKey();
            Menu();
            
        }
        static void ShopMax()
        {
            Clear();
            Console.Write("Кирка достигла максимального уровня!\n>Назад");
            Console.ReadKey();
            Shop();
        }
        static void ShowLowMoney()
        {
            Clear();
            Console.Write("У вас недостаточно денег!\n>Назад");
            Console.ReadKey();
            ShopPickaxe();
        }
        static void ShopPickaxe()
        {
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Console.Clear();
                Stats();
                Console.Write("(Кирки)\t\t(Уровень кирки)\t(Цена)\t(Время добычи)\t(Кол-во руды)\n\n");
                Console.Write("{0}Простая кирка\t\t({7})\t{1}\t-0.2сек.\t+5-10\n{2}Железная кирка\t\t({8})\t{3}\t-0.6 сек.\t+20-30\n{4}Профессиональная кирка\t({9})\t{5}\t-1,2сек.\t+40-50\n{6}Назад", kur[0],(woodpix[pickaxes[0]]),kur[1],(ironpix[pickaxes[1]]),kur[2],(goldpix[pickaxes[2]]),kur[3],pickaxes[0],pickaxes[1],pickaxes[2]);
                code = Kur(4, "");
            } while (code == 0);
            if (code == 1)
            {
                if ((money >= woodpix[pickaxes[0]]) && (pickaxes[0] <9))
                {
                    money -= (woodpix[pickaxes[0]]);
                    pickaxes[0] += 1;
                    pixcount[0] += 10;
                    ShopPickaxe();
                }
                else if(money < woodpix[pickaxes[0]])
                {
                    ShowLowMoney();
                }
                else if (pickaxes[0] == 9)
                {
                    ShopMax();
                }
            }
            else if(code == 2)
            {
                if ((money >= ironpix[pickaxes[1]]) && (pickaxes[1] < 9))
                {
                    money -= (ironpix[pickaxes[1]]);
                    pickaxes[1]++;
                    pixcount[1] += 30;
                    ShopPickaxe();
                }
                else if (money < ironpix[pickaxes[1]])
                {
                    ShowLowMoney();
                }
                else if (pickaxes[1] == 9)
                {
                    ShopMax();
                }
            }
            else if (code == 3)
            {
                if ((money >= goldpix[pickaxes[2]]) && (pickaxes[2] < 9))
                {
                    money -= (goldpix[pickaxes[2]]);
                    pickaxes[2]++;
                    pixcount[2] += 50;
                    ShopPickaxe();
                }
                else if (money < goldpix[pickaxes[2]])
                {
                    ShowLowMoney();
                }
                else if (pickaxes[2] == 9)
                {
                    ShopMax();
                }
            }
            else
            {
                Shop();
            }
        }
        static void Shop()
        {
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Clear();
                Stats();
                Console.Write("(Магазин)\n\n{0}Купить/Улучшить Кирку\n{1}Продать руду(1=4$)\n{2}Назад", kur[0], kur[1], kur[2]);
                code = Kur(3, "");
            } while (code == 0);
            if (code == 1)
            {
                ShopPickaxe();
            }
            else if (code == 2)
            {
                money += (ore * 4);
                ore = 0;
                Shop();
            }
            else Menu();
        }
        static int Kur(int maxi, string target)
        {
            ConsoleKey btn = Console.ReadKey().Key;
            if (btn == ConsoleKey.Enter)
            {
                kur[pun-1] = ' ';
                return pun;
            }
            if (btn == ConsoleKey.W || btn == ConsoleKey.UpArrow)
            {
                if (pun == 1)
                {
                    kur[0] = ' ';
                    pun = maxi;
                    kur[maxi - 1] = '>';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun - 2] = '>';
                    pun--;
                    return 0;
                }
            }
            else if (btn == ConsoleKey.S || btn == ConsoleKey.DownArrow)
            {
                if (pun == maxi)
                {
                    kur[maxi - 1] = ' ';
                    pun = 1;
                    kur[0] = '>';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun] = '>';
                    pun++;
                    return 0;
                }
            }
            else return 0;
        }
        //Угольная шахта
        static void CoalMine()
        {
            double worktime = 20;
            int orecount = 0;
            if (pickaxes[0] == 0)
            {
                Clear();
                Console.Write("Для работы в шахте вам нужно купить инструменты!\n>Назад");
                Console.ReadKey();
                Menu();
            }
            else
            {
                for (int i = 0; i < pickaxes.Length; i++)
                {
                    worktime -= pickaxes[i] * pixtime[i];
                    if (pickaxes[i] != 0)
                    {
                        orecount += rnd.Next(5, pixcount[i]);
                    }
                }
                ore += orecount;
                for (double i = 0; i < worktime; i+=0.2)
                {
                    Clear();
                    Console.Write("Добыча руды[{0}/{1}]", i, worktime);
                    Sleep(0.2);
                }
                pun = 1; code = 0; kur[0] = '>';
                do
                {
                    Clear();
                    Stats();
                    Console.Write("Добыто {0} Руды! Можете продать ее в магазине.\n\n{1}Продолжить\n{2}Назад", orecount, kur[0], kur[1]);
                    code = Kur(2, "");
                } while (code == 0);
                if (code == 1)
                {
                    CoalMine();
                }
                else
                {
                    OreMine();
                }
            }
        }

        //Шахты
        static void OreMine()
        {
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Clear();
                Stats();
                Console.Write("(Шахты)\n\n{0}Угольная шахта\n{1}В меню",kur[0],kur[1]);
                code = Kur(2, "");
            } while (code == 0);
            if (code == 1)
            {
                CoalMine();
            }
            if (code == 2)
            {
                Menu();
            }
        }


        static void NewGame()
        {
            Console.Write("Добро пожаловать в игру.\nВведите свой ник:");
            nickname = Console.ReadLine();
            money = 100;
            ore = 0;
            Clear();
            Menu();
        }
        static bool IsLoad()
        {
            FileInfo file = new FileInfo(mydocu + "/Mining Business/save.ini");

            if (file.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void SwitchStart(int number)
        {
            Clear();
            switch (number)
            {
                case 1: NewGame();
                    break;
                case 2: LoadGame();
                    break;
            }
        }
        static void Start()
        {
            int sw = 0;
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Clear();
               if (IsLoad())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}Загрузить\n", kur[0]);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}Новая игра", kur[1]);
                    Console.ResetColor();
                    sw = 1;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}Новая игра\n", kur[0]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}Загрузить", kur[1]);
                    Console.ResetColor();
                    sw = 2;
                };
               
                Console.Write("\n{0}Выход", kur[2]);
                Console.ResetColor();
                code = Kur(3, "");
            } while (code == 0);
            if (code == 1)
            {
                if (sw == 1)
                {
                    SwitchStart(2);
                }
                else SwitchStart(1);
            }
            else if (code == 2)
            {
                if (sw == 1)
                {
                    SwitchStart(sw);
                }
                else SwitchStart(2);
            }
            else
            {
                Exit();
            }
        }
        static void SaveGame(object args)
        {
            FileInfo file = new FileInfo(mydocu + "/Mining Business/save.ini");

            Sleep(0.2);
            string[] save = new string[6] { nickname, money.ToString(), ore.ToString(), pickaxes[0].ToString(), pickaxes[1].ToString(), pickaxes[2].ToString() };
            File.WriteAllLines(mydocu + "/Mining Business/save.ini", save);
        }
        static void LoadGame()
        {
            if (IsLoad())
            {
                code = 0; pun = 1; kur[0] = '>';
                do
                {
                    Clear();
                    Console.Write("Есть сохранение!\nSave: {0}\n{1}Загрузить\n{2}Удалить", File.GetCreationTime(mydocu + "/Mining Business/save.ini").ToString(), kur[0], kur[1]);
                    code = Kur(2, "");
                } while (code == 0);
                if (code == 1)
                {
                    string[] load = File.ReadAllLines(mydocu + "/Mining Business/save.ini", Encoding.UTF8);
                    nickname = load[0];
                    money = int.Parse(load[1]);
                    ore = int.Parse(load[2]);
                    for (int i = 0; i < pickaxes.Length; i++)
                    {
                        pickaxes[i] = int.Parse(load[3 + i]);
                        pixcount[i] += pickaxes[i] * pixcountsave[i];
                    }
                    Menu();
                }else if (code == 2)
                {
                    File.Delete(mydocu + "/Mining Business/save.ini");
                    Clear();
                    Console.Write("Сохранение было удалено!");
                    Console.ReadKey();
                    Start();
                }
            }              
            else
            {            
                pun = 1; code = 0; kur[0] = '>';
                do
                {
                    Clear();
                    Console.WriteLine("Сохранение не найдено!\n{0}Назад",kur[0]);
                    code = Kur(1, "");
                } while (code == 0);
                if (code == 1)
                {
                    Start();
                }
            }
        }

        static void Menu()
        {
            Timer timetosave = new Timer(SaveGame, null, 0, 10 * 1000);
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Clear();
                Stats();
                Console.Write("(Меню)\n\n{0}Копать\n{1}Магазин\n{2}Помощь\n{3}Сохранить и выйти", kur[0], kur[1], kur[2], kur[3]);
                code = Kur(4, "");
            } while (code == 0);
            if (code == 1)
            {
                OreMine();
            }
            else if (code == 2)
            {
                Shop();
            }
            else if (code == 3)
            {
                Help();
            }
            else if (code == 4)
            {
                SaveGame(2);
            }
        }


        static void Main(string[] args)
        {
            Directory.CreateDirectory(mydocu+"/Mining Business");
            Console.SetWindowSize(100,30);
            Console.Title = "Mining Business";
            Start();
            
        }
    }
}
