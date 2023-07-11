using System.ComponentModel;

namespace TestCase.Domain.Enums
{
    public enum SideRightTypes
    {
        [Description("Travel Expenses")]
        TravelExpenses = 1,
        [Description("Food Expenses")]
        FoodExpenses = 2,
        [Description("Private Health Insurance")]
        PrivateHealthInsurance = 3
    }
}
