using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BolekLolek;

public partial class MainWindow : Window
{
    private ObservableCollection<TaskItem> _zadania = new();

    public MainWindow()
    {
        InitializeComponent();
        ListBox.ItemsSource = _zadania;
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selected = selectedItem.Content?.ToString() ?? "";
            string path = selected switch
            {
                "Bolek" => "avares://BolekLolek/Assets/Bolek.png",
                "Lolek" => "avares://BolekLolek/Assets/lolek.jpg",
                _ => ""
            };
            if (!string.IsNullOrEmpty(path))
            {
                using var stream = AssetLoader.Open(new Uri(path));
                Image.Source = new Bitmap(stream);
            }
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Zadanie.Text) && ComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string priorytet = (new[] { RadioNiski, RadioNormalny, RadioWysoki }
                .FirstOrDefault(r => r.IsChecked == true))?.Content?.ToString() ?? "brak";

            var dodatkowe = new StringBuilder();
            if (CheckDworz.IsChecked == true) dodatkowe.Append(" Na dworze;");
            if (CheckSprzet.IsChecked == true) dodatkowe.Append(" Sprzęt;");
            if (CheckKolegow.IsChecked == true) dodatkowe.Append(" Koledzy;");

            var task = new TaskItem
            {
                Opis = Zadanie.Text,
                Osoba = selectedItem.Content?.ToString() ?? "",
                Priorytet = priorytet,
                Opcje = dodatkowe.ToString(),
                Data = Calendar.SelectedDate?.Date ?? DateTime.Today
            };

            _zadania.Add(task);
            Zadanie.Text = "";
        }
    }

    private void DeleteTask_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is TaskItem item)
        {
            _zadania.Remove(item);
        }
    }

    private void SummaryButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Calendar.SelectedDate.HasValue)
        {
            var summaryWindow = new SummaryWindow(_zadania, Calendar.SelectedDate.Value.Date);
            summaryWindow.Show();
        }
    }
}

public class TaskItem
{
    public string Opis { get; set; } = "";
    public string Osoba { get; set; } = "";
    public string Priorytet { get; set; } = "";
    public string Opcje { get; set; } = "";
    public DateTime Data { get; set; }

    public override string ToString()
        => $"{Osoba} – {Opis} [{Priorytet}] {Opcje} ({Data.ToShortDateString()})";
}
