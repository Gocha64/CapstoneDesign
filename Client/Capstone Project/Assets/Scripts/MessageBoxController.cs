using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageBoxController : MonoBehaviour
{
    [SerializeField]
    GameObject dialog_name;

    [SerializeField]
    GameObject dialog_text;

    [SerializeField]
    GameObject dialog_Avatar_Image;

    [SerializeField]
    GameObject dialog_Avatar_List;

    [SerializeField]
    GameObject PressToContinue;

    XmlNodeList _xmlList;

    TypingEffect _textEffect;

    //string _state;



    int _clickCount;


    private void Start()
    {
        Init();

    }

    //private void OnEnable()
    //{
    //    Init();
    //}

    // Update is called once per frame
    private void Update()
    {
        UpdateMessageBox();

    }

    private void Init()
    {
        _textEffect = dialog_text.GetComponent<TypingEffect>();
        _xmlList = xmlRead();

        _clickCount = 0;
        DeployMessage(_clickCount);
        _clickCount++;
    }

    private void UpdateMessageBox()
    {

        if (_textEffect.isTyping == false)
        {
            if (!PressToContinue.activeSelf)
                PressToContinue.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                DeployMessage(_clickCount);
                _clickCount++;
            }
        }
        else
        {
            if (PressToContinue.activeSelf)
                PressToContinue.SetActive(false);
        }

    }

    private void DeployMessage(int seq)
    {
        try
        {

            if (_clickCount >= _xmlList.Count)
            {

                if (Managers.MessageBox.XmlNameToRead.Contains("Start"))
                {
                    GameObject stageScene = GameObject.Find("@Scene");
                    stageScene.GetComponent<StageScene>().StartGenerating();
                }
                else if(Managers.MessageBox.XmlNameToRead.Contains("End"))
                {
                    Managers.Stage.HandleSuccess();
                }
                Destroy(gameObject);
            }
            else
            //Debug.Log(_xmlList[seq]["text"].InnerText);
            {
                DeployAvatar(_xmlList[seq]["avatar"].InnerText);
                _textEffect.m_Message = _xmlList[seq]["text"].InnerText;
                dialog_name.GetComponent<Text>().text = _xmlList[seq]["name"].InnerText;
                StartCoroutine(_textEffect.Typing());
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log("Failed to deploy messages");
            if (Managers.MessageBox.XmlNameToRead.Contains("Start"))
            {
                GameObject stageScene = GameObject.Find("@Scene");
                stageScene.GetComponent<StageScene>().StartGenerating();
            }
            else if (Managers.MessageBox.XmlNameToRead.Contains("End"))
            {
                Managers.Stage.HandleSuccess();
            }
            Destroy(gameObject);
        }

    }

    void DeployAvatar(string avatar)
    {
        try
        {
            if (avatar.Contains("character"))
                avatar.Replace("character", "Character");

            //Debug.Log($"{avatar}");

            if (avatar.Contains("Character"))
            {
                Transform tf = dialog_Avatar_List.transform.Find(avatar + "WithLenderCamera");

                if (tf == null)
                {
                    Managers.Resource.Instantiate(avatar + "WithLenderCamera", dialog_Avatar_List.transform);
                    tf = dialog_Avatar_List.transform.Find(avatar + "WithLenderCamera");
                    //Debug.Log($"new tf.name: {tf.name}");
                }
                Texture tex = (Texture)Resources.Load("Texture/" + avatar, typeof(Texture));
                dialog_Avatar_Image.GetComponent<RawImage>().texture = tex;
            }
            else
            {
                dialog_Avatar_Image.GetComponent<RawImage>().texture = null;
                Debug.Log($"An avatar name should contain a substring \"Character\" {avatar}");
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log($"Failed to deploy avatar: {avatar}");
        }


        //1. 이름에 Character가 들어갈경우
        //1.1 avatar리스트에서 찾을 수 있을 경우
        //1.2 avatar리스트에서 찾을 수 없을 경우
        //2. 아닌경우
    }

    public XmlNodeList xmlRead()
    {

        string path = string.Format("DialogScript/{0}", Managers.MessageBox.XmlNameToRead);

        try
        {
            TextAsset asset = Resources.Load<TextAsset>(path);

            //Debug.Log($"xml content: {asset.text} ");

            XmlDocument xml = new XmlDocument();

            xml.LoadXml(asset.text); //"D:\\test\\config.xml" == @"D:\test\config.xml" 

            XmlNodeList xmlList = xml.SelectNodes("/Message/dialog");

            return xmlList;

        }
        catch (NullReferenceException e)
        {
            Debug.Log($"Failed to load DialogScript from {path}");
            Clear();
            return null;
        }


        void Clear()
        {
            _clickCount = 0;
            _textEffect = null;
            _xmlList = null;
        }


        //foreach (XmlNode xnl in xmlList)
        //{
        //    _xml = xnl;
        //    dialog_text.GetComponent<Text>().text = _xml["text"].InnerText;
        //    Debug.Log($"{xnl.InnerText}");
        //    yield return null;
        //}
    }

}
