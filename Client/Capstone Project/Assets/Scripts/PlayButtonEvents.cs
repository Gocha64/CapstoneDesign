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
            //����
            GameObject.Find("BE2 Execution Manager").GetComponent<BE2_ExecutionManager>().PlayAfterDelay();

            //��� ���� ���� ����
            GameObject.Find("Canvas Blocks Selection").GetComponent<Canvas>().enabled = false;

            //ī�޶� ��ȯ
            GameObject.Find("QuaterView Camera").GetComponent<CameraController>().ChangeToQuarterView();

            //���� ��ư ����
            gameObject.SetActive(false);
        }




    }

}
