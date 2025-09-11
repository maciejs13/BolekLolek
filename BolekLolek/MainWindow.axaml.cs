using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;


namespace BolekLolek;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {

        if (ComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selected = selectedItem.Content?.ToString() ?? "";

            string path = selected switch
            {
                "Bolek" => "avares://BolekLolek/Assets/Bolek.png",
                "Lolek" => "avares://BolekLolek/Assets/Lolek.jpeg"
            };

            using var stream = AssetLoader.Open(new Uri(path));
            Image.Source = new Bitmap(stream);
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        
        string zadanie = Zadanie.Text;

        if (!string.IsNullOrWhiteSpace(zadanie) && ComboBox.SelectedItem != null)
        {
            string osoba = (ComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string wpis = zadanie + " (- " + osoba + ")";
            (ListBox.Items as System.Collections.IList)?.Add(wpis);
            Zadanie.Text = string.Empty;
        }
    }
}