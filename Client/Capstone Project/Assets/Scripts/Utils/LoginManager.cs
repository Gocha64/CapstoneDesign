using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;

namespace Login_Util
{
    public class LoginManager : MonoBehaviour
    {
<<<<<<< Updated upstream
        private Firebase.Auth.FirebaseAuth auth;
        private Firebase.Auth.FirebaseUser user;
=======
        public Text ScriptTxt1;

        public Text ScriptTxt2;
        string aa = "";
>>>>>>> Stashed changes

        public GameObject Btn_Login;
        public GameObject Btn_Logout;
        public GameObject User_name_UI;
        public GameObject Btn_MyPage_Login;
        public GameObject Btn_MyPage_Logout;


        //void Start()
        //{
        //    //���� �ʱ⼳��
        //    PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
        //        .RequestIdToken()
        //        .RequestEmail()
        //        .Build());
        //    PlayGamesPlatform.DebugLogEnabled = true;
        //    PlayGamesPlatform.Activate(); // ���� �÷��� Ȱ��ȭ

        //    Btn_Login.SetActive(true);
        //    Btn_Logout.SetActive(false);
        //    User_name_UI.SetActive(false);
        //    CheckFirebaseDependencies();
        //    auth = FirebaseAuth.DefaultInstance; // Firebase �׼���
        //}
        void start()
        {
            before_Test();
        }
        //void awake()
        //{
        //    CheckFirebaseDependencies();
        //}

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
                        Logout_State();
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
                Logout_State();
            }
        }

        public void CheckFirebaseDependencies()
        {
            Firebase.Auth.FirebaseUser user = auth.CurrentUser;
            if (user != null)
            {
                string name = user.DisplayName;
                string uid = user.UserId;
                User_name_UI.GetComponent<Text>().text = name;
                Login_State();
            }
            else
            {
                User_name_UI.GetComponent<Text>().text = "Default";
                Logout_State();
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
                    Logout_State();
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("���̾�̽� �α��� ����: " + task.Exception);
                    Logout_State();
                    return;
                }
                else
                {
                    Debug.Log("���̾�̽� �α��� ����");
                    Login_State();
                }
            });
        }
        public void Login_State()
        {
            Btn_MyPage_Login.SetActive(true);
            Btn_MyPage_Logout.SetActive(false);
            Btn_Login.SetActive(false);
            Btn_Logout.SetActive(true);
            User_name_UI.SetActive(true);
<<<<<<< Updated upstream
            Btn_MyPage_Login.SetActive(true);
            Btn_MyPage_Logout.SetActive(false);
            Btn_Play_Login.SetActive(true);
            Btn_Play_Logout.SetActive(false);
        }
=======
            aa = Social.localUser.userName;
            ScriptTxt1 = GameObject.Find("User_Name").GetComponent<Text>();
            ScriptTxt1.text = aa;

            Transform a = GameObject.Find("MainCanvas").transform;
            

            a = a.transform.Find("MyPageUI");
            a = a.transform.Find("MyPage");
            a = a.transform.Find("Character");
            a = a.transform.Find("User_Name");
>>>>>>> Stashed changes

            ScriptTxt2 = a.GetComponent<Text>();
            ScriptTxt2.text = aa;

            

        }
        //public void aaa()
        //{
        //   ScriptTxt2 = GameObject.Find("User_Name2").GetComponent<Text>();
        //   ScriptTxt2.text = aa;
        //}
        
        public void Logout_State()
        {
            Btn_Login.SetActive(true);
            Btn_Logout.SetActive(false);
            User_name_UI.SetActive(false);
            Btn_MyPage_Login.SetActive(false);
            Btn_MyPage_Logout.SetActive(true);
        }

        public void before_Test()
        {
            Btn_Login.SetActive(true);
            Btn_Logout.SetActive(false);
            User_name_UI.SetActive(false);
            Btn_MyPage_Login.SetActive(false);
            Btn_MyPage_Logout.SetActive(true);
        }
    }
}