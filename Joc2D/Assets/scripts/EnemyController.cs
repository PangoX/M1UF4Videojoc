using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour

{
    public float maxSpeed = 1f;
    public float speed = 1f;

    private Rigidbody2D rb;
    bool facingRight;
    /*
    public GameObject StartPoint;
    public GameObject EndPoint;

    public float enemySpeed;
    private bool facingRight; */



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * speed);
        float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

        if (rb.velocity.x > -0.01f && rb.velocity.x < 0.01f)
        {
            speed = -speed;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (speed < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (speed > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BARRERA")
        {
            Flip();
        }

    }

    void Flip()
    {
        // Swap Direction
        if (speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector2 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;

        }
    }
}

/* // Start is called before the first frame update
   void Start()
   {
       if (!facingRight)
       {
           transform.position = StartPoint.transform.position;
       }
       else
       {
           transform.position = EndPoint.transform.position;
       }
   }

   // Update is called once per frame
   void Update()
   {
       //OnDrawGizmos();

       if (!facingRight)
       {
           transform.position = Vector3.MoveTowards(transform.position, EndPoint.transform.position, enemySpeed * Time.deltaTime);
           if (transform.position == EndPoint.transform.position)
           {
               facingRight = true;
               GetComponent<SpriteRenderer>().flipX = true;
           }

       }
       if (facingRight)
       {
           transform.position = Vector3.MoveTowards(transform.position, StartPoint.transform.position, enemySpeed * Time.deltaTime);
           if (transform.position == StartPoint.transform.position)
           { 
               facingRight = false;
               GetComponent<SpriteRenderer>().flipX = false;
           }       
       }
   }

   public void OnDrawGizmos()
   {
       Gizmos.DrawLine(StartPoint.transform.position, EndPoint.transform.position);
   }
} */
