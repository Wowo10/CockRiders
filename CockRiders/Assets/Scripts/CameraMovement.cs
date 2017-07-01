using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private GameObject[] players;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0].transform;
    }

    void Update()
    {
        foreach(GameObject x in players)
        {
            if (x.transform.position.x > player.position.x)
                player = x.transform;
        }
        transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z); // Camera follows the player with specified offset position
    }

}
