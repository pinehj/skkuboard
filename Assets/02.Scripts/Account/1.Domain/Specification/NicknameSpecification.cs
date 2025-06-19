using System.Text.RegularExpressions;
using UnityEngine;

public class NicknameSpecification
{
    public string ErrorMassage { get; private set; }
    private const int MAXIMUMLENGTH = 8;
    private static readonly Regex _nicknameRegex = new Regex(@"^[가-힣a-zA-Z0-9]+$");

    public bool IsSatifiedBy(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            ErrorMassage = "낙네임이 비어있습니다." ;
            return false;
        }

        if (input.Length > MAXIMUMLENGTH)
        {
            ErrorMassage = $"닉네임은 {MAXIMUMLENGTH}글자 이하만 가능합니다.";
            return false;
        }
        
        if (!_nicknameRegex.IsMatch(input))
        {
            ErrorMassage = "닉네임은 영문 대소문자, 한글, 숫자만 가능합니다.";
            return false;
        }

        return true;
    }
}
