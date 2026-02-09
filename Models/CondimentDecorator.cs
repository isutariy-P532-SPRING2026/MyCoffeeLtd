namespace MyCoffeeLtd.Models;

public abstract class CondimentDecorator : Beverage
{
    protected Beverage beverage;

    protected CondimentDecorator(Beverage beverage)
    {
        this.beverage = beverage;
    }
}
