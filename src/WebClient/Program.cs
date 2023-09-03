using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Введите 1, если хотите вывести Клиента по Id, или 2, если хотите добавить Клиента на сервер, или 3, если хотите выйти из приложения.");

                var command = Console.ReadLine();

                try
                {
                    if (command == "1")
                    {

                        Console.WriteLine("Введите Id:");
                        var id = long.Parse(Console.ReadLine());
                        using var client = new WebApiClient();
                        var customer = await client.GetCustomerByIdAsync(id);
                        Console.WriteLine($"Customer: id = {customer.Id}, firstName = {customer.Firstname}, lastName = {customer.Lastname}");


                    }
                    else if (command == "2")
                    {
                        try
                        {
                            using var client = new WebApiClient();
                            var id = await client.AddCustomerAsync(RandomCustomer());
                            var customer = await client.GetCustomerByIdAsync(id);
                            Console.WriteLine($"Customer: id = {customer.Id}, firstName = {customer.Firstname}, lastName = {customer.Lastname}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    else if (command == "3")
                    {
                        Environment.Exit(-1);
                    }
                    else
                    {
                        Console.WriteLine("Вы выбрали некорректную команду.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели некорректные данные. Попробуйте ещё раз.");
                }
            }
        }

        private static CustomerCreateRequest RandomCustomer()
        {
            return new CustomerCreateRequest { Firstname = GenerateRandomString(), Lastname = GenerateRandomString() };
        }

        private static string GenerateRandomString()
        {
            var random = new Random();
            const string chars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            int length = random.Next(2, 16);

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}