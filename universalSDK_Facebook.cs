using Universal.UniversalSDK;

public class YourObjNameHere : MonoBehaviour {
    public void SignInFacebook()
    {        
        UniversalSDK.Ins.Login(LoginType.FACEBOOK,
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
