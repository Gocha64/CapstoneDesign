using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockManager : I_CheckClear
{
    int _blockRestriction = 10;

    GameObject _be2ProgEnv = null;

    public int BlockRestriction
    {
        get { return _blockRestriction; }
        set { _blockRestriction = value; }
    }

    public GameObject BE2ProgEnv
    {
        get { return _be2ProgEnv; }
        set { _be2ProgEnv = value; }
    }

    public bool CheckCleared()
    {
        int currentNOCodeBlocks = CheckChildCount(_be2ProgEnv.transform.Find("Speeder TO and PE").Find("Canvas Programming Env").Find("Scroll View").Find("Viewport").Find("Programming Env")
            .Find("HorizontalBlock Ins WhenPlayClicked").Find("Section0").Find("Body")) - 1; // ����Լ��� ���� Block �߰��� �� -1

        return currentNOCodeBlocks <= _blockRestriction;
    }

    int CheckChildCount(Transform transform) // �ڽ��� ���� ��� ȣ�� ���Ͽ� �ľ�
    {
        int childCount = 0;

        if (transform.childCount == 0)
            return 1;

        for (int i = 0; i < transform.childCount; i++)
        {
            childCount += CheckChildCount(transform.GetChild(i));
        }

        return childCount;
    }
}
