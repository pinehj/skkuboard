using NUnit.Framework.Interfaces;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UI_Account : MonoBehaviour
{
    [Header("[Panels]")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;
    public GameObject AccountDeletePopup;


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


    public async void Register()
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

        AccountResultMessage result = await AccountManager.Instance.TryRegister(email, passward, nickname);
        RegisterMessageText.text = result.MessageText;

        if (result.IsSuccess)
        {
            LoginEmailInputField.text = RegisterEmailInputField.text;
            LoginPasswardInputField.text = "";
            OnBackButton();
        }
    }

    public async void Login()
    {
        string email = LoginEmailInputField.text;
        if (!_emailSpecification.IsSatisfiedBy(email))
        {
            LoginMessageText.text = _emailSpecification.ErrorMassage;
            // LoginMessageText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        string passward = LoginPasswardInputField.text;
        if (!_passwardSpecification.IsSatisfiedBy(passward))
        {
            LoginMessageText.text = _passwardSpecification.ErrorMassage;
            // LoginMessageText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        AccountResultMessage result = await AccountManager.Instance.TryLogin(email, passward);
        LoginMessageText.text = result.MessageText;
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
