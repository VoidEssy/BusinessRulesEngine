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
            products.Add("Physical", GetListOfPhysicalProducts());
            products.Add("Services", GetListOfServiceProducts());

            return products;
        }

        private static List<Product> GetListOfPhysicalProducts()
        {
            return new List<Product>
            {
                new Product(false, "Book", "Animal Farm", "Purchase"),
                new Product(false, "Book", "1984", "Purchase"),
                new Product(false, "Video", "Terminator 10", "Purchase"),
                new Product(false, "Book", "Animal Farm", "Purchase"),
            };
        }

        private static List<Product> GetListOfServiceProducts()
        {
            return new List<Product>
            {
                new Product(true, "Membership", "User Usersen", "Activation"),
                new Product(true, "Membership", "User Usersen", "Upgrade"),
                new Product(true, "Video", "Learning to Ski", "Purchase"),
                new Product(true, "Book", "Animal Farm", "Purchase"),
            };
        }
    }

}
