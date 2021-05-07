using DomainModels;
using RuleEngine;
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
            Rule physicalOrder = new Rule("IsService", "Equal", "false");
            Rule serviceOrder = new Rule("IsService", "Equal", "true");
            Rule bookOrder = new Rule("ProductType", "Equal", "Book");
            Rule membershipOrder = new Rule("ProductType", "Equal", "Membership");
            Rule upgradeOrder = new Rule("Operation", "Equal", "Upgrade");
            Rule purchaseOrder = new Rule("Operation", "Equal", "Purchase");

            foreach (Product order in GenerateProducts())
            {
                if (Engine.CompileRule<Product>(physicalOrder).Invoke(order))
                {
                    Console.WriteLine($"Packaging slip generated for {order.Name}");
                }
                else if (Engine.CompileRule<Product>(serviceOrder).Invoke(order))
                {
                    Console.WriteLine("This is a service");
                }
            }



        }

        private static List<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product(false, "Book", "Animal Farm", "Purchase"),
                new Product(false, "Book", "1984", "Purchase"),
                new Product(false, "Video", "Terminator 10", "Purchase"),
                new Product(false, "Book", "Animal Farm", "Purchase"),
                new Product(true, "Membership", "User Usersen", "Activation"),
                new Product(true, "Membership", "User Usersen", "Upgrade"),
                new Product(true, "Video", "Learning to Ski", "Purchase"),
                new Product(true, "Book", "Animal Farm", "Purchase"),
            };
        }
    }

}
