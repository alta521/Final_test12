using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointS : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var controller = other.gameObject.GetComponent<PlayerMove>();
        if(controller != null )
        {
            controller.score += 1;
            Destroy(gameObject);
        }
    }
}
