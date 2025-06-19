using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UI_Account : MonoBehaviour
{
    [Header("[Panels]")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;


    [Header("[Login]")]
    public TMP_InputField LoginEmailInputField;
    public TMP_InputField LoginPasswardInputField;
    public TextMeshProUGUI LoginMessageText;


    [Header("[Register]")]
    public TMP_InputField RegisterEmailInputField;
    public TMP_InputField RegisterNicknameInputField;
    public TMP_InputField RegisterPasswardInputField;
    public TMP_InputField RegisterPasswardCheckInputField;
    public TextMeshProUGUI RegisterMessageText;


    private EmailSpecification _emailSpecification;
    private NicknameSpecification _nicknameSpecification;
    private PasswardSpecification _passwardSpecification;


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);

        _emailSpecification = new EmailSpecification();
        _nicknameSpecification = new NicknameSpecification();
        _passwardSpecification = new PasswardSpecification();
    }

    public void OnRegister() => Register();
    
    public async Task Register()
    {
        string email = RegisterEmailInputField.text;
        if (!_emailSpecification.IsSatisfiedBy(email))
        {
            RegisterMessageText.text = _emailSpecification.ErrorMassage;
            // RegisterMessageText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        string nickname = RegisterNicknameInputField.text;
        if (!_nicknameSpecification.IsSatifiedBy(nickname))
        {
            RegisterMessageText.text = _nicknameSpecification.ErrorMassage;
            // RegisterMessageText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        string passward = RegisterPasswardInputField.text;
        if (!_passwardSpecification.IsSatisfiedBy(passward))
        {
            RegisterMessageText.text = _passwardSpecification.ErrorMassage;
            // RegisterMessageText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        string passwardCheck = RegisterPasswardCheckInputField.text;
        if (passward != passwardCheck)
        {
            RegisterMessageText.text = "비밀번호와 비밀번호 확인이 서로 같지 않습니다.";
            // RegisterMessageText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        if (await AccountManager.Instance.TryRegister(email, nickname, passward))
        {
            RegisterMessageText.text = "회원가입에 실패하였습니다.";
            return;
        }

        RegisterMessageText.text = "회원가입이 완료되었습니다.";

    }

    public void Login()
    {

    }

    public void OnRegisterButton()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);

        LoginEmailInputField.text = RegisterEmailInputField.text;
    }
}
