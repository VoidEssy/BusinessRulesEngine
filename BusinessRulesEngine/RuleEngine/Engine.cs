using RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RuleEngine
{
    public static class Engine
    {
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

        public static Func<T, bool> CompileRule<T>(Rule r)
        {
            var paramNode = Expression.Parameter(typeof(T));
            Expression expr = BuildExpression<T>(r, paramNode);
            return Expression.Lambda<Func<T, bool>>(expr, paramNode).Compile();
        }

        // Technically could use an overload but one is Compile A rule the other is Compile multiple Rules at once
        public static List<Func<T, bool>> CompileRules<T>(List<Rule> rules)
        {
            List<Func<T, bool>> compiledRules = new List<Func<T, bool>>();
            foreach(var rule in rules)
            {
                compiledRules.Add(CompileRule<T>(rule));
            }
            return compiledRules;
        }
    }
}
