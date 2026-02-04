using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using MyCoffeeLtd.Models;

namespace MyCoffeeLtd;

public partial class MainWindow : Window
{
    private const decimal TaxRate = 0.08m;

    private readonly ObservableCollection<Beverage> _cart = new();

    private decimal _selectedTipRate = 0.00m;      // 0 / 0.10 / 0.15 / 0.20
    private bool _customTipSelected = false;       // true only when Custom is selected
    private decimal _customTipAmount = 0.00m;      // custom dollar amount

    public MainWindow()
    {
        InitializeComponent();

        // Bind cart once so UI updates automatically on Add/Remove/Clear
        CartList.ItemsSource = _cart;

        // Default state: 0% tip selected, custom disabled
        CustomTipBox.IsEnabled = false;

        UpdateCartUI();
    }

    // ---------- Add Items ----------
    private void AddEspresso_Click(object? sender, RoutedEventArgs e)
    {
        _cart.Add(new Espresso());
        UpdateCartUI();
    }

    private void AddDarkRoast_Click(object? sender, RoutedEventArgs e)
    {
        _cart.Add(new DarkRoast());
        UpdateCartUI();
    }

    private void AddHouseBlend_Click(object? sender, RoutedEventArgs e)
    {
        _cart.Add(new HouseBlend());
        UpdateCartUI();
    }

    private void AddDecaf_Click(object? sender, RoutedEventArgs e)
    {
        _cart.Add(new Decaf());
        UpdateCartUI();
    }

    // ---------- Cart Actions ----------
    private void RemoveSelected_Click(object? sender, RoutedEventArgs e)
    {
        if (CartList.SelectedItem is Beverage selected)
        {
            _cart.Remove(selected);
            UpdateCartUI();
        }
    }

    private void ClearCart_Click(object? sender, RoutedEventArgs e)
    {
        _cart.Clear();
        UpdateCartUI();
    }

    // ---------- Tip Selection (Radio Buttons) ----------
    private void TipOption_Checked(object? sender, RoutedEventArgs e)
    {
        if (sender is not RadioButton rb) return;

        var tag = rb.Tag?.ToString() ?? "0";

        if (tag == "CUSTOM")
        {
            _customTipSelected = true;
            CustomTipBox.IsEnabled = true;
            // Rate not used for custom
            _selectedTipRate = 0.00m;
        }
        else
        {
            _customTipSelected = false;
            CustomTipBox.IsEnabled = false;

            // Clear custom tip when leaving custom mode
            CustomTipBox.Text = "";
            _customTipAmount = 0.00m;

            _selectedTipRate = tag switch
            {
                "10" => 0.10m,
                "15" => 0.15m,
                "20" => 0.20m,
                _ => 0.00m
            };
        }

        UpdateCartUI();
    }

    // Custom tip enabled only when Custom selected
    private void CustomTipBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (!_customTipSelected) return;

        if (string.IsNullOrWhiteSpace(CustomTipBox.Text))
        {
            _customTipAmount = 0.00m;
            UpdateCartUI();
            return;
        }

        // Parse safely
        if (decimal.TryParse(CustomTipBox.Text, out var val) && val >= 0)
        {
            _customTipAmount = val;
        }
        else
        {
            _customTipAmount = 0.00m;
        }

        UpdateCartUI();
    }

    // ---------- Place Order ----------
    private async void PlaceOrder_Click(object? sender, RoutedEventArgs e)
    {
        if (_cart.Count == 0)
        {
            await ShowMessage("Cart is empty", "Add at least one item before placing an order.");
            return;
        }

        var subtotal = _cart.Sum(x => x.Cost());
        var tax = subtotal * TaxRate;

        // Tip based on (subtotal + tax) as you requested
        var baseForTip = subtotal + tax;
        var tipChosen = _customTipSelected ? _customTipAmount : baseForTip * _selectedTipRate;

        var total = subtotal + tax + tipChosen;

        _cart.Clear();
        UpdateCartUI();

        await ShowMessage("Order Placed ✅", $"Thanks! Your total was ${total:0.00}.");
    }

    // ---------- UI Update ----------
    private void UpdateCartUI()
    {
        var subtotal = _cart.Sum(x => x.Cost());
        var tax = subtotal * TaxRate;

        // Tip is calculated on (subtotal + tax)
        var baseForTip = subtotal + tax;

        var tip0 = baseForTip * 0.00m;
        var tip10 = baseForTip * 0.10m;
        var tip15 = baseForTip * 0.15m;
        var tip20 = baseForTip * 0.20m;

        // show amounts under each tip option
        Tip0Amt.Text = $"${tip0:0.00}";
        Tip10Amt.Text = $"${tip10:0.00}";
        Tip15Amt.Text = $"${tip15:0.00}";
        Tip20Amt.Text = $"${tip20:0.00}";

        decimal tipChosen = _customTipSelected ? _customTipAmount : baseForTip * _selectedTipRate;
        var total = subtotal + tax + tipChosen;

        SubtotalText.Text = $"${subtotal:0.00}";
        TaxText.Text = $"${tax:0.00}";
        TotalText.Text = $"${total:0.00}";
    }

    // ---------- Dialog ----------
    private async Task ShowMessage(string title, string message)
    {
        var okButton = new Button
        {
            Content = "OK",
            Width = 90,
            CornerRadius = new Avalonia.CornerRadius(12),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
            IsDefault = true
        };

        var dialog = new Window
        {
            Title = title,
            Width = 420,
            Height = 180,
            Content = new StackPanel
            {
                Margin = new Avalonia.Thickness(16),
                Spacing = 12,
                Children =
                {
                    new TextBlock
                    {
                        Text = message,
                        FontSize = 14,
                        TextWrapping = TextWrapping.Wrap
                    },
                    okButton
                }
            }
        };

        okButton.Click += (_, __) => dialog.Close();

        dialog.KeyDown += (_, e) =>
        {
            if (e.Key == Key.Escape)
                dialog.Close();
        };

        await dialog.ShowDialog(this);
    }
}
