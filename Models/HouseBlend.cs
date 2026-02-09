namespace MyCoffeeLtd.Models;

public sealed class HouseBlend : Beverage
{
    public HouseBlend() { Description = "House Blend"; }
    public override decimal Cost() => 2.99m;
}
