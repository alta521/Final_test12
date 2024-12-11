using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
    //�ִ� HP
    float maxHP = 100;
    //���� HP
    float currHP = 0;
    //HPBar
    public Image hpBar;
    public GameManager gameManager;
    void Start()
    {
        //���� HP �� �ִ� HP �� ����
        currHP = maxHP;
    }

    //������ �¾����� HP �� �ٿ��ִ� �Լ�    
    public void UpdateHP(float damage)
    {
        //���� HP �� damage ��ŭ �ٿ��ش�.
        currHP += damage;
        //HPBar �����Ѵ�.
        hpBar.fillAmount = currHP / maxHP;
        if (currHP <= 0)
        {
            if (gameManager != null)
            {
                gameManager.gameOver();
            }
        }
    }
}