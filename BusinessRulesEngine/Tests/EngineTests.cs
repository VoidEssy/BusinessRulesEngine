using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEngine;
using RuleEngine.Models;

namespace Tests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void EngineSingleRuleBuildTest()
        {
            Product prod = new Product(true, TypeEnum.Book );
            Rule basicRule = new Rule("IsService", "Equal", "true");
            var myRule = Engine.CompileRule<Product>(basicRule, prod);
            //var result = myRule.;
            //Assert.IsTrue(myRule.Invoke());
        }
    }
}
