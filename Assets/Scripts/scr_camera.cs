using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class scr_camera : MonoBehaviour
{
    scr_controlPlayer controlPlayerScr;
    public Transform player;
    public Vector3 offset;
    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
     

        controlPlayerScr = player.GetComponent<scr_controlPlayer>();
    }
    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position + offset;
            newPosition.y = transform.position.y;

            transform.position = newPosition;
        }
    }
}
