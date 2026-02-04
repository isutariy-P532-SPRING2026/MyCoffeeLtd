namespace MyCoffeeLtd.Models;

public sealed class Espresso : Beverage
{
    public Espresso() { Description = "Espresso Shot"; }
    public override decimal Cost() => 2.49m;
}
