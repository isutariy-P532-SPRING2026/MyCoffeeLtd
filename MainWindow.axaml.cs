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

    // IMPORTANT CHANGE:
    // UI works with BeverageViewModel, not Beverage
    private readonly ObservableCollection<BeverageViewModel> _cart = new();

    private decimal _selectedTipRate = 0.00m;
    private bool _customTipSelected = false;
    private decimal _customTipAmount = 0.00m;

    public MainWindow()
    {
        InitializeComponent();

        CartList.ItemsSource = _cart;
        CustomTipBox.IsEnabled = false;

        UpdateCartUI();
    }

    // ---------- Add Items ----------
    private void AddEspresso_Click(object? sender, RoutedEventArgs e)
        => AddToCart(new Espresso());

    private void AddDarkRoast_Click(object? sender, RoutedEventArgs e)
        => AddToCart(new DarkRoast());

    private void AddHouseBlend_Click(object? sender, RoutedEventArgs e)
        => AddToCart(new HouseBlend());

    private void AddDecaf_Click(object? sender, RoutedEventArgs e)
        => AddToCart(new Decaf());

    // ---------- Cart Actions ----------
    private void RemoveSelected_Click(object? sender, RoutedEventArgs e)
    {
        if (CartList.SelectedItem is BeverageViewModel selected)
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

    // ---------- Tip Selection ----------
    private void TipOption_Checked(object? sender, RoutedEventArgs e)
    {
        if (sender is not RadioButton rb) return;

        var tag = rb.Tag?.ToString() ?? "0";

        if (tag == "CUSTOM")
        {
            _customTipSelected = true;
            CustomTipBox.IsEnabled = true;
            _selectedTipRate = 0.00m;
        }
        else
        {
            _customTipSelected = false;
            CustomTipBox.IsEnabled = false;
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

    private void CustomTipBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (!_customTipSelected) return;

        if (decimal.TryParse(CustomTipBox.Text, out var val) && val >= 0)
            _customTipAmount = val;
        else
            _customTipAmount = 0.00m;

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

        var subtotal = _cart.Sum(x => (decimal)x.Price);
        var tax = subtotal * TaxRate;

        var baseForTip = subtotal + tax;
        var tipChosen = _customTipSelected ? _customTipAmount : baseForTip * _selectedTipRate;

        var total = subtotal + tax + tipChosen;

        _cart.Clear();
        UpdateCartUI();

        await ShowMessage("Order Placed ✅", $"Thanks! Your total was ${total:0.00}.");
    }

    // ---------- Decorator Application ----------
    private void AddToCart(Beverage beverage)
    {
        if (ChkSteamedMilk.IsChecked == true)
            beverage = new SteamedMilk(beverage);

        if (ChkSoy.IsChecked == true)
            beverage = new Soy(beverage);

        if (ChkMocha.IsChecked == true)
            beverage = new Mocha(beverage);

        if (ChkChocolate.IsChecked == true)
            beverage = new Chocolate(beverage);

        if (ChkWhippedMilk.IsChecked == true)
            beverage = new WhippedMilk(beverage);

        // IMPORTANT CHANGE:
        _cart.Add(new BeverageViewModel(beverage));

        ChkSteamedMilk.IsChecked = false;
        ChkSoy.IsChecked = false;
        ChkMocha.IsChecked = false;
        ChkChocolate.IsChecked = false;
        ChkWhippedMilk.IsChecked = false;

        UpdateCartUI();
    }

    // ---------- UI Update ----------
    private void UpdateCartUI()
    {
        var subtotal = _cart.Sum(x => (decimal)x.Price);
        var tax = subtotal * TaxRate;

        var baseForTip = subtotal + tax;

        Tip0Amt.Text = $"${baseForTip * 0.00m:0.00}";
        Tip10Amt.Text = $"${baseForTip * 0.10m:0.00}";
        Tip15Amt.Text = $"${baseForTip * 0.15m:0.00}";
        Tip20Amt.Text = $"${baseForTip * 0.20m:0.00}";

        var tipChosen = _customTipSelected ? _customTipAmount : baseForTip * _selectedTipRate;
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
