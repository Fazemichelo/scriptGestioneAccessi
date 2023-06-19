using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UI;
using Google;
using System.Net.Http;

public class FirebaseHandler : MonoBehaviour
{
    private string GoogleWebAPI = "581880652098-tktobgtdctf08sgcblidhkqhndd56nei.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public Text UsernameTxt, UserEmailTxt;
    public GameObject LoginScreen, ProfileScreen;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleWebAPI,
            RequestIdToken = true
        };

    }

    private void Start()
    {
        LoginScreen.SetActive(true);
        ProfileScreen.SetActive(false);
        InitFirebase();
    }
    private void InitFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public void GoogleSingInClick()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }
    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("fault "+ task.Exception);

        }
        else if (task.IsCanceled)
        {
            Debug.LogError("login canceled");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SingInWithCredentialAsync was cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SingInWithCredentialAsync occurred in an error: " + task.Exception);
                    return;
                }
                Debug.Log("Riuscito!");
                user = auth.CurrentUser;
                UsernameTxt.text = user.DisplayName;
                UserEmailTxt.text = user.Email;
                LoginScreen.SetActive(false);
                ProfileScreen.SetActive(true);
            });
        }
    }
}
