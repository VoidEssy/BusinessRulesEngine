using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEngine.Models
{
    public class Rule
    {
        public string PropertyName { get; set; }
        public string Operator { get; set; }
        public string TargetValue { get; set; }

        public Rule(string propName, string op, string targetValue)
        {
            PropertyName = propName;
            Operator = op;
            TargetValue = targetValue;
        }
    }
}
