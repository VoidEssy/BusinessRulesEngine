using DomainModels;
using System;

namespace Handlers
{
    public interface IHandler
    {
        bool CanHandle(Product prod);
        bool Handle(Product prod);
    }
}
