using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class Gameover : MonoBehaviourPun
{
    //public GameManager GameManager;
    public PlayerMove playerMove;
    // ����
    public TextMeshProUGUI scoreText;
    //�̸�
    public TextMeshProUGUI Pname;
    void Start()
    {
        // PlayerPrefs���� ����� ������ �ҷ��ɴϴ�.
        //int lastScore = PlayerPrefs.GetInt("LastScore", 0);  // �⺻���� 0

        // �ؽ�Ʈ�� �����Ͽ� ȭ�鿡 ǥ���մϴ�.
        //scoreText.text = playerMove.score.ToString();
        if (photonView.IsMine == true)
        {
            //UI �� ��Ȱ��ȭ ����
            Pname.text = photonView.Owner.NickName;
        }
        
    }
}
