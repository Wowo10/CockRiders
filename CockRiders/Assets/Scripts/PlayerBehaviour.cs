using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public int playernumber;
    public float speed = 10.0f;
    public float friction = 0.05f;
    public float maxvelocity = 10.0f; //times that max velocity is bigger that speed
    public float startvelocity = 0.0f;

    string axis;
    Rigidbody2D rb;

    void Start ()
    {
        axis = "Fire" + (playernumber + 1);
        rb = gameObject.GetComponent<Rigidbody2D>();

        maxvelocity = speed * maxvelocity;

        rb.velocity = gameObject.transform.right * 0 * speed;
    }
	
	void Update ()
    {
        
    }

    void FixedUpdate()
    {

        if (Input.GetButtonDown(axis))
        {
            Debug.Log(rb.velocity.x / maxvelocity);

            float brake = 1 - (rb.velocity.x / maxvelocity);
            rb.velocity += Vector2.right * speed * brake;
        }
        else
        {
            rb.velocity += Vector2.right * speed * friction * -1;

            if (rb.velocity.x < 0)
                rb.velocity = Vector3.zero;
        }
        
    }


}
