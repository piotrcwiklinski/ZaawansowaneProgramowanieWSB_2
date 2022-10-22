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
            while (!liczba.HasValue);
            return liczba.Value;
        }

    }
    public static class Bukiet
    {
        private static readonly string[] flowerNames = { "fiołek", "róża", "bratek", "jaskier", "goździk", "tulipan" };
        private static readonly double[] flowerPrices = { 5.50, 8, 3.30, 6.20, 3.70, 2.20 };
        private static int minFlowers = 1;
        private static int maxFlowers = 4;

        public static SortedList<int, int> UtworzNowy()
        {

            int wybranaIloscDanegoKwiatka;
            SortedList<int, int> specyfikacja = new SortedList<int, int>();

            for (int i = 0; i < flowerNames.Length; i++)
            {
                Console.WriteLine("\nKWIAT: " + flowerNames[i] + ", CENA ZA SZT.: " + $"{flowerPrices[i]:C}");
                do 
                {
                    wybranaIloscDanegoKwiatka = InputHelper.PobierzOdUzytkownikaLiczbeCalkowita("\tIle sztuk dodać do bukietu?:", maxFlowers, minFlowers);
                }
                while (wybranaIloscDanegoKwiatka > maxFlowers || wybranaIloscDanegoKwiatka < minFlowers);

                specyfikacja.Add(i, wybranaIloscDanegoKwiatka);
            }
            return specyfikacja;
        }

        public static string UtworzOpis(SortedList<int, int> wybranyBukiet)
        {
            string s = "";

            foreach (KeyValuePair<int, int> kwiat in wybranyBukiet)
            {
                string flowerName = flowerNames[kwiat.Key];
                s += "\n" + kwiat.Value + " x " + flowerName;
            }
            return s;
        }

        public static double ObliczCene(SortedList<int, int> wybranyBukiet)
        {
            double s = 0;

            foreach (KeyValuePair<int, int> kwiat in wybranyBukiet)
            {
                double flowerPrice = flowerPrices[kwiat.Key];
                s += (kwiat.Value * flowerPrice);
            }
            return s;
        }
    }
    internal class Program
    {
        
        static void Main(string[] args)
        {
            SortedList<int, int> twojBukiet;

            Console.WriteLine("\t!!! WITAMY W KWIACIARNI !!!");
            Console.WriteLine("\nPodaj liczbę (pomiędzy 1-4) dla każdego rodzaju kwiatów, z których ma zostać utworzony Twój bukiet: ");
            twojBukiet = Bukiet.UtworzNowy();
            Console.WriteLine("\nUtworzony przez Ciebie bukiet: " + Bukiet.UtworzOpis(twojBukiet));
            Console.WriteLine("Cena łączna: " + $"{Bukiet.ObliczCene(twojBukiet):C}");   
        }
    }
}
