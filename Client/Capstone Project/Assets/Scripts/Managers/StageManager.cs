using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager
{
    public static bool ToMain = false;
    public static bool ToMain2 = false;
    public static bool ToMain3 = false;
    public static bool ToMain4 = false;

    public static bool Basic = false;
    public static bool Codition = false;
    public static bool Loop = false;
    public static bool Challenge = false;


    List<I_CheckClear> _incompletedConditionList = new List<I_CheckClear>(); // �� ȹ�� ���� ����Ʈ - ����Ʈ�� �߰��� Ŭ�������� CheckClear() �޼ҵ带 ���� ���� �����ߴ��� üũ
    List<string> _completedConditionList = new List<string>();

    public List<string> CompletedConditionList
    {
        get { return _completedConditionList; }
    }


    public Action<I_CheckClear> ConditionAction = null; // UI ��� Condition�� ���� �Ǿ��� �� �˸��� �ޱ� ���� Action
    public Action<I_CheckClear> ClearAction = null; // ���������� Clear �Ǿ��� �� �˸��� �ޱ� ���� Action



    public void ConditionSet() // ���� ����
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Contains("Challenge"))
        {
            _incompletedConditionList.Add(Managers.Coin);
            Managers.CodeBlock.BlockRestriction = (int)Enum.Parse(typeof(Define.StageBlock), sceneName);
            _incompletedConditionList.Add(Managers.CodeBlock);
            // TODO, Define.StageBlock���� �о�� ���� �ڵ� ����� �����ϴ� �Ŵ������� �ְ�, �Ŵ����� �ڽ��� ���� ������ �ش� ������ ����, CheckClear ���� �� �ش� ���� �ڵ� ����� �� ��
        }
        _incompletedConditionList.Add(Managers.Map);
    }

    public void HandleSuccess()
    {
        UI_Finished popup = null;

        if (!SceneManager.GetActiveScene().name.Contains("Challenge")) // Challenge ���������� �ƴ� �� �˾�
        {
            GameObject go = Managers.Resource.Instantiate("StudyStage_Complete1");
            popup = go.AddComponent<UI_StudyClearPopup>();
            popup.Init();
        }
        else
        {
            GameObject go = Managers.Resource.Instantiate("ChallengeStage_Complete1");
            popup = go.AddComponent<UI_ClearPopup>();
            popup.Init();

            string sceneName = SceneManager.GetActiveScene().name;
            string tempName = Regex.Replace(sceneName, @"\D", "");
            byte challengeNum = byte.Parse(tempName);

            byte stars = 0;

            if (Managers.User.ChallangeStageInfo.TryGetValue(challengeNum, out stars))
            {
                int currentCount = Managers.Stage.CompletedConditionList.Count;
                if (currentCount > stars)
                {
                    stars = (byte)currentCount;
                    Managers.User.ChallangeStageInfo.Remove(challengeNum);
                    Managers.User.ChallangeStageInfo.Add(challengeNum, stars);
                    Managers.User.ChallangeStageInfo.Add((ushort)(challengeNum + 1), 0);
                    Managers.User.TotalStars += (ushort)currentCount;

                    C_ChallengeUpdateStars pkt = new C_ChallengeUpdateStars();
                    pkt.UId = Managers.User.UID;
                    pkt.stageId = challengeNum;
                    pkt.numberOfStars = stars;

                    Managers.Network.Send(pkt.Write());
                }
            }
            else
            {
                Debug.Log("Stage Num Error");
            }

        }
    }

    public void HandleFailed()
    {
        UI_Finished popup = null;
        GameObject go = Managers.Resource.Instantiate("Stage_fail1");
        popup = go.AddComponent<UI_FailedPopup>();
        popup.Init();
    }

    public bool CheckConditionCompleted() // ���ǵ��� �����Ǿ����� Ȯ��
    {
        foreach (I_CheckClear condition in _incompletedConditionList)
        {
            if (condition.CheckCleared() == true)
            {
                _completedConditionList.Add(condition.GetType().Name);
                //_incompletedConditionList.Remove(condition);

                if (condition is MapManager)
                {
                    //ClearAction.Invoke(condition);
                    //TODO: �������� Ŭ����, ������ Ŭ�������ǿ� ���� ���� ���� ��Ŷ ������.
                    return true;
                }
            }

        }

        return false;
    }


    public void Clear() // �� ��ȯ�� ���� Clear
    {
        _incompletedConditionList.Clear();
        _completedConditionList.Clear();
        ConditionAction = null;
        ClearAction = null;
    }




}
