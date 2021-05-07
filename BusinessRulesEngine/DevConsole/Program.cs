using DomainModels;
using RuleEngine.Models;
using System;
using System.Collections.Generic;

namespace DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Here will be a theoretical simplified execution of whole system simulated, goal is to show the engine in action and not create a whole system.
            
            Rule physicalProduct = new Rule("IsService", "Equal", "false");

        }

        private static Dictionary<string, List<Product>> GenerateProducts()
        {
            Dictionary<string, List<Product>> products = new Dictionary<string, List<Product>>();
            products.Add("Physical", null);
            products.Add("Services", null);

            return products;
        }

        private static List<Product> GetListOfPhysicalProducts()
        {

        }

        private static List<Product> GetListOfServiceProducts()
        {

        }
    }

}
