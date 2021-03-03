using System;
using System.Net.Http;

namespace DemoBranch.Webapp.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var httpClient = new HttpClient();

            var apiClient = new swaggerClient("https://localhost:44310/", httpClient);

            // create a product
            var result = apiClient.DemoEventAllAsync().Result;

          




            foreach (var r in result)
            {
                Console.WriteLine(r.ToString());
            }

         


        }
    }
}
