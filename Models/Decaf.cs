
namespace MyCoffeeLtd.Models;

public class Decaf : Beverage
{
    public override string GetDescription()
    {
        return "Decaf Coffee";
    }

    public override double Cost()
    {
        return 2.79;
    }
}
