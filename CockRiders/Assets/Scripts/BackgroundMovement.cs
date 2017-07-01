using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{


    public float movementSpeed;
    public int layer = 0;

    private float defaultX;
    private Camera camera;

    // Use this for initialization
    void Start()
    {
        transform.Translate(new Vector3(0, 0, 10 * layer));
        defaultX = transform.position.x;
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
        transform.position = new Vector3(defaultX + camera.transform.position.x * movementSpeed, transform.position.y, transform.position.z);
    }
}