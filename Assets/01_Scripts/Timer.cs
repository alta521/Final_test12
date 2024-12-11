using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[RequireComponet(typeof(PhotonView))]
//public class Timer : MonoBehaviourPunCallbacks, IPunObservable
//{
//    System.DateTime startTime = System.DateTime.UtcNow;
//    public Text timerUI;
//    public float ongametime;
//    float testgametime = 120f;
//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if (stream.IsWriting)
//        {
//            stream.SendNext(startTime.Ticks);
//        }
//        else 
//        {
//            startTime = new System.DateTime((long)stream.ReceiveNext());

//        }
//    }
//    public void SetTimer()
//    {
//        ongametime = testgametime - ((System.DateTime.UtcNow.Ticks - startTime.Ticks) / 1000000);
//        ongametime = Mathf.Clamp(0,ongametime, testgametime);
//        timerUI.text = ongametime.ToString("F1");
//    }
//}
public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float time;
    [SerializeField] private float curTime;

    int minute;
    int second;

    private void Awake()
    {
        time = 70;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            text.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (curTime <= 0)
            {
                curTime = 0;
                GameManager.instance.gameOver();
                yield break;
            }
        }
    }
}