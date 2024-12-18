using UnityEngine;
using UnityEngine.UI; // UI 요소 사용을 위해 추가
using TMPro;

public class Score : MonoBehaviour
{
    //public PlayerMove playerMove; // PlayerMove를 참조할 변수
    public TMP_Text scoreText;     // UI 텍스트를 연결

    //private void Start()
    //{
    //    playerMove = FindObjectOfType<PlayerMove>();

    //    if (playerMove == null)
    //    {
    //        Debug.LogError("PlayerMove not found in the scene!");
    //    }
    //}
    private void Update()
    {

        if (GameManager.instance != null  && scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.instance.currentScore.ToString();
        }
    }
}
