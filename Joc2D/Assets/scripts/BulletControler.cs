using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{

    public Vector2 speed;
    Rigidbody2D rb;

    public static float damage; // Dany Bala
    public float damageRef; // Dany bala grafic al Game

    // Start is called before the first frame update
     void Awake()
    {
        damage = damageRef; // Equivalencia
    }

    void Start()
    {
        damage = damageRef;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {
       rb.velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) // Colisions de les bales
    {
        if (collision.gameObject.CompareTag("ENEMY")) // Desapareix la bala amb contacte ENEMY
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("GROUND")) // Desapareix la bala amb contacte GROUND
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("BARRERA")) // Desapareix la bala i la barrera amb contace BARRERA
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
