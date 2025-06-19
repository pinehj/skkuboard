using System.Text.RegularExpressions;
using UnityEngine;

public class EmailSpecification : ISpecification<string>
{
    public string ErrorMassage { get; private set; }
    private static readonly Regex _emailRegex = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$");

    public bool IsSatisfiedBy(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            ErrorMassage = "이메일이 비어있습니다.";
            return false;
        }
        
        if (!_emailRegex.IsMatch(input))
        {
             ErrorMassage = "올바른 이메일 형태가 아닙니다.";
            return false;
        }

        return true;
    }
}
