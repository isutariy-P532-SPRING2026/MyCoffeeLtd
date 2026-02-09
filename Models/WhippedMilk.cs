
namespace MyCoffeeLtd.Models;

public class WhippedMilk : CondimentDecorator
{
    

    public WhippedMilk(Beverage beverage): base(beverage)
    {
        
    }

    public override string GetDescription()
    {
        return beverage.GetDescription() + ", Whipped Milk";
    }

    public override double Cost()
    {
        return beverage.Cost() + 0.35;
    }
}
