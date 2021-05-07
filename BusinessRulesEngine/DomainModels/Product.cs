using System;

namespace DomainModels
{
    public class Product
    {
        public bool IsService { get; set; }
        public TypeEnum ProductType { get; set; }

        public Product(bool isService, TypeEnum productType)
        {
            IsService = isService;
            ProductType = productType;
        }

        public Product()
        {
        }

    }
}
