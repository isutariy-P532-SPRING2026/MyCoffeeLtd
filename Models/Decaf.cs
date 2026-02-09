namespace MyCoffeeLtd.Models;

public sealed class Decaf : Beverage
{
    public Decaf() { Description = "Decaf Coffee"; }
    public override decimal Cost() => 2.79m;
}
