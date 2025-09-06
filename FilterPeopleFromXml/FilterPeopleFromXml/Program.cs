using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.Json;

namespace XmlPersonFilter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XML verisini girin:");
            string xmlVeri = Console.ReadLine();

            string sonuc = FilterPeopleFromXml(xmlVeri);
            Console.WriteLine("Sonuç: " + sonuc);
            Console.ReadKey();
        }

        public static string FilterPeopleFromXml(string xmlData)
        {
            try
            {
                XDocument xmlDokuman = XDocument.Parse(xmlData);

                List<string> uygunIsimler = new List<string>();
                List<int> uygunMaaslar = new List<int>();

                var kisiler = xmlDokuman.Root.Elements("Person");

                foreach (var kisi in kisiler)
                {
                    string isim = kisi.Element("Name").Value;
                    int yas = int.Parse(kisi.Element("Age").Value);
                    string departman = kisi.Element("Department").Value;
                    int maas = int.Parse(kisi.Element("Salary").Value);
                    DateTime iseGirisTarihi = DateTime.Parse(kisi.Element("HireDate").Value);

                    if (yas > 30 && departman == "IT" && maas > 5000 && iseGirisTarihi.Year < 2019)
                    {
                        uygunIsimler.Add(isim);
                        uygunMaaslar.Add(maas);
                    }
                }

                // İsimleri alfabetik sırala
                for (int i = 0; i < uygunIsimler.Count - 1; i++)
                {
                    for (int j = i + 1; j < uygunIsimler.Count; j++)
                    {
                        if (string.Compare(uygunIsimler[i], uygunIsimler[j]) > 0)
                        {
                            string gecici = uygunIsimler[i];
                            uygunIsimler[i] = uygunIsimler[j];
                            uygunIsimler[j] = gecici;

                            int geciciMaas = uygunMaaslar[i];
                            uygunMaaslar[i] = uygunMaaslar[j];
                            uygunMaaslar[j] = geciciMaas;
                        }
                    }
                }

                int toplamMaas = 0;
                int enYuksekMaas = 0;

                for (int i = 0; i < uygunMaaslar.Count; i++)
                {
                    toplamMaas = toplamMaas + uygunMaaslar[i];
                    if (uygunMaaslar[i] > enYuksekMaas)
                    {
                        enYuksekMaas = uygunMaaslar[i];
                    }
                }

                double ortalamaMaas = 0;
                if (uygunMaaslar.Count > 0)
                {
                    ortalamaMaas = (double)toplamMaas / uygunMaaslar.Count;
                }

                var sonuc = new
                {
                    Names = uygunIsimler,
                    TotalSalary = toplamMaas,
                    AverageSalary = ortalamaMaas,
                    MaxSalary = enYuksekMaas,
                    Count = uygunIsimler.Count
                };

                return JsonSerializer.Serialize(sonuc);
            }
            catch
            {
                var bosSonuc = new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0.0,
                    MaxSalary = 0,
                    Count = 0
                };
                return JsonSerializer.Serialize(bosSonuc);
            }
        }
    }
}