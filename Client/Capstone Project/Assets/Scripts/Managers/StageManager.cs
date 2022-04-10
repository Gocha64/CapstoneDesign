using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager
{
    List<I_CheckClear> _conditionList = new List<I_CheckClear>(); // �� ȹ�� ���� ����Ʈ - ����Ʈ�� �߰��� Ŭ�������� CheckClear() �޼ҵ带 ���� ���� �����ߴ��� üũ

    public Action<I_CheckClear> ConditionAction = null; // UI ��� Condition�� ���� �Ǿ��� �� �˸��� �ޱ� ���� Action
    public Action<I_CheckClear> ClearAction = null; // ���������� Clear �Ǿ��� �� �˸��� �ޱ� ���� Action

    public void ConditionSet() // ���� ����
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        _conditionList.Add(Managers.Coin);
        // TODO, Define.StageBlock���� �о�� ���� �ڵ� ����� �����ϴ� �Ŵ������� �ְ�, �Ŵ����� �ڽ��� ���� ������ �ش� ������ ����, CheckClear ���� �� �ش� ���� �ڵ� ����� �� ��
        _conditionList.Add(Managers.Map);
    }

   public bool CheckConditionCompleted() // ���ǵ��� �����Ǿ����� Ȯ��
   {
        foreach (I_CheckClear condition in _conditionList)
        {
            if (condition.CheckCleared() == true)
            {
                ConditionAction.Invoke(condition);
                _conditionList.Remove(condition);

                if (condition is MapManager)
                {
                    ClearAction.Invoke(condition);
                    return true;
                }
            }
            
        }

        return false;
    }

    public void Clear() // �� ��ȯ�� ���� Clear
    {
        _conditionList.Clear();
        ConditionAction = null;
        ClearAction = null;
    }
}
