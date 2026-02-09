
namespace MyCoffeeLtd.Models;

public class SteamedMilk : CondimentDecorator
{
    

    public SteamedMilk(Beverage beverage): base(beverage)
    {
        
    }

    public override string GetDescription()
    {
        return beverage.GetDescription() + ", Steamed Milk";
    }

    public override double Cost()
    {
        return beverage.Cost() + 0.30;
    }
}
