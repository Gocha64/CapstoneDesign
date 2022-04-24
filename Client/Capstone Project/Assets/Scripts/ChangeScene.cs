using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    
    public void MoveToMain()
    {
        if(StageManager.Basic == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain = true;
            
        }
        else if(StageManager.Codition == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain2 = true;

        }
        else if(StageManager.Loop == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain3 = true;

        }
        
        else if (StageManager.Challenge == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain4 = true;
        }
        


    }

    public void ReloadMe()
    {
        Managers.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void MoveToBasic_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 1");
    }

    public void MoveToBasic_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 2");
    }

    public void MoveToBasic_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 3");
    }
    public void MoveToBasic_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 4");
    }
    public void MoveToBasic_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 5");
    }
    public void MoveToBasic_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 6");
    }
    public void MoveToBasic_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 7");
    }
    public void MoveToBasic_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 8");
    }
    public void MoveToBasic_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 9");
    }
    public void MoveToBasic_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 10");
    }

    public void MoveToCodition_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 1");
    }
    public void MoveToCodition_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 2");
    }
    public void MoveToCodition_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 3");
    }
    public void MoveToCodition_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 4");
    }
    public void MoveToCodition_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 5");
    }
    public void MoveToCodition_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 6");
    }
    public void MoveToCodition_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 7");
    }
    public void MoveToCodition_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 8");
    }
    public void MoveToCodition_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 9");
    }
    public void MoveToCodition_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 10");
    }


    public void MoveToLoop_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 1");
    }
    public void MoveToLoop_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 2");
    }
    public void MoveToLoop_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 3");
    }
    public void MoveToLoop_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 4");
    }
    public void MoveToLoop_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 5");
    }
    public void MoveToLoop_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 6");
    }
    public void MoveToLoop_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 7");
    }
    public void MoveToLoop_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 8");
    }
    public void MoveToLoop_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 9");
    }
    public void MoveToLoop_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 10");
    }

    public void MoveToChallenge_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 1");
    }
    public void MoveToChallenge_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 2");
    }
    public void MoveToChallenge_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 3");
    }
    public void MoveToChallenge_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 4");
    }
    public void MoveToChallenge_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 5");
    }
    public void MoveToChallenge_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 6");
    }
    public void MoveToChallenge_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 7");
    }
    public void MoveToChallenge_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 8");
    }
    public void MoveToChallenge_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 9");
    }
    public void MoveToChallenge_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge 10");
    }


}
