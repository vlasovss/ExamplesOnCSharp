using System;
using BankLibrary;

using static System.Console;

namespace BankApplication
{
    class Program
    {
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            WriteLine(e.Message);
            if (e.Sum > 0) { WriteLine("Идем тратить деньги."); }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(ReadLine());
            WriteLine("Выберите тип счета: 1. До востребования; 2. Депозит.");

            AccountType accountType;
            int type = Convert.ToInt32(ReadLine());

            if (type == 2) { accountType = AccountType.Deposit; }
            else { accountType = AccountType.Ordinary; }

            bank.Open(accountType,
                sum,
                AddSumHandler,
                WithdrawSumHandler,
                (o, e) => WriteLine(e.Message),
                CloseAccountHandler,
                OpenAccountHandler);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            WriteLine("Введите Id счета, который нужно закрыть:");
            int id = Convert.ToInt32(ReadLine());

            bank.Close(id);
        }

        private static void Put(Bank<Account> bank)
        {
            WriteLine("Укажите сумма, чтобы положить на счета");
            decimal sum = Convert.ToDecimal(ReadLine());

            WriteLine("Введите Id:");
            int id = Convert.ToInt32(ReadLine());

            bank.Put(sum, id);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            WriteLine("Укажите сумма для вывода со счета:");
            decimal sum = Convert.ToDecimal(ReadLine());

            WriteLine("Введите Id:");
            int id = Convert.ToInt32(ReadLine());

            bank.Withdraw(sum, id);            
        }

        private static void Main(string[] args)
        {
            Bank<Account> Unitbank = new Bank<Account>("ЮнитБанк");

            bool alive = true;
            while (alive)
            {
                ConsoleColor color = ForegroundColor;
                ForegroundColor = ConsoleColor.DarkGreen;
                WriteLine("1. Открыть счет. \t 2. Вывести средства. \t 3. Добавить на счет.");
                WriteLine("4. Закрыть счет. \t 5. Пропустить день. \t 6. Выйти из программы.");
                WriteLine("Введите номер пункта: ");
                ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(ReadLine());

                    switch (command)
                    {
                        case 1:
                            {
                                OpenAccount(Unitbank);
                                break;
                            }
                        case 2:
                            {
                                Withdraw(Unitbank);
                                break;
                            }
                        case 3:
                            {
                                Put(Unitbank);
                                break;
                            }
                        case 4:
                            {
                                CloseAccount(Unitbank);
                                break;
                            }
                        case 5:
                            {
                                break;
                            }
                        case 6:
                            {
                                alive = false;
                                continue;
                            }
                    }
                    Unitbank.CalculatePercentage();
                }
                catch (Exception exeption)
                {
                    color = ForegroundColor;
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(exeption.Message);
                    ForegroundColor = color;
                }
            }
        }
    }
}
