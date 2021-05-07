using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEngine;
using RuleEngine.Models;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void EngineBooleanRuleBuildTest()
        {
            Product prod = new Product(true, "Book");
            Rule basicRule = new Rule("IsService", "Equal", "true");
            var TestRule = Engine.CompileRule<Product>(basicRule, prod);
            var result = TestRule.Invoke(prod);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EngineEnumRuleBuildTest()
        {
            Product prod = new Product(true, "Book");
            Rule basicRule = new Rule("ProductType", "Equal", "Book");
            var TestRule = Engine.CompileRule<Product>(basicRule, prod);
            var result = TestRule.Invoke(prod);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EngineRuleSetBuildTest()
        {
            List<Rule> rules = new List<Rule> { new Rule("IsService", "Equal", "true"), new Rule("ProductType", "Equal", "Book") };
            Product prod = new Product(true, "Book");
            var rule1 = Engine.CompileRule<Product>(rules[0], prod);
            var rule2 = Engine.CompileRule<Product>(rules[1], prod);
            var result1 = rule1.Invoke(prod);
            var result2 = rule2.Invoke(prod);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void EngineRuleSetMultiEvaluationBuildTest()
        {
            List<Rule> rules = new List<Rule> { new Rule("IsService", "Equal", "true"), new Rule("ProductType", "Equal", "Book") };
            Product prod = new Product(true, "Book");
        }
    }
}
