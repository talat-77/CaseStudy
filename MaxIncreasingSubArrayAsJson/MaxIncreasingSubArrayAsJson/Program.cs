using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MaxIncreasingSubArrayAsJson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Sayıları virgül ile girin: ");
            string input = Console.ReadLine();

            List<int> sayilar = new List<int>();

            if (!string.IsNullOrEmpty(input))
            {
                string[] parcalar = input.Split(',');
                for (int i = 0; i < parcalar.Length; i++)
                {
                    int sayi;
                    if (int.TryParse(parcalar[i].Trim(), out sayi))
                    {
                        sayilar.Add(sayi);
                    }
                }
            }

            string sonuc = MaxIncreasingSubarrayAsJson(sayilar);
            Console.WriteLine("Sonuç: " + sonuc);
            Console.ReadKey();
        }

        public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return JsonSerializer.Serialize(new List<int>());
            }

            if (numbers.Count == 1)
            {
                return JsonSerializer.Serialize(numbers);
            }

            List<int> enBuyukDizi = new List<int>();
            int enBuyukToplam = int.MinValue;

            List<int> simdikiDizi = new List<int>();
            simdikiDizi.Add(numbers[0]);
            int simdikiToplam = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    simdikiDizi.Add(numbers[i]);
                    simdikiToplam = simdikiToplam + numbers[i];
                }
                else
                {
                    if (simdikiToplam > enBuyukToplam)
                    {
                        enBuyukToplam = simdikiToplam;
                        enBuyukDizi = new List<int>();
                        for (int j = 0; j < simdikiDizi.Count; j++)
                        {
                            enBuyukDizi.Add(simdikiDizi[j]);
                        }
                    }

                    simdikiDizi = new List<int>();
                    simdikiDizi.Add(numbers[i]);
                    simdikiToplam = numbers[i];
                }
            }

            if (simdikiToplam > enBuyukToplam)
            {
                enBuyukToplam = simdikiToplam;
                enBuyukDizi = new List<int>();
                for (int k = 0; k < simdikiDizi.Count; k++)
                {
                    enBuyukDizi.Add(simdikiDizi[k]);
                }
            }

            return JsonSerializer.Serialize(enBuyukDizi);
        }
    }
}