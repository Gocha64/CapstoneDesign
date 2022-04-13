using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;

public class Login_Util : MonoBehaviour
{
    private Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public GameObject Btn_Login;
    public GameObject Btn_Logout;

    void Start()
    {
        //���� �ʱ⼳��
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate(); // ���� �÷��� Ȱ��ȭ

        Btn_Login.SetActive(true);
        Btn_Logout.SetActive(false);

        auth = FirebaseAuth.DefaultInstance; // Firebase �׼���
    }

    void awake()
    {
        CheckFirebaseDependencies();
    }

    public void TryGoogleLogin()
    {
        if (!Social.localUser.authenticated) // �α��� �Ǿ� ���� �ʴ��� Ȯ��
        {
            Social.localUser.Authenticate(success => // �α��� �õ�
            {
                if (success) // �����ϸ�
                {
                    Debug.Log("�α��� ����");
                    StartCoroutine(TryFirebaseLogin()); // Firebase Login �õ�
                }
                else // �����ϸ�
                {
                    Debug.Log("�α��� ����");
                    Set_Logout_to_LogIn();
                }
            });
        }
    }


    public void TryGoogleLogout()
    {
        if (Social.localUser.authenticated) // �α��� �Ǿ� �ִ��� Ȯ��
        {
            PlayGamesPlatform.Instance.SignOut(); // Google �α׾ƿ�
            Debug.Log("���� �α׾ƿ� �Ϸ�");
            auth.SignOut(); // Firebase �α׾ƿ�
            Debug.Log("���̾�̽� �α׾ƿ� �Ϸ�");
            Set_Logout_to_LogIn();
        }
    }

    private void CheckFirebaseDependencies()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            string name = user.DisplayName;
            string uid = user.UserId;
            Set_Login_to_LogOut();
        }
        else
        {
            Set_Logout_to_LogIn();
        }
    }

    IEnumerator TryFirebaseLogin()
    {
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();


        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("���̾� ���̽� �α��� ���� ���");
                Set_Logout_to_LogIn();
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("���̾�̽� �α��� ����: " + task.Exception);
                Set_Logout_to_LogIn();
                return;
            }
            else {
                Set_Login_to_LogOut();
                Debug.Log("���̾�̽� �α��� ����");
            }
        });
    }

    public string Get_User_Id() // firebase_id get
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        string uid = "Nothing";
        if (user != null)
        {
            uid = user.UserId;
        }
        return uid;
    }

    public void Set_Login_to_LogOut() // �α����� ���϶� -> �α׾ƿ��� ���̰� �ٲٱ�
    {
        Btn_Login.SetActive(true);
        Btn_Logout.SetActive(false);
    }

    public void Set_Logout_to_LogIn() // �α׾ƿ��� ���϶� -> �α����� ���̰� �ٲٱ�
    {
        Btn_Login.SetActive(false);
        Btn_Logout.SetActive(true);
    }
}