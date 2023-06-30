using Universal.UniversalSDK;

public class LoginController : MonoBehaviour {
    public void OnClickExampleLogin()
    {        
        UniversalSDK.Ins.Login(LoginType.GOOGLE,
            result =>
            {
                result.Match(
                    value =>
                    {
                        UpdateLoginResult(value);
                    },
                    error =>
                    {
                        messageText.text = error.Message;
                    });
            });
    }
}