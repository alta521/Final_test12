//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using UnityEngine.EventSystems;
//using Photon.Realtime;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviourPunCallbacks
//{

//    //자신을 담을 static 변수
//    public static GameManager instance = null;
//    public PlayerMove playerMove;
//    //모든 Player들의 PhotonView 를 가지는 List
//    public List<PhotonView> listPlayer = new List<PhotonView>();

//    // 포인트를 저장하는 변수
//    public int currentScore;

//    // TMP 텍스트 컴포넌트를 연결할 변수
//    public TextMeshProUGUI scoreText;

//    private void Awake()
//    {
//        //만약에 instance 값이 null 이라면
//        if(instance == null)
//        {
//            //instance 에 나 자신을 셋팅
//            instance = this;
//        }
//        //그렇지 않으면
//        else
//        {
//            //나를 파괴하자
//            Destroy(gameObject);
//        }
//    }


//    void Start()
//    {
//        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);

//        //RPC 호출 빈도
//        PhotonNetwork.SendRate = 30;

//        //OnPhotonSerializeView 호출 빈도
//        PhotonNetwork.SerializationRate = 30;

//        SetSpawnPos();

//        //내가 위치해야 하는 idx 구하자
//        int idx = PhotonNetwork.CurrentRoom.PlayerCount - 1;
//        //나의 Player 생성
//        PhotonNetwork.Instantiate("Player", spawnPos[idx], Quaternion.identity);

//        //마우스 포인터를 비활성화
//        Cursor.visible = false;
//    }

//    //spawnPosGroup Transform
//    public Transform trSpawnPosGroup;

//    //Spanw 위치를 담아놓을 변수
//    public Vector3[] spawnPos;
//    //점수
//    //private int currentScore;
//    void SetSpawnPos()
//    {
//        //최대 인원 만큼 spawnPos 의 공간을 할당
//        spawnPos = new Vector3[PhotonNetwork.CurrentRoom.MaxPlayers];

//        //간격 (anlge)
//        float angle = 360 / spawnPos.Length;
//        for(int i = 0; i < spawnPos.Length; i++)
//        {
//            trSpawnPosGroup.Rotate(0, angle, 0);

//            spawnPos[i] = trSpawnPosGroup.position + trSpawnPosGroup.forward * 5;

//            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
//            //go.transform.position = pos;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //만약에 esc 키를 누르면 
//        if(Input.GetKeyDown(KeyCode.Escape))
//        {
//            //마우스 포인터를 활성화
//            Cursor.visible = true;
//        }

//        //마우스 클릭했을 때
//        if(Input.GetMouseButtonDown(0))
//        {
//            //마우스 클릭시 해당 위치에 UI가 없으면
//            if(EventSystem.current.IsPointerOverGameObject() == false)
//            {
//                //마우스 포인터를 비활성화
//                Cursor.visible = false;
//            }
//        }
//    }

//    //참여한 Player 의 PhotonView 추가
//    public void AddPlayer(PhotonView pv)
//    {
//        listPlayer.Add(pv);

//        //모든 Player 가 참여했다면
//        if(listPlayer.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
//        {
//            //Turn 을 시작하자
//            ChangeTurn();
//        }
//    }

//    //현재 Turn Idx
//    int currTurnIdx = -1;
//    public void ChangeTurn()
//    {
//        //방장이 아니라면 함수를 나가자
//        if (PhotonNetwork.IsMasterClient == false) return;

//        if(currTurnIdx != -1)
//        {
//            //발사한 사람 Turn 종료
//            listPlayer[currTurnIdx].RPC("ChangeTurnRpc", RpcTarget.All, false);
//        }

//        //currTurnIdx 을 증가
//        currTurnIdx++;

//        currTurnIdx = currTurnIdx % listPlayer.Count;
//        ////만약에 currTurnIdx 가 3이면
//        //if(currTurnIdx == 3)
//        //{
//        //    //currTurnIdx 을 0 으로 한다.
//        //    currTurnIdx = 0;
//        //}

//        //다음 사람 Turn 시작
//        listPlayer[currTurnIdx].RPC("ChangeTurnRpc", RpcTarget.All, true);


//    }


//    //새로운 인원이 방에 들어왔을때 호출되는 함수
//    public override void OnPlayerEnteredRoom(Player newPlayer)
//    {
//        base.OnPlayerEnteredRoom(newPlayer);

//        print(newPlayer.NickName +  "님이 들어왔습니다!");
//    }
//    public void gameOver()
//    {
//        Debug.Log("gameOver");

//        // 각 플레이어의 점수를 가져오고, 게임 오버 씬으로 이동
//        if (PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount == 1) // 플레이어 수가 1명이면 마스터 클라이언트가 아니어도 실행됨
//        {
//            // 각 플레이어의 점수를 가져오기
//            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
//            {
//                // 1명의 경우 자신만의 점수를 가져오는 로직
//                currentScore = playerMove.score;
//            }
//            else
//            {
//                // 여러 명일 경우, 모든 플레이어의 점수를 가져오는 로직
//                List<int> allScores = new List<int>();

//                // 각 플레이어의 점수를 가져오기 (listPlayer에 있는 모든 PhotonView에서 score를 가져옴)
//                foreach (PhotonView pv in listPlayer)
//                {
//                    // PhotonView를 통해 해당 플레이어의 PlayerMove 컴포넌트에 접근하고 점수를 가져옴
//                    PlayerMove playerMoveScript = pv.GetComponent<PlayerMove>();
//                    if (playerMoveScript != null)
//                    {
//                        allScores.Add(playerMoveScript.score); // 플레이어의 점수를 리스트에 추가
//                    }
//                }

