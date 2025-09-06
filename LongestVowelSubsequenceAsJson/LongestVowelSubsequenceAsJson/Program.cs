using System;
using System.Collections.Generic;
using System.Text.Json;

namespace LongestVowelSubsequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Kelimeleri virgül ile girin: ");
            string input = Console.ReadLine();

            List<string> kelimeler = new List<string>();

            if (!string.IsNullOrEmpty(input))
            {
                string[] parcalar = input.Split(',');
                for (int i = 0; i < parcalar.Length; i++)
                {
                    kelimeler.Add(parcalar[i].Trim());
                }
            }

            string sonuc = LongestVowelSubsequenceAsJson(kelimeler);
            Console.WriteLine("Sonuç: " + sonuc);
            Console.ReadKey();
        }

        public static string LongestVowelSubsequenceAsJson(List<string> words)
        {
            List<object> sonucListesi = new List<object>();

            for (int i = 0; i < words.Count; i++)
            {
                string kelime = words[i];
                string enUzunSesliDizi = "";
                int enUzunUzunluk = 0;

                string simdikiDizi = "";

                for (int j = 0; j < kelime.Length; j++)
                {
                    char harf = char.ToLower(kelime[j]);

                    if (SesliHarfMi(harf))
                    {
                        simdikiDizi = simdikiDizi + harf;
                    }
                    else
                    {
                        if (simdikiDizi.Length > enUzunUzunluk)
                        {
                            enUzunUzunluk = simdikiDizi.Length;
                            enUzunSesliDizi = simdikiDizi;
                        }
                        simdikiDizi = "";
                    }
                }

                if (simdikiDizi.Length > enUzunUzunluk)
                {
                    enUzunUzunluk = simdikiDizi.Length;
                    enUzunSesliDizi = simdikiDizi;
                }

                var kelimeSonucu = new
                {
                    word = kelime,
                    sequence = enUzunSesliDizi,
                    length = enUzunUzunluk
                };

                sonucListesi.Add(kelimeSonucu);
            }

            return JsonSerializer.Serialize(sonucListesi);
        }

        private static bool SesliHarfMi(char harf)
        {
            return harf == 'a' || harf == 'e' || harf == 'i' || harf == 'o' || harf == 'u';
        }
    }
}