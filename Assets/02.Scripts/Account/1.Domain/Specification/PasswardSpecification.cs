using System.Text.RegularExpressions;
using UnityEngine;

public class PasswardSpecification : ISpecification<string>
{
    public string ErrorMassage { get;  private set;}
    private const int MININUMLENGTH = 6;    
    private static readonly Regex _passwardRegex = new Regex(@"^[가-힣]+$");

    public bool IsSatisfiedBy(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            ErrorMassage = "비밀번호가 비어있습니다.";
            return false;
        }

        if (input.Length < 6)
        {
            ErrorMassage = $"비밀번호는 {MININUMLENGTH}글자 이상이어야만 합니다.";
            return false;
        }

        if (_passwardRegex.IsMatch(input))
        {
            ErrorMassage = "비밀번호는 한글을 사용할 수 없습니다.";
            return false;
        }

        return true;
    }
}
