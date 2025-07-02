using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{

    /*
     * 1.
•	You have a class which has methods for transaction for a banking system. (created earlier)
•	Define your own methods for deposit money, withdraw money and balance in the account.
•	Write your own new application Exception class called InsuffientBalanceException. 
•	This new Exception will be thrown in case of withdrawal of money from the account where customer doesn’t have sufficient balance.
•	Identify and categorize all possible checked and unchecked exception.
     */
    // Custom Checked Exception
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    // Custom Unchecked Exception
    public class InvalidInputException : ApplicationException
    {
        public InvalidInputException(string message) : base(message) { }
    }

    class Banking_System
    {
        int AccountNo;
        string CustomerName;
        string AccountType;
        float Balance;

        public Banking_System(int accNo, string custName, string accType, float initialBalance)
        {
            if (string.IsNullOrWhiteSpace(custName) || string.IsNullOrWhiteSpace(accType))
                throw new InvalidInputException("Customer name or account type cannot be empty.");

            if (initialBalance < 0)
                throw new InvalidInputException("Initial balance cannot be negative.");

            AccountNo = accNo;
            CustomerName = custName;
            AccountType = accType;
            Balance = initialBalance;
        }

        public void Deposit(float amount)
        {
            if (amount <= 0)
                throw new InvalidInputException("Deposit amount must be greater than zero.");

            Balance += amount;
            Console.WriteLine($"Deposited: {amount}. New Balance: {Balance}");
        }

        public void Withdraw(float amount)
        {
            if (amount <= 0)
                throw new InvalidInputException("Withdrawal amount must be greater than zero.");

            if (amount > Balance)
                throw new InsufficientBalanceException("Insufficient balance for withdrawal.");

            Balance -= amount;
            Console.WriteLine($"Withdrawn: {amount}. New Balance: {Balance}");
        }

        public void ShowData()
        {
            Console.WriteLine("\n..................Account Details:......................");
            Console.WriteLine($"Account No: {AccountNo}");
            Console.WriteLine($"Customer Name: {CustomerName}");
            Console.WriteLine($"Account Type: {AccountType}");
            Console.WriteLine($"Balance: {Balance}");
        }

        public static void Main()
        {
            Console.WriteLine("Welcome to the Code Banking");
            try
            {
                Console.WriteLine("Enter Account Number:");
                if (!int.TryParse(Console.ReadLine(), out int accNo))
                    throw new InvalidInputException("Account number must be a valid integer.");

                Console.WriteLine("Enter Customer Name:");
                string custName = Console.ReadLine();

                Console.WriteLine("Enter Account Type:");
                string accType = Console.ReadLine();

                Console.WriteLine("Enter Initial Balance:");
                if (!float.TryParse(Console.ReadLine(), out float initialBalance))
                    throw new InvalidInputException("Initial balance must be a valid number.");

                Banking_System account = new Banking_System(accNo, custName, accType, initialBalance);
                account.ShowData();

                while (true)
                {
                    Console.WriteLine("\nEnter Transaction Type (D=Deposit, W=Withdraw, S=Stop):");
                    char transactionType = Convert.ToChar(Console.ReadLine().ToUpper());

                    if (transactionType == 'S')
                    {
                        Console.WriteLine("Thanks for the System Banking");
                        break;
                    }

                    Console.WriteLine("Enter Amount:");
                    if (!float.TryParse(Console.ReadLine(), out float amount))
                        throw new InvalidInputException("Amount must be a valid number.");

                    switch (transactionType)
                    {
                        case 'D':
                            account.Deposit(amount);
                            break;
                        case 'W':
                            account.Withdraw(amount);
                            break;
                        default:
                            Console.WriteLine("Invalid transaction type.");
                            break;
                    }

                    account.ShowData();
                }
            }
            catch (InsufficientBalanceException except)
            {
                Console.WriteLine($"Transaction Error: {except.Message}");
            }
            catch (InvalidInputException except)
            {
                Console.WriteLine($"Input Error: {except.Message}");
            }
            catch (Exception except)
            {
                Console.WriteLine($"Unexpected Error: {except.Message}");
            }

            Console.WriteLine("Thank you for using the banking system.");
            Console.Read();
        }
    }
}
