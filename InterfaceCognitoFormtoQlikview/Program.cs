using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using InterfaceSingTaxitoSQL.Class;

namespace InterfaceSingTaxitoSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            // initiate database connection
            var connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

            // initiate function to collect data
            var synch = new SynchronizetoSingTaxiAPI();

            const string url = "https://api.data.gov.sg/v1/";

            try
            {
                // function to collect data
                synch.SynchronizeSingTaxiResultToStaging(url);

            }
            catch (Exception aa)
            {

                Console.WriteLine("Error : " + aa.Message.ToString());
            }

        }
    }
}
