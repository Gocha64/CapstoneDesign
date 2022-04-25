using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace Login_Util
{
    public class LoginManager : MonoBehaviour

    {
        public GameObject Btn_Login;
        public GameObject Btn_Logout;
        public GameObject User_name_UI;
        public GameObject Btn_MyPage_Login;
        public GameObject Btn_MyPage_Logout;
        public GameObject Btn_Play_Login;
        public GameObject Btn_Play_Logout;

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
            //���ӽ��۽� �ڵ��α���
            doAutoLogin();
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

        // �����α��� 
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
            Btn_Login.SetActive(false);
            Btn_Logout.SetActive(true);
            User_name_UI.SetActive(true);
            User_name_UI.GetComponent<Text>().text = Social.localUser.userName;
            Btn_MyPage_Login.SetActive(true);
            Btn_MyPage_Logout.SetActive(false);
            Btn_Play_Login.SetActive(true);
            Btn_Play_Logout.SetActive(false);
        }

        public void Logout_State()
        {
            Btn_Login.SetActive(true);
            Btn_Logout.SetActive(false);
            User_name_UI.SetActive(false);
            Btn_MyPage_Login.SetActive(false);
            Btn_MyPage_Logout.SetActive(true);
            Btn_Play_Login.SetActive(false);
            Btn_Play_Logout.SetActive(true);
        }

        public void before_Test()
        {
            Btn_Login.SetActive(true);
            Btn_Logout.SetActive(false);
            User_name_UI.SetActive(false);
            Btn_MyPage_Login.SetActive(false);
            Btn_MyPage_Logout.SetActive(true);
            Btn_Play_Login.SetActive(false);
            Btn_Play_Logout.SetActive(true);
        }
    }
}