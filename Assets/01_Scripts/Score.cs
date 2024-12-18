using UnityEngine;
using UnityEngine.UI; // UI ��� ����� ���� �߰�
using TMPro;

public class Score : MonoBehaviour
{
    //public PlayerMove playerMove; // PlayerMove�� ������ ����
    public TMP_Text scoreText;     // UI �ؽ�Ʈ�� ����

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
