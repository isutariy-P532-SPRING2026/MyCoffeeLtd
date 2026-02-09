namespace MyCoffeeLtd.Models;

public class Espresso : Beverage
{
    public override string GetDescription()
    {
        return "Espresso";
    }

    public override double Cost()
    {
        return 1.99;
    }
}
