using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ElectricityBoardBilling.Components
{
    public class ElectricityBoard
    {
        public void CalculateBill(ElectricityBill ebill)
        {
            int units = ebill.UnitsConsumed;
            double amount = 0;

            if (units <= 100) amount = 0;
            else if (units <= 300) amount = (units - 100) * 1.5;
            else if (units <= 600) amount = 200 * 1.5 + (units - 300) * 3.5;
            else if (units <= 1000) amount = 200 * 1.5 + 300 * 3.5 + (units - 600) * 5.5;
            else amount = 200 * 1.5 + 300 * 3.5 + 400 * 5.5 + (units - 1000) * 7.5;

            ebill.BillAmount = amount;
        }

        public void AddBill(ElectricityBill ebill)
        {
            using (SqlConnection conn = DBHandler.GetConnection())
            {
                string query = "INSERT INTO ElectricityBill(consumer_number, consumer_name, units_consumed, bill_amount) VALUES(@num, @name, @units, @amount);";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@num", ebill.ConsumerNumber);
                cmd.Parameters.AddWithValue("@name", ebill.ConsumerName);
                cmd.Parameters.AddWithValue("@units", ebill.UnitsConsumed);
                cmd.Parameters.AddWithValue("@amount", ebill.BillAmount);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            List<ElectricityBill> bills = new List<ElectricityBill>();
            using (SqlConnection conn = DBHandler.GetConnection())
            {
                string query = "SELECT * FROM ElectricityBill ORDER BY entry_at ASC;";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ElectricityBill eb = new ElectricityBill
                    {
                        ConsumerNumber = reader["consumer_number"].ToString(),
                        ConsumerName = reader["consumer_name"].ToString(),
                        UnitsConsumed = Convert.ToInt32(reader["units_consumed"]),
                        BillAmount = Convert.ToDouble(reader["bill_amount"])
                    };
                    bills.Add(eb);
                }
            }

            bills.Reverse();

            if (num > bills.Count)
            {
                throw new Exception($"Only {bills.Count} bill(s) available. You requested {num}.");
            }

            return bills.GetRange(0, num);
        }
    }
}