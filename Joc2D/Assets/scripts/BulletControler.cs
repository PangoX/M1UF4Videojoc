using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{

   // public float speedX = 5f;
   // float speedY = 0f;
    public Vector2 speed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       rb.velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {
      // rb.velocity = new Vector2(speedX, speedY);
       rb.velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }


}
