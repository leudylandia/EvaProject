using ConsoleApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
           InvokeAPI();
            //Invoke2();
        }

        private static async void InvokeAPIAsync()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("http://localhost:5050/");

            var request = await http.GetAsync("api/pais");

            if (request.IsSuccessStatusCode)
            {
                var response = await request.Content.ReadAsStringAsync();
            }
        }

        private static void InvokeAPI()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("http://localhost:5050/");

            var request = http.GetAsync("api/pais").Result;


            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<List<Country>>(responseJson.ToString());

               

                //Console.WriteLine(responseJson);
                //Console.ReadLine();

                foreach (var item in response)
                {
                    Console.WriteLine(item.pais.ToString());
                }
                Console.ReadLine();
            }
        }

        private static void Invoke2()
        {
            string file = File.ReadAllText(@"C:\Users\Papa\source\repos\PracticandoParaEvaluacion\Evaluacion1\ConsoleApp1\oj.json");
            var response = JsonConvert.DeserializeObject<List<Country>>(file);

            foreach (var item in response)
            {
                Console.WriteLine(item.pais.Name);
            }
            Console.ReadLine();
        }
    }
}
