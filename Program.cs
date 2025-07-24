using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FibonnaciSequence
{
    class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.MarkupLine("[bold aqua]🌀 FIBONACCI GENERATOR (Console GUI Style)[/]");
            AnsiConsole.WriteLine();

            //1. Prompt batas input
            int maxFibonacci = AnsiConsole.Prompt(
                new TextPrompt<int>("🔢 [green]Masukkan batas maksimum Fibonacci[/] (misal 4000000):")
                    .DefaultValue(4000000)
                    .Validate(val => val > 0
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Harus lebih dari 0![/]"))
            );

            //2. Inisialisasi variabel
            int a = 1, b = 2, sumEven = 0;
            List<int> fibonacciList = new();

            // Progress bar
            AnsiConsole.Status()
                .Start("⏳ [yellow]Menghitung deret Fibonacci...[/]", ctx =>
                {
                    while (a < maxFibonacci)
                    {
                        fibonacciList.Add(a);
                        if (a % 2 == 0)
                            sumEven += a;

                        int next = a + b;
                        a = b;
                        b = next;

                        Thread.Sleep(10); // animasi smooth
                    }
                });

            // Tampilkan hasil dalam tabel
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[bold underline yellow]📋 Deret Fibonacci[/]")
                .AddColumn("[aqua]Index[/]")
                .AddColumn("[green]Angka[/]");

            for (int i = 0; i < fibonacciList.Count; i++)
            {
                var val = fibonacciList[i];
                table.AddRow($"{i + 1}", val % 2 == 0 ? $"[bold green]{val}[/]" : $"{val}");
            }

            AnsiConsole.Write(table);

            // Tampilkan hasil akhir
            AnsiConsole.MarkupLine($"\n💰 [bold green]Total bilangan genap:[/] [underline yellow]{sumEven}[/]");
            AnsiConsole.MarkupLine("\n✅ [bold]Selesai! Tekan tombol apa saja untuk keluar...[/]");
            Console.ReadKey();
        }
    }
}