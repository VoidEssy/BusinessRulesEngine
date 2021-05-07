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
            Product prod = new Product(false, "Book", "Animal Farm", "Purchase");
            Rule basicRule = new Rule("IsService", "Equal", "false");
            var TestRule = Engine.CompileRule<Product>(basicRule);
            var result = TestRule.Invoke(prod);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EngineEnumRuleBuildTest()
        {
            Product prod = new Product(false, "Book", "Animal Farm", "Purchase");
            Rule basicRule = new Rule("ProductType", "Equal", "Book");
            var TestRule = Engine.CompileRule<Product>(basicRule);
            var result = TestRule.Invoke(prod);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EngineRuleSetBuildTest()
        {
            List<Rule> rules = new List<Rule> { new Rule("IsService", "Equal", "false"), new Rule("ProductType", "Equal", "Book") };
            Product prod = new Product(false, "Book", "Animal Farm", "Purchase");
            var rule1 = Engine.CompileRule<Product>(rules[0]);
            var rule2 = Engine.CompileRule<Product>(rules[1]);
            var result1 = rule1.Invoke(prod);
            var result2 = rule2.Invoke(prod);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void EngineCompileMultipleRulesTest()
        {
            List<Rule> rules = new List<Rule> { new Rule("IsService", "Equal", "true"), new Rule("ProductType", "Equal", "Book") };
            var compiledRules = Engine.CompileRules<Product>(rules);
            Assert.AreEqual(2, compiledRules.Count);
        }

        [TestMethod]
        public void EngineEvalAgainstCompiledRuleCollectionTest()
        {
            List<Rule> rules = new List<Rule> { new Rule("IsService", "Equal", "false"), new Rule("ProductType", "Equal", "Book") };
            Product prod = new Product(false, "Book", "Animal Farm", "Purchase");
            var compiledRules = Engine.CompileRules<Product>(rules);
            var result = Engine.PassesRuleSet(compiledRules, prod);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EngineEvalAgainsRuleCollectionTest()
        {
            List<Rule> rules = new List<Rule> { new Rule("IsService", "Equal", "false"), new Rule("ProductType", "Equal", "Book") };
            Product prod = new Product(false, "Book", "Animal Farm", "Purchase");
            var result = Engine.PassesRuleSet(rules, prod);
            Assert.IsTrue(result);
        }
    }
}
