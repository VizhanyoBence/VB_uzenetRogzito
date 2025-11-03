using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace uzenetRogzito
{
        class Program
        {
            static Dictionary<string, string> mellekek = new Dictionary<string, string>()
    {
        { "11", "titkárság" },
        { "14", "tanári" },
        { "15", "kollégium" },
        { "16", "könyvtár" }
    };

            static Random rand = new Random();

            static void Main(string[] args)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Üzenetrögzítő rendszer");
                    Console.WriteLine("1 – Telefonáló felület");
                    Console.WriteLine("2 – Hívott fél felület");
                    Console.WriteLine("0 – Kilépés");
                    Console.Write("Válassz menüpontot: ");
                    string valasztas = Console.ReadLine();

                    switch (valasztas)
                    {
                        case "1":
                            TelefonaloFelulet();
                            break;
                    case "2":
                        Console.Write("Add meg a felhasználó nevet(Admin):");
                        string felhasznalo = Console.ReadLine();
                        Console.Write("Add meg a jelszót(Admin123): ");
                        string jelszo = Console.ReadLine();
                        

                        if (felhasznalo == "Admin" && jelszo == "Admin123")
                        {
                            HivottFelFelulet();
                        }
                        else
                        {
                            Console.WriteLine("Hibás jelszó! Nyomj Entert a visszatéréshez...");
                            Console.ReadLine();
                        }
                        break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Érvénytelen választás! Nyomj Entert...");
                            Console.ReadLine();
                            break;
                    }
                }
            }

            static void TelefonaloFelulet()
            {
                Console.Clear();
                Console.WriteLine("Választható mellékek:");
                foreach (var mellek in mellekek)
                {
                    Console.WriteLine($"{mellek.Key} – {mellek.Value}");
                }

                Console.Write("Add meg a kívánt melléket: ");
                string mell = Console.ReadLine();

                if (!mellekek.ContainsKey(mell))
                {
                    Console.WriteLine("Érvénytelen mellék! Nyomj Entert...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Add meg a neved: ");
                string nev = Console.ReadLine();

                Console.Write("Írd be az üzeneted: ");
                string uzenet = Console.ReadLine();

                string telefonszam = "+36" + rand.Next(100000000, 999999999);

                string fajlNev = $"{mell}.txt";
                int sorszam = 1;

                if (File.Exists(fajlNev))
                {
                    var sorok = File.ReadAllLines(fajlNev);
                    if (sorok.Length > 0)
                    {
                        var utolsoSor = sorok[1];
                        var mezok = utolsoSor.Split(';');
                        int.TryParse(mezok[0], out sorszam);
                        sorszam++;
                    }
                }

                string datumIdo = DateTime.Now.ToString("yyyy. MM. dd. HH:mm:ss");
                string sor = $"{sorszam};{nev};{uzenet};{telefonszam};{datumIdo}";

                File.AppendAllText(fajlNev, sor + Environment.NewLine, Encoding.UTF8);
                Console.WriteLine("Üzenet rögzítve! Nyomj Entert...");
                Console.ReadLine();
            }

            static void HivottFelFelulet()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Üzenetek megtekintése:");
                    foreach (var mellek in mellekek)
                    {
                        Console.WriteLine($"\t{mellek.Key} – {mellek.Value}");
                    }
                    Console.WriteLine("\t1 – Összes");
                    Console.WriteLine("\t0 – Vissza");
                    Console.Write("Válassz menüpontot: ");
                    string valasztas = Console.ReadLine();

                    if (valasztas == "0")
                        return;

                    if (valasztas == "1")
                    {
                        foreach (var mellek in mellekek)
                        {
                            string fajl = $"{mellek.Key}.txt";
                            if (File.Exists(fajl))
                            {
                                Console.WriteLine(new string('-', 40));
                                Console.WriteLine($"{mellek.Key} – {mellek.Value}");
                                Console.WriteLine(new string('-', 40));
                                string[] sorok = File.ReadAllLines(fajl, Encoding.UTF8);
                                foreach (var sor in sorok)
                                {
                                    Console.WriteLine(sor);
                                }
                                File.Delete(fajl);
                            }
                        }
                        Console.WriteLine("Nyomj Entert a folytatáshoz...");
                        Console.ReadLine();
                    }
                    else if (mellekek.ContainsKey(valasztas))
                    {
                        string fajl = $"{valasztas}.txt";
                        Console.WriteLine(new string('-', 40));
                        Console.WriteLine($"{valasztas} – {mellekek[valasztas]}");
                        Console.WriteLine(new string('-', 40));
                        if (File.Exists(fajl))
                        {
                            string[] sorok = File.ReadAllLines(fajl, Encoding.UTF8);
                            foreach (var sor in sorok)
                            {
                                Console.WriteLine(sor);
                            }
                            File.Delete(fajl);
                        }
                        else
                        {
                            Console.WriteLine("Nincs új üzenet.");
                        }

                        Console.WriteLine("Nyomj Entert a folytatáshoz...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Érvénytelen választás! Nyomj Entert...");
                        Console.ReadLine();
                    }
                }
            }
        }
}
