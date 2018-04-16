using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestCustomerConsumer.Model;
using System.Net;
using Newtonsoft.Json;

namespace RestCustomerConsumer.Controllers
{
    public class TaskController
    {
       // local server used private const string CustomersUri = "http://localhost:19844/Service1.svc/customers/";
        private const string CustomersUri = "http://restcustomerwebservice.azurewebsites.net/CustomerService.svc/customers";

        public TaskController() {    }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public static async Task<IList<Customer>> GetCustomersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CustomersUri);
                IList<Customer> cList = JsonConvert.DeserializeObject<IList<Customer>>(content);
                return cList;
            }
        }

        public static async Task<Customer> GetOneCustomerAsync(int id)
        {
            string requestUri = CustomersUri + "/" + id;
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(requestUri);
                Customer c = JsonConvert.DeserializeObject<Customer>(content);
                return c;
            }
        }

        public static async Task<Customer> GetOneCustomerAsync1(int id)
        {
            string requestUri = CustomersUri + "/" + id;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Customer not found. Try another id");

                }
                response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                Customer c = JsonConvert.DeserializeObject<Customer>(str);
                return c;
            }
        }
        public static async Task<Customer> DeleteOneCustomerAsync(int id)
        {
            string requestUri = CustomersUri + id;

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.DeleteAsync(requestUri);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Customer not found. Try another id");

                }
                response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                Customer deletedCustomer = JsonConvert.DeserializeObject<Customer>(str);
                return deletedCustomer;

            }
        }


        public static async Task<Customer> AddCustomerAsync(Customer newCustomer)
        {
            using (HttpClient client = new HttpClient())
            {
       
                var jsonString = JsonConvert.SerializeObject(newCustomer);
                Console.WriteLine("JSON: " + jsonString);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(CustomersUri, content);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("Customer already exists. Try another id");
                }
                response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                Customer copyOfNewCustomer = JsonConvert.DeserializeObject<Customer>(str);
                return copyOfNewCustomer;
            }

        }

        public static async Task<Customer> UpdateCustomerAsync(Customer newCustomer, int id)
        {
            
           
            using (HttpClient client = new HttpClient())
            {
                string requestUri = CustomersUri + id;
                var jsonString = JsonConvert.SerializeObject(newCustomer);
                Console.WriteLine("JSON: " + jsonString);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(requestUri, content);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("Customer already exists. Try another id");
                }
                //response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                Customer updCustomer = JsonConvert.DeserializeObject<Customer>(str);
                return updCustomer;
            }
        }

        //Altenative using the built in method: PostAsJsonAsync 
        //private static async Task<Customer> AddCustomerAsyncAsJson(Customer newCustomer)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.PostAsJsonAsync(CustomersUri, newCustomer);
        //        if (response.StatusCode == HttpStatusCode.Conflict)
        //        {
        //            throw new Exception("Customer already exists. Try another id");
        //        }
        //        response.EnsureSuccessStatusCode();
        //        string str = await response.Content.ReadAsStringAsync();
        //        Customer copyOfNewCustomer = JsonConvert.DeserializeObject<Customer>(str);

        //        return copyOfNewCustomer;
        //    }
        // }

        public static async Task<string> printTest()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CustomersUri);
                IList<Customer> cList = JsonConvert.DeserializeObject<IList<Customer>>(content);

                for (int i = 0; i < cList.Count; i++)
                    Console.WriteLine(cList[i].ToString());
                Console.WriteLine();
            }

            return null;


        }







    }
}
