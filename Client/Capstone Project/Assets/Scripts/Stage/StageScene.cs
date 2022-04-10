using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        bool success = Managers.Map.GenerateMap();
        success = Managers.Coin.GenerateCoin();

        if (!success) // ��, ���� �� �� �ϳ��� ���� ���� ��
            Managers.Scene.LoadScene(Define.Scene.Lobby);
        else
        {
            GameObject character = Managers.TargetObject.GetTargetObject("Character");
            Vector3 startPosition = character.transform.position;
            if (startPosition == null)
            {
                Debug.Log("StartPosition wasn't set");
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }

            GameObject be2ProgEnv = Managers.Resource.Instantiate("Blocks Engine 2");
            if (be2ProgEnv == null)
            {
                Debug.Log("Wrong engine name");
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }
        }
    }
    public override void Clear()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
