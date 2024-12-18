using UnityEngine;

public class rotmap : MonoBehaviour
{
    // 회전 속도 (각 축별로 설정 가능)
    public Vector3 rotationSpeed = new Vector3(0, 10, 0);

    void Update()
    {
        // 매 프레임마다 오브젝트 회전
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
