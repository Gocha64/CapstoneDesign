using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private int _blockId;
    [SerializeField]
    private char _blockType;

    public int BlockId
    {
        get { return _blockId; }
        set { _blockId = value; }
    }

    public char BlockType
    {
        get { return _blockType; }
        set { _blockType = value; }
    }

}
