namespace MyCoffeeLtd.Models;

public sealed class Espresso : Beverage
{
    public Espresso() { Description = "Espresso Shot"; }
    protected override decimal BasePrice => 2.49m;
}
