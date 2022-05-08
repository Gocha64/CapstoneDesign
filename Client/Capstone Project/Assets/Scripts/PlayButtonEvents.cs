using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonEvents : MonoBehaviour
{

    [SerializeField]
    private Transform _section = null;


    public void PlayEvents()
    {
        _section = GameObject.Find("HorizontalBlock Ins WhenPlayClicked").transform;
        _section = _section.GetChild(0).GetChild(1);

        
        //���� ��ų�� ���ٸ� �н� 
        if(_section.childCount != 0)
        {

            //�ڵ� ���� ����
            GameObject g = GameObject.Find("ProgrammingEnv");

            Managers.CodingArea._mainAreaSaved = Instantiate(g.transform.Find("HorizontalBlock Ins WhenPlayClicked").Find("Section0").Find("Body"));
            Managers.CodingArea._mainAreaSaved.name = "MainArea(Clone)";
            DontDestroyOnLoad(Managers.CodingArea._mainAreaSaved);
            Debug.Log($"{Managers.CodingArea._mainAreaSaved.name}");

            Managers.CodingArea._functionAreaSaved = Instantiate(g.transform.Find("FunctionArea").Find("Section0").Find("Body"));
            Managers.CodingArea._functionAreaSaved.name = "FunctionArea(Clone)";
            DontDestroyOnLoad(Managers.CodingArea._functionAreaSaved);
            Debug.Log($"{Managers.CodingArea._functionAreaSaved.name}");



            //ī�޶� ��ȯ
            GameObject.Find("QuaterView Camera").GetComponent<CameraController>().ChangeToQuarterView();


            //��� ���� ���� ����
            GameObject.Find("Canvas Blocks Selection").GetComponent<Canvas>().enabled = false;


            //����
            BE2_ExecutionManager e2ExecutionManager = GameObject.Find("BE2 Execution Manager").GetComponent<BE2_ExecutionManager>();
            e2ExecutionManager.totalNumOfBlocks = GameObject.Find("HorizontalBlock Ins WhenPlayClicked").GetComponent<BE2_BlocksStack>().InstructionsArray.Length; // ���� ������ ��� ���� ����
            //Debug.Log(e2ExecutionManager.totalNumOfBlocks);
            e2ExecutionManager.PlayAfterDelay();


            //���� ��ư ����
            gameObject.SetActive(false);

        }




    }

}
