using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int playernumber;
    public float speed = 10.0f;
    public float friction = 0.05f;
    public float maxvelocity = 10.0f; //times that max velocity is bigger that speed
    public float startvelocity = 0.0f;

    public char currentanswer;
    public float jumpHeight = 20.0f;
    public float jumpForce = 10.0f;
    
    public float groundLevelY;
    string fire, jump, aans, bans, cans;
    Rigidbody2D rb;

    void Start()
    {
        groundLevelY = transform.position.y;
        fire = "Fire" + (playernumber + 1);
        jump = "Jump" + (playernumber + 1);
        aans = "Aanswer" + (playernumber + 1);
        bans = "Banswer" + (playernumber + 1);
        cans = "Canswer" + (playernumber + 1);

        rb = gameObject.GetComponent<Rigidbody2D>();

        maxvelocity = speed * maxvelocity;

        rb.velocity = gameObject.transform.right * 0 * speed;
    }

    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishLine"))
        {
            Debug.Log(name + " won!!!!!!1111one");
        }
        if (collision.CompareTag("Obstacle"))
        {
            if(rb.velocity.y <= 0.0f + Mathf.Epsilon)
                OnObstacleHit();
        }
    }


    private void OnObstacleHit()
    {
        Debug.Log(name + " is retarded");
        //TODO
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown(fire))
        {
            float brake = 1 - (rb.velocity.x / maxvelocity);
            rb.velocity += Vector2.right * speed * brake;
        }
        else
        {
            rb.velocity += Vector2.right * speed * friction * -1;

            if (rb.velocity.x < 0)
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (Input.GetButtonDown(jump))
        {
            if (rb.velocity.y >= 0.0f - Mathf.Epsilon && rb.velocity.y <= 0.0f + Mathf.Epsilon)
                OnJump();
        }


        if(transform.position.y > groundLevelY)
        {
            rb.AddForce(new Vector2(0, -30.81f), ForceMode2D.Force);
        }
        else if (transform.position.y < groundLevelY)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, groundLevelY, transform.position.z), Quaternion.identity);
            rb.velocity = new Vector3(rb.velocity.x, 0, 0); ;
        }


        if (Input.GetButtonDown(aans))
        {
            currentanswer = 'a';
        }

        if (Input.GetButtonDown(bans))
        {
            currentanswer = 'b';
        }

        if (Input.GetButtonDown(cans))
        {
            currentanswer = 'c';
        }
    }

    public void WinQuiz()
    {
        Debug.Log(name + " has won");
    }

    public void LoseQuiz()
    {
        Debug.Log(name + " has lost");
    }

    private void OnJump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
