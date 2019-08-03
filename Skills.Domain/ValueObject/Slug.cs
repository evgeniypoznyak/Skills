using System;
using System.Collections.Generic;
using System.Linq;

namespace Skills.Domain.ValueObject
{
    public class Slug
    {
        public string Name { get; }

        public Slug(string name)
        {
            Name = ParseString(name);
        }
        
        private string ParseString(string input)
        {
            input = input.Trim();
            var inputList = !string.IsNullOrEmpty(input) && input != "null"
                ? input.Split(' ').ToList()
                : new List<string>();
            var resultList = new List<string>();
            foreach (var s in inputList) resultList.Add(UseWhere(s));

            return String.Join("-", resultList.Where(s => !String.IsNullOrEmpty(s))).ToLower();
        }
        
        private string UseWhere(string dirtyString)
        {
            return new String(dirtyString.Where(IsLetterOrDigitOrDash).ToArray());
        }

        public bool IsLetterOrDigitOrDash(char ch)
        {
            return Char.IsLetterOrDigit(ch) || ch == '-';
        }

    }
}