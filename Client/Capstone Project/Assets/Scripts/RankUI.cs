using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    float waitTime = 0;
    public IEnumerator WaitForPacket()
    {
        yield return new WaitForSeconds(2.0f);
    }

    void Start()
    {
        
    }

    public void OnEnable()
    {
        Managers.User.RankPacketArrival = false; // ��Ŷ�� ������ ������ ����ϱ� ���� ����
        Managers.Login.LoadTop30();
        Debug.Log($"RankUI ������ RankPacketArriavl: {Managers.User.RankPacketArrival}");
        while (Managers.User.RankPacketArrival == false) // �����κ��� ��Ŷ�� ������ ������ ���
        {
            Debug.Log($"While�� ������ RankPacketArriavl: {Managers.User.RankPacketArrival}");
            // busy wait for rank packet
            waitTime += Time.deltaTime;
            if (waitTime >= 10)
            {
                waitTime = 0;
                return;
            }
        }

        //StartCoroutine("WaitForPacket");

        Debug.Log("SetUI ��Ⱦ����");

        Transform rank = gameObject.transform.Find("Rank");
        for (int i = 1; i <= 3; i++)
        {
            Transform ranki = rank.Find("Scroll View").Find("Viewport").Find("Content").Find($"Rank{i}");
            Text name = ranki.Find("Name").gameObject.GetComponent<Text>();
            ChallengeRankerInfo ranker;

            if (Managers.User.ChallengeTop30.TryGetValue(i, out ranker) == false)
                break;

            name.text = ranker.userName;

            Text stars = ranki.Find("Text").gameObject.GetComponent<Text>();

            stars.text = ranker.totalStars.ToString();
        }
    }

    // Start is called before the first frame update
    public void SetUI()
    {

        
    }




    // Update is called once per frame
}
