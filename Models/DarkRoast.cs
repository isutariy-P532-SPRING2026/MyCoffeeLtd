
namespace MyCoffeeLtd.Models;

public class DarkRoast : Beverage
{
    public override string GetDescription()
    {
        return "Dark Roast";
    }

    public override double Cost()
    {
        return 3.19;
    }
}
