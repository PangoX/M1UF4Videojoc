using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{

    public Vector2 speed;
    Rigidbody2D rb;

    public static float damage;
    public float damageRef;

    // Start is called before the first frame update
     void Awake()
    {
        damage = damageRef;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {
       rb.velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            // Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("GROUND"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("BARRERA"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
