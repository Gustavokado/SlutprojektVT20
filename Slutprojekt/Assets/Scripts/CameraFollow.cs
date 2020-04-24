using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player"); //referens till spelaren
    }
    void LateUpdate()
    {
        if (player!=null)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10); //flyttar kameran till spelarens position, måste vara i lateupdate så att det inte blir hackit
            transform.position = newPosition;
        }       
    }
}
