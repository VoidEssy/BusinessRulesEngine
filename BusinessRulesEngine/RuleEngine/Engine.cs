using RuleEngine.Models;
using System;
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

        public static Func<T, bool> CompileRule<T>(Rule r, object targetObj)
        {
            
            var paramNode = Expression.Parameter(targetObj.GetType());
            Expression expr = BuildExpression<T>(r, paramNode);
            return Expression.Lambda<Func<T, bool>>(expr, paramNode).Compile();
        }
    }
}
