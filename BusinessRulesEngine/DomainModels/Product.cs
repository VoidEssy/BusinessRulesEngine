using System;

namespace DomainModels
{
    public class Product
    {
        public bool IsService { get; set; }
        public string ProductType { get; set; }
        public string  Name { get; set; }
        public string Operation { get; set; }

        public Product(bool isService, string productType, string name, string operation)
        {
            IsService = isService;
            ProductType = productType;
            Name = name;
            Operation = operation;
        }

        public Product()
        {
        }

    }
}
