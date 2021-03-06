using RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RuleEngine
{
    public static class Engine
    {
        /// <summary>
        /// Builds an expression based on supplied Rule and a Node from Expression tree
        /// </summary>
        /// <typeparam name="T">Object type which will be used in generated expression</typeparam>
        /// <param name="rule">Rule definition for the expression to build from</param>
        /// <param name="exp">Expression Tree node</param>
        /// <returns>BinaryExpression or MethodCallExpression</returns>
        public static Expression BuildExpression<T>(Rule rule, ParameterExpression exp)
        {
            var leftHand = Expression.Property(exp, rule.PropertyName);
            var targetPropertyType = typeof(T).GetProperty(rule.PropertyName).PropertyType;
            if (Enum.TryParse(rule.Operator, out ExpressionType expType))
            {
                var rightHand = Expression.Constant(Convert.ChangeType(rule.TargetValue, targetPropertyType));
                // Use an operation ex "IsService == True"
                return Expression.MakeBinary(expType, leftHand, rightHand);
            }
            else
            {
                var method = targetPropertyType.GetMethod(rule.Operator);
                var targetParam = method.GetParameters()[0].ParameterType;
                var rightHand = Expression.Constant(Convert.ChangeType(rule.TargetValue, targetParam));
                // Call a method ex. List.Contains(something).
                return Expression.Call(leftHand, method, rightHand);
            }
        }

        /// <summary>
        /// Generate a Method / Lambda Expression based on defined rule.
        /// </summary>
        /// <typeparam name="T">Type of the object you'll use in the evaluation</typeparam>
        /// <param name="r">A Rule Definition</param>
        /// <returns>Compiled Lambda Expression / Dynamically generated method call</returns>
        public static Func<T, bool> CompileRule<T>(Rule r)
        {
            var paramNode = Expression.Parameter(typeof(T));
            Expression expr = BuildExpression<T>(r, paramNode);
            return Expression.Lambda<Func<T, bool>>(expr, paramNode).Compile();
        }

        // Technically could use an overload but one is Compile A rule the other is Compile multiple Rules at once
        /// <summary>
        /// Compiles a set of rules read for execution, wrapper around CompileRule method
        /// </summary>
        /// <typeparam name="T">Object type they are compiled for</typeparam>
        /// <param name="rules">Rule Definitions</param>
        /// <returns>List of Compiled Lambda Expressions</returns>
        public static List<Func<T, bool>> CompileRules<T>(List<Rule> rules)
        {
            List<Func<T, bool>> compiledRules = new List<Func<T, bool>>();
            foreach(var rule in rules)
            {
                compiledRules.Add(CompileRule<T>(rule));
            }
            return compiledRules;
        }

        /// <summary>
        /// Based on List of Rule definitions evaluates if target object passes all of them
        /// </summary>
        /// <typeparam name="T">Target Object Type</typeparam>
        /// <param name="rules">Rule definitions</param>
        /// <param name="targetObject">Target Object</param>
        /// <returns>Boolean</returns>
        public static bool PassesRuleSet<T>(List<Rule> rules, T targetObject)
        {
            var rulesForEvaluation = CompileRules<T>(rules);
            return rulesForEvaluation.TrueForAll(x => x.Invoke(targetObject));
        }

        /// <summary>
        /// Based on List of Compiled Lambda expressions will check if the object passes all of them
        /// </summary>
        /// <typeparam name="T">Target Object Type</typeparam>
        /// <param name="compiledRules">Compiled Rules / Lambda Expressions</param>
        /// <param name="targetObject">Target Object</param>
        /// <returns></returns>
        public static bool PassesRuleSet<T>(List<Func<T, bool>> compiledRules, T targetObject)
        {
            return compiledRules.TrueForAll(x => x.Invoke(targetObject));
        }
    }
}
