using System;

namespace ElectricityBoardBilling.Components
{
    public class ElectricityBill
    {
        private string consumerNumber;
        private string consumerName;
        private int unitsConsumed;
        private double billAmount;

        public string ConsumerNumber
        {
            get { return consumerNumber; }
            set { consumerNumber = value; }
        }

        public string ConsumerName
        {
            get { return consumerName; }
            set { consumerName = value; }
        }

        public int UnitsConsumed
        {
            get { return unitsConsumed; }
            set { unitsConsumed = value; }
        }

        public double BillAmount
        {
            get { return billAmount; }
            set { billAmount = value; }
        }
    }
}