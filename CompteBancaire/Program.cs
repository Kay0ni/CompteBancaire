using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompteBancaire
{
    internal class Program
    {
        static int cursor = 1;
        static int max = 6;
        static int min = 1;

        static void CursorOnSelect(int iPosition, string Text, ConsoleColor Color) {
            if (cursor == iPosition) {
                Console.ForegroundColor = ConsoleColor.Yellow;
            } else
            {
                Console.ForegroundColor = Color;
            }
            Console.WriteLine(Text);
            Console.ResetColor();
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Bienvenu a la BanqueDeLaHess");
            Console.WriteLine();
            CursorOnSelect(1, " Créer un compte", ConsoleColor.White);
            CursorOnSelect(2, " Créditer un compte ", ConsoleColor.Green);
            CursorOnSelect(3, " Débiter un compte ", ConsoleColor.DarkRed);
            CursorOnSelect(4, " Consulter un compte", ConsoleColor.White);
            CursorOnSelect(5, " Liste des comptes ", ConsoleColor.White);
            CursorOnSelect(6, " Exuit", ConsoleColor.Red);
        }

        static void MoveCursor(int Direction)
        {
            if (cursor + Direction < min)
            {
                cursor = max;
            }
            else if (cursor + Direction > max)
            {
                cursor = min;
            }
            else
            {
                cursor += Direction;
            }
        }

        static void Main(string[] args)
        {
            Data AcData = new Data("BanqueHess");
            /*
            Compte Account1 = new Compte(1, "Jhonny", "Dett", -10000);
            Compte Account2 = new Compte(2, "Trump", "Dollard", 1000000000);
            Compte Account3 = new Compte(3, "Putune", "Vladimire", 0);
            AcData.AddAccount(Account1);
            AcData.AddAccount(Account2);
            AcData.AddAccount(Account3);
            */

            /*
            foreach (var Account in AcData.GetAllAccount()) {
                Console.WriteLine("N° de Compte : " + Account.GetId() + "\n Prenom du Compte : " + Account.GetPrenom() + "\n Nom du Compte : " + Account.GetNom() + "\n Sold du Compte : " + Account.GetSold());
                Console.WriteLine("");
            }
            */

            void Listed()
            {
                Console.Clear();
                string message = "";
                int i = 0;
                foreach (var Acc in AcData.GetAllAccount())
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    double iDouble = Convert.ToDouble(i);
                    double MaxDouble = Convert.ToDouble(AcData.Count);
                    double percent = (Convert.ToDouble(i) / Convert.ToDouble(AcData.Count)) * 100;
                    double result = iDouble / MaxDouble * 115;
                    string Bar = new string('-', Convert.ToInt32(result));
                    string Space = new string(' ', 115 - Convert.ToInt32(result));
                    string Space2 = new string(' ', 40);
                    Console.WriteLine("[" + Bar + Space + "]");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Space2 + "Loading list [" + Convert.ToInt32(percent) + "%" + "] Eleves");
                    message += ("\nid : " + Acc.Id + "\n\n" + Acc.Prenom + "\n" + Acc.Nom + "\n" + Acc.Sold + "\n" + "_______________________________________________");
                    i += 1;
                }
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine(message);
                Console.ResetColor();
                Console.ReadKey();
            }

            void CreateAccount()
            {
                string Prenom = "";
                do
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Entrez le Prenom: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Prenom = Console.ReadLine();
                    Console.ResetColor();
                } while (Prenom == "");
                string Nom = "";
                do
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Entrez le Nom: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Nom = Console.ReadLine();
                    Console.ResetColor();
                } while (Nom == "");
                bool done = false;
                double sold = -1;
                do
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Entrez la sold: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        sold = Convert.ToDouble(Console.ReadLine());
                        done = true;
                    }
                    catch
                    {
                        done = false;
                    }
                    Console.ResetColor();
                } while (done == false);
                done = false;
                int id = 0;
                while (done == false)
                {
                    id++;
                    done = true;
                    foreach (var acc in AcData.GetAllAccount())
                    {
                        if (acc.Id == id)
                        {
                            done = false;
                        }
                    }
                }
                AcData.AddAccount(new Compte(id, Prenom, Nom, sold));
            }

            void Crediter()
            {
                max = AcData.Count;
                cursor = 0;
                min = 0;
                bool done = false;
                while (done == false)
                {
                    int index = 0;
                    Console.Clear();
                    Console.WriteLine("Selectionner un compte :");
                    foreach (var acc in AcData.GetAllAccount())
                    {
                        CursorOnSelect(index, "Id : " + acc.Id + " // debug : " + index + " " + min + " " + max, ConsoleColor.White);
                        CursorOnSelect(index, acc.Nom + " " + acc.Prenom, ConsoleColor.White);
                        index++;
                    }
                    
                    var Key = Console.ReadKey().Key;
                    switch (Key)
                    {
                        case ConsoleKey.DownArrow: MoveCursor(1); break;
                        case ConsoleKey.UpArrow: MoveCursor(-1); break;
                        case ConsoleKey.Enter: done = true; break;
                    }
                }
                Compte Account = AcData.GetAllAccount()[cursor];
                double sold = -1;
                do
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Entrez la sold: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        sold = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        sold = -1;
                    }
                    Console.ResetColor();
                } while (sold == -1);
                Account.Crediter(sold);
                min = 1;
                max = 6;
                cursor = 1;
            }

            void Leave()
            {
                Console.Clear();
                AcData.Save();
                Console.ReadKey();
                Environment.Exit(0);

            }
            
            void SelectFunc(int currentSelect)
            {
                switch (currentSelect)
                {
                    case 1: CreateAccount(); break;
                    case 2: Crediter(); break;
                    case 5: Listed(); break;
                    case 6: Leave(); break;
                }
            }

            void Handler()
            {
                MainMenu();

                var Key = Console.ReadKey().Key;

                switch(Key)
                {
                    case ConsoleKey.DownArrow: MoveCursor(1); break;
                    case ConsoleKey.UpArrow: MoveCursor(-1); break;
                    case ConsoleKey.Enter: SelectFunc(cursor); break;
                }

                Handler();
            }
            Handler();

            Console.ReadKey();
            
        }
    }
}
