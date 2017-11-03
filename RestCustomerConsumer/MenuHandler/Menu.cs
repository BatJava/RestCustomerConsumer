using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestCustomerConsumer.Model;
using RestCustomerConsumer.Controllers;

namespace RestCustomerConsumer.MenuHandler
{
    public class Menu
    {

        public static void MainMenu()
        {
            Boolean status = true;

            while (status)
            {
                Console.WriteLine("Give your choice 1-6, 9:");
                Console.WriteLine("1: Get all customers");
                Console.WriteLine("2: Get one customer by id");
                Console.WriteLine("3: Delete a customer by id");
                Console.WriteLine("4: Add a new customer");
                Console.WriteLine("5: Update a customer");
                Console.WriteLine("9: Exit program");

                ConsoleKeyInfo key = Console.ReadKey();  //måske char key
                Console.WriteLine("Key:" + key);

                if (char.IsDigit(key.KeyChar))
                {
                    int choice = Convert.ToInt32(key.KeyChar) - 48;

                    Console.WriteLine("choice:" + choice);
                    switch (choice)
                    {
                        case 1:
                            menu1(); break;
                        case 2:
                            menu2(); break;
                        case 3:
                            menu3(); break;
                        case 4:
                            menu4();break;
                        case 5:
                            menu5();
                            break;
                        case 6:
                            menu6();
                            break;
                        case 9:
                            status = false;
                            break;
                        default:
                            Console.WriteLine("Sorry wrong number not (1-5, 9) ");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nSorry, you need to input a number");
                }
            }
        }

        private static void menu1()
        {
            IList<Customer> cList = TaskController.GetCustomersAsync().Result;
            Console.WriteLine(string.Join("\n", cList.ToString()));
            //Fast write out
            for (int i = 0; i < cList.Count; i++)
                Console.WriteLine(cList[i].ToString());
            Console.WriteLine();
        }


        private static void menu2()
        {
            try
            {
                Console.WriteLine("GET Give an id of customer to be found. See exception if not found");
                string idStr = Console.ReadLine();
                int id = int.Parse(idStr);
                Customer customer = TaskController.GetOneCustomerAsync1(id).Result;
                Console.WriteLine(customer.ToString());
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Console.WriteLine(ex.Message);
            }

        }


        private static void menu3()
        {
            Console.WriteLine("DELETE: Give an id of customer to be deleted");
            String idStr = Console.ReadLine();
            int id = int.Parse(idStr);
            Customer customer = TaskController.DeleteOneCustomerAsync(id).Result;
            Console.WriteLine(customer.ToString() + "\n");
        }

        private static void menu4()
        {
            Console.WriteLine("POST: Give data for customer to be inserted");
            Console.WriteLine("First Name: ");
            String first = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            String last = Console.ReadLine();
            Console.WriteLine("Year: ");
            String yearStr = Console.ReadLine();
            int year = Int32.Parse(yearStr);

            Customer newCustomer = new Customer(first, last, year);
            Customer customer = TaskController.AddCustomerAsync(newCustomer).Result;
            Console.WriteLine("Customer inserted");
            Console.WriteLine(customer.ToString());
        }

        private static void menu5()
        {
            Console.WriteLine("PUT Give id of customer to be updated");
            string idStr = Console.ReadLine();
            int id = int.Parse(idStr);
            Customer customer = TaskController.GetOneCustomerAsync(id).Result;
            Console.WriteLine("This customer will be updated:\n" + customer.ToString());

            Console.WriteLine("First Name: ");
            customer.FirstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            customer.LastName = Console.ReadLine();
            Console.WriteLine("Year: ");
            String yearStr = Console.ReadLine();
            customer.Year = Int32.Parse(yearStr);

            Customer customer1 = TaskController.UpdateCustomerAsync(customer,id).Result;
            Console.WriteLine("Customer updated");
            Console.WriteLine(customer1.ToString());

        }

        private static void menu6()
        {

        }

    }
}


