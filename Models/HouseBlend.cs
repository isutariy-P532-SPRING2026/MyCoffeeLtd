
namespace MyCoffeeLtd.Models;

public class HouseBlend : Beverage
{
    public override string GetDescription()
    {
        return "House Blend";
    }

    public override double Cost()
    {
        return 2.99;
    }
}
