using System.Text.RegularExpressions;
using UnityEngine;

public class ContentSpecification : ISpecification<string>
{
    public string ErrorMassage { get; private set; }

    public bool IsSatisfiedBy(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            ErrorMassage = "내용이 없는 글은 작성할 수 없습니다.";
            return false;
        }
        if (input.Length > 1000)
        {
            ErrorMassage = $"글자수 제한을 벗어났습니다. ({input.Length}/1000)";
            return false;
        }

        return true;
    }
}