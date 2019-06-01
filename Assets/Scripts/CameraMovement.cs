using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 camOffset;

    void Start()
    {
        camOffset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + camOffset;
    }
}