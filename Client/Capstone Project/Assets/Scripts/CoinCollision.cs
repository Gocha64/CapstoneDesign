using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    //ĳ���Ͱ� ���ο� ����� �� 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("����ȹ��!");

        Managers.Coin.AcquireCoin(gameObject);
    }

}
