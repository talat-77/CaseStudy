using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Console.WriteLine("Personel sayısını girin:");
        int count = int.Parse(Console.ReadLine());

        var employees = new List<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)>();

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Personel {i + 1} - Ad,Yaş,Departman,Maaş,Yıl,Ay,Gün:");
            var input = Console.ReadLine().Split(',');

            employees.Add((
                input[0],
                int.Parse(input[1]),
                input[2],
                decimal.Parse(input[3]),
                new DateTime(int.Parse(input[4]), int.Parse(input[5]), int.Parse(input[6]))
            ));
        }

        Console.WriteLine(FilterEmployees(employees));
    }

    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .ToList();

        var result = new
        {
            Names = filtered.Select(e => e.Name).ToList(),
            TotalSalary = filtered.Count > 0 ? filtered.Sum(e => e.Salary) : 0,
            AverageSalary = filtered.Count > 0 ? Math.Round(filtered.Average(e => e.Salary), 2) : 0,
            MinSalary = filtered.Count > 0 ? filtered.Min(e => e.Salary) : 0,
            MaxSalary = filtered.Count > 0 ? filtered.Max(e => e.Salary) : 0,
            Count = filtered.Count
        };

        return JsonSerializer.Serialize(result);
    }
}