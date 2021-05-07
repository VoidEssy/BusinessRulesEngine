using System;

namespace DomainModels
{
    public class Product
    {
        public bool IsService { get; set; }
        public string ProductType { get; set; }

        public Product(bool isService, string productType)
        {
            IsService = isService;
            ProductType = productType;
        }

        public Product()
        {
        }

    }
}
