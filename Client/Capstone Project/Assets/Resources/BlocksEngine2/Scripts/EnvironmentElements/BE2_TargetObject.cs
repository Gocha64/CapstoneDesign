﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_TargetObject : MonoBehaviour, I_BE2_TargetObject
{
    enum Direction
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3,
        size = 4,
        
    }

    enum State
    {
        Idle = 0,
        Moving = 1,

    }

    int currentPositionInMap;

    public int CurrentPositionInMap
    {
        get { return currentPositionInMap; }
        set { currentPositionInMap = value; }
    }

    int _velocity = 1;
    bool interactable = true;
    GameObject currentBlock = null;

    public GameObject CurrentBlock
    {
        get { return currentBlock; }
        set { currentBlock = value; }
    }
    

    int currentDirection = (int)Direction.up;

    public Transform Transform => transform;
    public I_BE2_ProgrammingEnv ProgrammingEnv { get; set; }

    void I_BE2_TargetObject.Move()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.Move(currentDirection, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
                return;

            this.gameObject.transform.position = newBlock.transform.position + new Vector3(0, 0.9f, 0);
            currentBlock = newBlock;
        }

    }

    void I_BE2_TargetObject.Turn(bool clockWise)
    {
        if (clockWise)
        {
            currentDirection = (currentDirection + 1) % (int)Direction.size;
            this.gameObject.transform.forward = this.gameObject.transform.right;
        }
        else
        {
            currentDirection = (currentDirection + (int)Direction.size - 1) % (int)Direction.size;
            this.gameObject.transform.forward = -this.gameObject.transform.right;
        }
    }


    bool I_BE2_TargetObject.AbleRight()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck((currentDirection + 1) % (int)Direction.size, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                Debug.Log("오른쪽 불가능1");
                return false;

            }

            Debug.Log("오른쪽 가능");
            return true;
        }

        Debug.Log("오른쪽 불가능2");
        return false;
    }

    bool I_BE2_TargetObject.AbleLeft()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck((currentDirection - 1) % (int)Direction.size, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                Debug.Log("왼쪽 불가능1");
                return false;

            }

            Debug.Log("왼쪽 가능");
            return true;
        }

        Debug.Log("왼쪽 불가능2");
        return false;
    }

    bool I_BE2_TargetObject.AbleForward()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck(currentDirection, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                Debug.Log("전진 불가능1");
                return false;

            }

            Debug.Log("전진 가능");
            return true;
        }

        Debug.Log("전진 불가능2");
        return false;
    }

}
