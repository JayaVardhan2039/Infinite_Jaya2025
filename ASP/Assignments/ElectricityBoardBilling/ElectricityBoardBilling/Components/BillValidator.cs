using System;
using ElectricityBoardBilling;

namespace ElectricityBoardBilling.Components
{
    public class BillValidator
    {
        public string ValidateUnitsConsumed(int units)
        {
            if (units < 0)
            {
                return "Units consumed cannot be negative.";
            }
            else if (units > 10000)
            {
                return "Units consumed exceeds maximum allowed limit.";
            }
            else
            {
                return "Valid";
            }
        }
    }
}