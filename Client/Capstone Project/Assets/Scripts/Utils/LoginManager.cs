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


        public Text ScriptTxt1;
        public Text ScriptTxt2;
        string aa = "";


        public GameObject Btn_Login;
        public GameObject Btn_Logout;
        public GameObject User_name_UI;
        public GameObject Btn_MyPage_Login;
        public GameObject Btn_MyPage_Logout;

        private bool bWaitingForAuth = false;

        private void Awake()
        {
            // ���� ���Ӽ��� Ȱ��ȭ (�ʱ�ȭ)
            PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }


        private void Start()
        {

        }

        // �ڵ��α���
        public void doAutoLogin()
        {
            if (bWaitingForAuth)
                return;
            //���� �α����� �Ǿ����� �ʴٸ� 
            if (!Social.localUser.authenticated)
            {
                bWaitingForAuth = true;
                //�α��� ���� ó������ (�ݹ��Լ�)
                Social.localUser.Authenticate(AuthenticateCallback);
            }
        }

        public void OnBtnLoginClicked()
        {
            //�̹� ������ ����ڴ� �ٷ� �α��� �����ȴ�. 
            if (Social.localUser.authenticated)
            {
                Login_State();
            }
            else
                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        Login_State();
                    }
                    else
                    {
                        Logout_State();
                    }
                });
        }

        public void checkLogin()
        {
            if (Social.localUser.authenticated)
            {
                Login_State();
            }
            else
            {
                Logout_State();
            
            }
        }

        // ���� �α׾ƿ� 
        public void OnBtnLogoutClicked()
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
            Logout_State();
        }


        // ���� callback
        void AuthenticateCallback(bool success)
        {
            if (success)
            {
                // ����� �̸��� ����� 
                Login_State();
            }
            else
            {
                Logout_State();
            }
        }

        public void Login_State()
        {
            Btn_MyPage_Login.SetActive(true);
            Btn_MyPage_Logout.SetActive(false);
            Btn_Login.SetActive(false);
            Btn_Logout.SetActive(true);
            User_name_UI.SetActive(true);
        
            aa = Social.localUser.userName;
            ScriptTxt1 = GameObject.Find("User_Name").GetComponent<Text>();
            ScriptTxt1.text = aa;

            Transform a = GameObject.Find("MainCanvas").transform;
            

            a = a.transform.Find("MyPageUI");
            a = a.transform.Find("MyPage");
            a = a.transform.Find("Character");
            a = a.transform.Find("User_Name");


            ScriptTxt2 = a.GetComponent<Text>();
            ScriptTxt2.text = aa;

            Managers.User.UID = Social.localUser.id;
            Managers.User.Name = Social.localUser.userName;
            Managers.User.ChallangeStageInfo.Add(1, 0);

            Managers.Login.SendRequestNameInput();
            Managers.Login.SendRequestLoadStar();
            Managers.Login.SendRequestMyPage();


        }
        
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