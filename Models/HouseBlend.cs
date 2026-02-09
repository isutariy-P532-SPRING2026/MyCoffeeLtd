namespace MyCoffeeLtd.Models;

public sealed class HouseBlend : Beverage
{
    public HouseBlend() { Description = "House Blend"; }
    protected override decimal BasePrice => 2.99m;
}
