using UnityEngine;

public class rotmap : MonoBehaviour
{
    // ȸ�� �ӵ� (�� �ະ�� ���� ����)
    public Vector3 rotationSpeed = new Vector3(0, 10, 0);

    void Update()
    {
        // �� �����Ӹ��� ������Ʈ ȸ��
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
