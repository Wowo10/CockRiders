using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public int playernumber;
    public float speed = 10.0f;

    string axis;
    Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        axis = "Fire" + (playernumber + 1);
        rb = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void FixedUpdate()
    {

        if (Input.GetButtonDown(axis))
        {
            rb.velocity = gameObject.transform.right * speed;
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        
    }


}
