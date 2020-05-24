using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour

{
    public GameObject StartPoint; // Point Inici Recorregut
    public GameObject EndPoint; //  Point Final Recorregut
    Rigidbody2D rb;

    public float enemySpeed; // Velocitat de l'enemic
    private bool facingRight; // Bolea per girar l'enemic

    public float enemyHealth; // Valor barra de vida
    private float curHealth; // Resta de vida

    public GameObject healthBar;


    // Start is called before the first frame update
    void Start()
    {
       
        if (!facingRight) // Posició esquerra
        {
            transform.position = StartPoint.transform.position;
        }
        else // Posició dreta
        {
            transform.position = EndPoint.transform.position;
        }

        curHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (!facingRight) // S'actualitza el moviment cap a l'esquerra
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.transform.position, enemySpeed * Time.deltaTime);
            if (transform.position == EndPoint.transform.position)
            {
                facingRight = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }

        }
        if (facingRight) // S'actualitza el moviment cap a la dreta
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPoint.transform.position, enemySpeed * Time.deltaTime);
            if (transform.position == StartPoint.transform.position)
            {
                facingRight = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision) // Colisions de l'enemic
    {
        if (collision.gameObject.CompareTag("BULLET")) // Colision amb la bala
        {
            curHealth -= BulletControler.damage; // Es redueiex la vida per el valor de la bala
            float barLength = curHealth / enemyHealth; // Es disminuex la vida per proporció
            SetHealthBar(barLength);
            if (curHealth <= 0) // Si la barra de vida arriba a 0 s'elimina l'enemic
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetHealthBar(float eHealth) // Barra de vida
    {
        healthBar.transform.localScale = new Vector3(eHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
