using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    //ĳ���Ͱ� ���ο� ����� �� 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"�浹�� ��ü: {collision.gameObject}");
        if (collision.gameObject.name.Contains("Character"))
        {
            Debug.Log("����ȹ��!");
            Managers.Coin.AcquireCoin(gameObject);
        }
    }

}
