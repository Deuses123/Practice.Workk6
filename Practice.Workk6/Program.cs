using Library.Bankomat.Accounts;
using Library.Bankomat.Clients;
using Library.Bankomat.MyBank;

namespace Practice.Workk6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Для открытия счета в банке, введите некоторую информацию");
            Bank bank = new Bank();

            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            Client client = new Client(name, password);

            Console.WriteLine("Введите начальный баланс:");
            decimal initialBalance = ReadDecimal();

            Account account = bank.openAccount(client, initialBalance);
            client.LinkAccount(account);

            int attempts = 3;
            while (attempts > 0)
            {
                Console.Write("Введите пароль: ");
                string inputPassword = Console.ReadLine();


                if (inputPassword == client.Password)
                {
                    ShowMenu(client);
                    break;
                }
                else
                {
                    attempts--;
                    if (attempts > 0)
                    {
                        Console.WriteLine($"Неверный пароль. У вас осталось {attempts} попыток.");
                    }
                    else
                    {
                        Console.WriteLine("Исчерпаны все попытки. Попробуйте позже.");
                        Thread.Sleep(2500);
                    }
                }
            }
        }

       
        static decimal ReadDecimal()
        {
            decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Введите корректное число.");
            }
            return result;
        }

        static void ShowMenu(Client client)
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("a. Вывод баланса на экран");
                Console.WriteLine("b. Пополнение счета");
                Console.WriteLine("c. Снять деньги со счета");
                Console.WriteLine("d. Выход");
                Console.WriteLine("e. Очистить консоль");

                Console.Write("Выберите действие: ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case 'a':
                        Console.WriteLine($"Баланс: {client.Account.Balance}");
                        break;
                    case 'b':
                        Console.Write("Введите сумму для пополнения:");
                        decimal depositAmount = ReadDecimal();
                        client.Account.Deposit(depositAmount);
                        break;
                    case 'c':
                        Console.Write("Введите сумму:");
                        decimal withdrawAmount = ReadDecimal();
                        if (client.Account.Withdraw(withdrawAmount))
                        {
                            Console.WriteLine($"{withdrawAmount} снята со счета.");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно средств на счете или сумма снятия меньше нуля.");
                        }
                        break;
                    case 'd':
                        Console.WriteLine("Выход.");
                        return;
                    case 'e':
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Повторите попытку.");
                        break;
                }
            }
        }
    }
}
