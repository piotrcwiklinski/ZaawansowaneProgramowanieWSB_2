using System;
using System.Collections.Generic;

namespace ZaawansowaneProgramowanieWSB_2
{
    public static class InputHelper
    {
        public static int PobierzOdUzytkownikaLiczbeCalkowita(string zacheta , int wartoscMaksymalna, int wartoscMinimalna)
        {
            int? liczba = null;
            do
            {
                try
                {
                    string s;
                    do
                    {
                        Console.Write(zacheta);
                        s = Console.ReadLine();
                    }
                    while (string.IsNullOrWhiteSpace(s));
                    liczba = int.Parse(s);
                    if (liczba < wartoscMinimalna || liczba > wartoscMaksymalna)
                        throw new Exception("Niepoprawna wartość liczby!");
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Błąd: " + exc.Message);
                    Console.WriteLine("Niepoprawna wartość. Spróbuj jeszcze raz!");
                };
            }
            while (!liczba.HasValue || liczba > wartoscMaksymalna || liczba < wartoscMinimalna);
            return liczba.Value;
        }
    }
    public class Bukiet
    {
        private static readonly string[] flowerNames = { "fiołek", "róża", "bratek", "jaskier", "goździk", "tulipan" };
        private static readonly double[] flowerPrices = { 5.50, 8, 3.30, 6.20, 3.70, 2.20 };
        private static int minFlowers = 1;
        private static int maxFlowers = 4;
        private List<int> specyfikacjaBukietu;
        private static List<Bukiet> sprzedaneBukiety = new List<Bukiet>();

        public Bukiet(List<int> specyfikacjaBukietu)
        {
            this.specyfikacjaBukietu = specyfikacjaBukietu;
        } 

        public static List<int> UtworzNowy()
        {
            int wybranaIloscKwiatow;
            List<int> specyfikacja = new List<int>();
            Random r = new Random();

            wybranaIloscKwiatow = InputHelper.PobierzOdUzytkownikaLiczbeCalkowita("\nPodaj liczbę kwiatów (pomiędzy 1-4), z których ma zostać utworzony Twój bukiet:", maxFlowers, minFlowers);

            for (int i = 0; i < wybranaIloscKwiatow; i++)
            {
                int flowerIndex = r.Next(0, flowerNames.Length - 1);
                specyfikacja.Add(flowerIndex);
            }
            return specyfikacja;
        }

        private static void DodajDoListySprzedanychBukietow(Bukiet bukiet)
        {
            sprzedaneBukiety.Add(bukiet);
        }

        public static void Sprzedaj()
        {
            List<int> specyfikacja = UtworzNowy();
            Bukiet nowyBukiet = new Bukiet(specyfikacja);
            DodajDoListySprzedanychBukietow(nowyBukiet);
            Kwiaciarnia.ZwiekszUtarg(ObliczCene(nowyBukiet));
            Console.WriteLine("Utworzono nowy bukiet: " + Bukiet.UtworzOpis(nowyBukiet));
            Console.WriteLine("Cena łączna: " + $"{Bukiet.ObliczCene(nowyBukiet):C}");
        }

        public static string UtworzOpis(Bukiet wybranyBukiet)
        {
            string s = "";

            foreach (int kwiatIndex in wybranyBukiet.specyfikacjaBukietu)
            {
                string flowerName = flowerNames[kwiatIndex];
                double flowerPrice = flowerPrices[kwiatIndex];
                s += "\n" + flowerName + "(cena: " + $"{flowerPrice:C})";
            }
            return s;
        }

        public static double ObliczCene(Bukiet wybranyBukiet)
        {
            double s = 0;

            foreach (int kwiatIndex in wybranyBukiet.specyfikacjaBukietu)
            {
                double flowerPrice = flowerPrices[kwiatIndex];
                s += flowerPrice;
            }
            return s;
        }

        public static void WyswietlSprzedane()
        {
            if (sprzedaneBukiety.Count > 0)
            {
                int i = 1;
                Console.WriteLine("\nLista sprzedanych dziś bukietów:");
                foreach (Bukiet bukiet in sprzedaneBukiety)
                {
                    Console.WriteLine("\nBukiet nr. " + i + ":");
                    Console.WriteLine(UtworzOpis(bukiet));
                    i++;
                }
            }
            else
                Console.WriteLine("Nie sprzedano dziś jeszcze żadnego bukietu!");
        }
    }

    public static class Kwiaciarnia
    {
        public static double utarg = 0;


        public static void ZwiekszUtarg(double cenaBukietu)
        {
            utarg += cenaBukietu;
        }

        public static void WyswietlUtarg()
        {
            Console.WriteLine("\nDzisiejszy utarg łączny wynosi: " + $"{utarg:C}.");
        }

    }

    public class Menu
    {
        private static void wyswietlMenu()
        {
            string s = "\nMENU: ";
            s += "\n1. Sprzedaj nowy bukiet Klientowi";
            s += "\n2. Wyświetl aktualny łączny utarg";
            s += "\n3. Wyświetl listę wszystkich sprzedanych dziś bukietów";
            s += "\n0. Zakończ dzień pracy (podsumuj łączny zarobek)";
            Console.WriteLine(s);
        }

        public static void Uruchom()
        {
            int wybor = 0;
            do
            {
                wyswietlMenu();
                wybor = InputHelper.PobierzOdUzytkownikaLiczbeCalkowita("\nWybierz pozycję z menu wpisując liczbę i naciskając klawisz Enter: ", 3, 0);
                switch (wybor)
                {
                    case 1:
                        Bukiet.Sprzedaj();
                        break;
                        case 2:
                        Kwiaciarnia.WyswietlUtarg();
                        break;
                    case 3:
                        Bukiet.WyswietlSprzedane();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                        break;
                }
            }
            while (wybor != 0);
        }
    }

    internal class Program
    {
        
        static void Main(string[] args)
        {


            Console.WriteLine("\t!!! WITAMY W KWIACIARNI !!!");
            Menu.Uruchom();
            Console.WriteLine("\nUtarg na koniec dnia: " + $"{ Kwiaciarnia.utarg:C}");
            
        }
    }
}
