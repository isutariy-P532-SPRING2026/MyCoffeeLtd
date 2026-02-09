namespace MyCoffeeLtd.Models;

public class BeverageViewModel
{
    private readonly Beverage _beverage;

    public BeverageViewModel(Beverage beverage)
    {
        _beverage = beverage;
    }

    public string Description => _beverage.GetDescription();
    public double Price => _beverage.Cost();
}