//                // 점수 리스트 출력 (디버깅용)
//                foreach (int score in allScores)
//                {
//                    Debug.Log("Player Score: " + score);
//                }
//            }

//            // 게임 오버 씬으로 이동
//            SceneManager.LoadScene("GameOverScene");
//        }
//    }

//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon.StructWrapping;

public class GameManager : MonoBehaviourPunCallbacks
{
    // 자신을 담을 static 변수
    public static GameManager instance = null;
    public PlayerMove playerMove; // 로컬 플레이어의 PlayerMove
    // 모든 Player들의 PhotonView 를 가지는 List
    public List<PhotonView> listPlayer = new List<PhotonView>();

    // 포인트를 저장하는 변수
    public int currentScore;

    // TMP 텍스트 컴포넌트를 연결할 변수
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        // 만약에 instance 값이 null 이라면
        if (instance == null)
        {
            // instance 에 나 자신을 셋팅
            instance = this;
        }
        else
        {
            // 나를 파괴하자
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);

        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;

        SetSpawnPos();
        
        //생성 위치
        int idx = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        //Player 생성
        GameObject player = PhotonNetwork.Instantiate("Player", spawnPos[idx], Quaternion.identity);

        //PlayerMove를 가져오기
        if (player.GetComponent<PhotonView>().IsMine)
        {
            playerMove = player.GetComponent<PlayerMove>();
        }

        // 마우스 비활성화
        Cursor.visible = false;
    }

    // spawnPosGroup Transform
    public Transform trSpawnPosGroup;

    // Spawn 위치
    public Vector3[] spawnPos;

    void SetSpawnPos()
    {
        // 최대 인원 만큼 spawnPos 의 공간을 할당
        spawnPos = new Vector3[PhotonNetwork.CurrentRoom.MaxPlayers];

        // 간격 (angle)
        float angle = 360 / spawnPos.Length;
        for (int i = 0; i < spawnPos.Length; i++)
        {
            trSpawnPosGroup.Rotate(0, angle, 0);
            spawnPos[i] = trSpawnPosGroup.position + trSpawnPosGroup.forward * 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 만약에 esc 키를 누르면
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 마우스 포인터를 활성화
            Cursor.visible = true;
        }

        // 마우스 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭시 해당 위치에 UI가 없으면
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // 마우스 포인터를 비활성화
                Cursor.visible = false;
            }
        }
    }

    // 참여한 Player 의 PhotonView 추가
    public void AddPlayer(PhotonView pv)
    {
        listPlayer.Add(pv);

        // 모든 Player 가 참여했다면
        if (listPlayer.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // Turn 을 시작하자
            ChangeTurn();
        }
    }

    // 현재 Turn Idx
    int currTurnIdx = -1;

    public void ChangeTurn()
    {
        // 방장이 아니라면 함수를 나가자
        if (PhotonNetwork.IsMasterClient == false) return;

        if (currTurnIdx != -1)
        {
            // 발사한 사람 Turn 종료
            listPlayer[currTurnIdx].RPC("ChangeTurnRpc", RpcTarget.All, false);
        }

        // currTurnIdx 을 증가
        currTurnIdx++;
        currTurnIdx = currTurnIdx % listPlayer.Count;

        // 다음 사람 Turn 시작
        listPlayer[currTurnIdx].RPC("ChangeTurnRpc", RpcTarget.All, true);
    }

    // 새로운 인원이 방에 들어왔을 때 호출되는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        print(newPlayer.NickName + "님이 들어왔습니다!");
    }

    // 게임 오버 함수
    public void gameOver()
    {
        Debug.Log("gameOver");

        // 로컬 플레이어의 PlayerMove 할당
        if (playerMove == null)
        {
            // PhotonNetwork.LocalPlayer가 생성한 로컬 플레이어 오브젝트를 찾는다.
            // PhotonView를 통해 해당 플레이어의 PlayerMove 컴포넌트를 찾는다.
            foreach (PhotonView pv in PhotonNetwork.PhotonViewCollection)
            {
                if (pv.IsMine) // 로컬 플레이어의 PhotonView를 찾는다.
                {
                    playerMove = pv.GetComponent<PlayerMove>(); // PlayerMove 컴포넌트 가져오기
                    break;
                }
            }
        }

        if (playerMove == null)
        {
            Debug.LogError("playerMove가 null입니다.");
            return;
        }

        // 게임 오버 처리는 마스터 클라이언트나 1명일 경우에만 진행
        if (PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            // 1명의 경우 자신만의 점수를 가져오는 로직
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                currentScore = playerMove.score;
            }
            else
            {
                // 여러 명일 경우, 모든 플레이어의 점수를 가져오는 로직
                List<int> allScores = new List<int>();

                // 각 플레이어의 점수를 가져오기 (listPlayer에 있는 모든 PhotonView에서 score를 가져옴)
                foreach (PhotonView pv in listPlayer)
                {
                    // PhotonView를 통해 해당 플레이어의 PlayerMove 컴포넌트에 접근하고 점수를 가져옴
                    PlayerMove playerMoveScript = pv.GetComponent<PlayerMove>();
                    if (playerMoveScript != null)
                    {
                        allScores.Add(playerMoveScript.score); // 플레이어의 점수를 리스트에 추가
                    }
                }

                // 점수 리스트 출력 (디버깅용)
                foreach (int score in allScores)
                {
                    Debug.Log("Player Score: " + score);
                }
            }

            // 게임 오버 씬으로 이동
            SceneManager.LoadScene("gameover");
        }
    }

}

