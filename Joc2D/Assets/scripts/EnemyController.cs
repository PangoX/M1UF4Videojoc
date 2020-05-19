using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour

{
    public GameObject StartPoint;
    public GameObject EndPoint;
    Rigidbody2D rb;

    public float enemySpeed;
    private bool facingRight;

    public float enemyHealth;
    private float curHealth;

    public GameObject healthBar;


    // Start is called before the first frame update
    void Start()
    {
       
       // rb.freezeRotation = true;
        if (!facingRight)
        {
            transform.position = StartPoint.transform.position;
        }
        else
        {
            transform.position = EndPoint.transform.position;
        }

        curHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {

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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
        {
            curHealth -= BulletControler.damage;
            float barLength = curHealth / enemyHealth;
            SetHealthBar(barLength);
            if (curHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetHealthBar(float eHealth)
    {
        healthBar.transform.localScale = new Vector3(eHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
