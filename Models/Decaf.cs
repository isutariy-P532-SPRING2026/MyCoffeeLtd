namespace MyCoffeeLtd.Models;

public sealed class Decaf : Beverage
{
    public Decaf() { Description = "Decaf Coffee"; }
    protected override decimal BasePrice => 2.79m;
}
