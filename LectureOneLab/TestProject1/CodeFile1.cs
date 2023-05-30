using System;
using SeleniumBasicDemo;

namespace CodeFile1
{
    class Program
    {
        static void Main(string[] args)
        {
            var BankAccount = new BankAcount(1000);
            BankAccount.Deposit(200);

            var amount = BankAccount.Amount;
        }
    }
}