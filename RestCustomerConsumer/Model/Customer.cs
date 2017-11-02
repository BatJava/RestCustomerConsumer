using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestCustomerConsumer.Model
{
   
    public class Customer
    {    
        public int ID { get; set; }   
        public String FirstName { get; set; }    
        public String LastName { get; set; }

       
        public int Year { get; set; }
        public Customer()
        { //Start data generation
        }

        public Customer(String firstName)
        {
            this.ID = 0;
            this.FirstName = firstName;
        }

        public Customer(String firstName, String lastName, int year)
        {
            this.ID = 0;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Year = year;
        }

        public Customer(int id, String firstName, String lastName, int year)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Year = year;
        }
        public override string ToString()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append(ID); sb.Append(", ");
            sb.Append(FirstName); sb.Append(", ");
            sb.Append(LastName); sb.Append(", ");
            sb.Append(Year.ToString()); sb.Append(", ");
            return sb.ToString();

        }

    }
}
