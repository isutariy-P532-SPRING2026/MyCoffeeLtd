namespace MyCoffeeLtd.Models;

public sealed class DarkRoast : Beverage
{
    public DarkRoast() { Description = "Dark Roast"; }
    protected override decimal BasePrice => 3.19m;
}
