using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="wall")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
