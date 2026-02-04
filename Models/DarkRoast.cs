namespace MyCoffeeLtd.Models;

public sealed class DarkRoast : Beverage
{
    public DarkRoast() { Description = "Dark Roast"; }
    public override decimal Cost() => 3.19m;
}
