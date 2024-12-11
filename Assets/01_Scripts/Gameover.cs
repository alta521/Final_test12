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
    // 점수
    public TextMeshProUGUI scoreText;
    //이름
    public TextMeshProUGUI Pname;
    void Start()
    {
        // PlayerPrefs에서 저장된 점수를 불러옵니다.
        //int lastScore = PlayerPrefs.GetInt("LastScore", 0);  // 기본값은 0

        // 텍스트를 갱신하여 화면에 표시합니다.
        //scoreText.text = playerMove.score.ToString();
        if (photonView.IsMine == true)
        {
            //UI 를 비활성화 하자
            Pname.text = photonView.Owner.NickName;
        }
        
    }
}
