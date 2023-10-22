using Library.Bankomat.Accounts;
using Library.Bankomat.MyBank;
using Library.Bankomat.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    namespace Bankomat
    {


        namespace MyBank
        {

            public class Bank
            {
                
                private string accountNumber;
                private decimal initialBalance;

                public Account openAccount(Client client, decimal initialBalance)
                {
                    Random random = new();
                    string accountNumber = random.Next().ToString();
                    
                    Account newAccount = new Account(accountNumber, initialBalance);

                    client.Account = newAccount;

                    Console.WriteLine($"\nСчет: {accountNumber}\nИмя: {client.Name}\nБаланс: {initialBalance}\n");

                    return newAccount;
                }
            }
        }

        namespace Clients
        {

            public class Client
            {
                public string Name { get; set; }
                public byte[] CardNumber { get; set; }
                public string Password { get; set; }
                public Account Account { get; set; }

                public Client(string name, string password)
                {
                    Name = name;
                    CardNumber = Guid.NewGuid().ToByteArray();
                    Password = password;
                }

                public Client()
                {
                }

               
                public void LinkAccount(Account account)
                {
                    Account = account;
                }
            }
        }

        namespace Accounts
        {

            public class Account
            {
                public decimal Balance { get; private set; }

                public Account(string accountNumber, decimal initialBalance)
                {
                    Balance = initialBalance;
                }

                public void Deposit(decimal amount)
                {
                    if (amount > 0)
                    {
                        Balance += amount;
                        Console.WriteLine($"Пополнено {amount}\nБаланс: {Balance}");
                    }
                    else
                    {
                        Console.WriteLine("rules: Ammount > 0.");
                    }
                }

                public bool Withdraw(decimal amount)
                {
                    if (amount > 0 && Balance >= amount)
                    {
                        Balance -= amount;
                        Console.WriteLine($"{amount} тг снято со счета. \nБаланс: {Balance}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств");
                        return false;
                    }
                }
            }
        }
    }
}