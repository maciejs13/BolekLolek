using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BolekLolek;

public partial class SummaryWindow : Window
{
    public SummaryWindow(IEnumerable<TaskItem> wszystkieZadania, DateTime data)
    {
        InitializeComponent();
        Title = $"Zadania na {data.ToShortDateString()}";
        Naglowek.Text = $"Zadania na dzień {data:dd.MM.yyyy}";

        ListaZadan.ItemsSource = wszystkieZadania
            .Where(z => z.Data.Date == data.Date)
            .ToList();
    }
}