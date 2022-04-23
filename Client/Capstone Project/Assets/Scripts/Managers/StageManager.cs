using System;
using System.Collections;
using System.Collections.Generic;
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

        _incompletedConditionList.Add(Managers.Coin);
        Managers.CodeBlock.BlockRestriction = (int)Enum.Parse(typeof(Define.StageBlock), sceneName);
        _incompletedConditionList.Add(Managers.CodeBlock);
        // TODO, Define.StageBlock���� �о�� ���� �ڵ� ����� �����ϴ� �Ŵ������� �ְ�, �Ŵ����� �ڽ��� ���� ������ �ش� ������ ����, CheckClear ���� �� �ش� ���� �ڵ� ����� �� ��
        _incompletedConditionList.Add(Managers.Map);
    }


    public bool CheckConditionCompleted() // ���ǵ��� �����Ǿ����� Ȯ��
    {
        foreach (I_CheckClear condition in _incompletedConditionList)
        {
            if (condition.CheckCleared() == true)
            {
                _completedConditionList.Add(condition.GetType().Name);
                _incompletedConditionList.Remove(condition);

                if (condition is MapManager)
                {
                    //ClearAction.Invoke(condition);
                    Managers.UI.ShowPopupUI<UI_ClearPopup>();
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
