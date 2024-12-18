using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class Gameover : MonoBehaviourPun
{
    public PlayerMove playerMove;
    //¿Ã∏ß
    public TextMeshProUGUI Pname;
    void Start()
    {

        //scoreText.text = playerMove.score.ToString();
        if (photonView.IsMine == true)
        {
            Pname.text = photonView.Owner.NickName;
        }
        
    }
}
