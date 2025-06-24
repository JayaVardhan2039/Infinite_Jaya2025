using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    /// <summary>
    /// 1. Create a class called Accounts which has data members/fields like Account no, Customer name, Account type, Transaction type (d/w), amount, balance
    //D->Deposit
    //W->Withdrawal

    //-write a function that updates the balance depending upon the transaction type

    //	-If transaction type is deposit call the function credit by passing the amount to be deposited and update the balance

    //  function Credit(int amount)

    //	-If transaction type is withdraw call the function debit by passing the amount to be withdrawn and update the balance

    //  Debit(int amt) function 

    //-Pass the other information like Account no, customer name, acc type through constructor

    //-write and call the show data method to display the values.
    /// </summary>
  
    class Accounts
    {
        
        int AccountNo;
        string CustomerName;
        string AccountType;
        float Balance, Amount;
        char TransactionType;

        public Accounts(int accNo, string custName, string accType, float initialBalance)
        {
            AccountNo = accNo;
            CustomerName = custName;
            AccountType = accType;
            Balance = initialBalance;
            Console.WriteLine("Initial Details");
            ShowData();
            lisa: Console.WriteLine("Enter 'D for Deposit and 'W' for Withdraw and 'S' to Stop Transactions" );
            TransactionType = Convert.ToChar(Console.ReadLine());
            if(TransactionType=='D')
            {
                Console.WriteLine("Enter the amount : ");
                Amount=float.Parse(Console.ReadLine());
                Credit(Amount);
                goto lisa;
            }
            if(TransactionType=='W')
            {
                Console.WriteLine("Enter the amount : ");
                Amount = float.Parse(Console.ReadLine());
                Debit(Amount);
                goto lisa;
            }

            
        }
        public void Credit(float amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Amount Deposited: {amount}. Updated Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
            ShowData();
        }

        public void Debit(float amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Amount Withdrawn: {amount}. Updated Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or insufficient balance.");
            }
            ShowData();
        }


        public void ShowData()
        {
            Console.WriteLine("Account Details:");
            Console.WriteLine($"Account No: {AccountNo}");
            Console.WriteLine($"Customer Name: {CustomerName}");
            Console.WriteLine($"Account Type: {AccountType}");
            Console.WriteLine($"Balance: {Balance}");
        }
    }

    class Tester_Accounts
    {
        static void Main()
        {
            Console.WriteLine("Question 1");
            Console.WriteLine();
            Accounts account = new Accounts(667698, "Jaya", "Current Savings", 5000);
            
            Console.Read();
        }
    }

}
