using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game; 

    private AudioSource audioPlayer; // Pista de so background
    public AudioClip jumpClip; // Pista de so del salt
    Animator anim;
    Rigidbody2D rb;
    public AudioClip shootClip; // Pista de so al disparar

    public GameObject leftBullet, rightBullet; // Prefabs de les bales
    Vector2 bulletPost; // Spawn bales
    public Transform firePost;// Spawn bales
    public float fireRate = 0.5f; // Cadencia de les bales
    float nexFire = 0.0f;

    public float speedX; // Velocitat de l'x
    public float jumpSpeedY; // Velocitat del salt
    bool facingRight, Jumping;
    float speed; // Velocitat





    // Start is called before the first frame update
    void Start()
    {
        Jumping = false;
        facingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioPlayer = GetComponent<AudioSource>();
        rb.freezeRotation = true; // Mantener personaje estable

    }

    // Update is called once per frame
    void Update()
    {
       // bool gamePlaying = game.GetComponent<GameController>().gameState == GameState.Playing;

        // Player Movement
        MovePlayer(speed); 
        Flip();
        MovePlayer(speed);

        // Left Movement
        if (Input.GetKeyDown(KeyCode.A))
        {
            speed = -speedX;
            anim.SetInteger("Status", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            speed = 0;
            anim.SetInteger("Status", 0);
        }

        // Right Movement
        if (Input.GetKeyDown(KeyCode.D))
        {
            speed = speedX;
            anim.SetInteger("Status", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            speed = 0;
            anim.SetInteger("Status", 0);
        }

        // Jump Movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Jumping == false)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
                anim.SetInteger("Status", 2);
                Jumping = true;
                audioPlayer.clip = jumpClip;
                audioPlayer.Play();

            }
        }

        // Shoots
       if (Input.GetKeyDown(KeyCode.L) && Time.time > nexFire)
        {
            nexFire = Time.time + fireRate;
            Fire();
        }


        // VOIDS 
    void MovePlayer(float playerSpeed) // Void moviment Player
        {
          if (playerSpeed < 0 && !Jumping || playerSpeed > 0 && !Jumping)
           {
               anim.SetInteger("Status", 1);
           }

           if (playerSpeed == 0 && !Jumping)
            {
               anim.SetInteger("Status", 0);
            }

            rb.velocity = new Vector3(speed, rb.velocity.y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Colisions del Jugador
    {
        if (collision.gameObject.tag == "GROUND") // Contacte amb GROUND mode IDLE no salta
        {
            anim.SetInteger("Status", 0);
            Jumping = false;
        }

        if (collision.gameObject.tag == "WATER" || collision.gameObject.tag == "ENEMY") // Quan fa contacte amb WATER ENEMY pasa a estat DIE
        {
           // Debug.Log("Die");
            UpdateState("Player-DIE");
            game.GetComponent<GameController>().gameState = GameState.Ended; // L'estat del joc pasa a ENDED

        }

    }

    public void UpdateState (string state = null)
    {
        if (state != null)
        {
            anim.Play(state);
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

    public void Fire() // Void per disparar
    {

       if (!facingRight) // Disparar mirant  a l'esquerra + so
        {
            Instantiate(rightBullet, firePost.position, Quaternion.identity);
            audioPlayer.clip = shootClip;
            audioPlayer.Play();
        }
       if (facingRight) // Dispara mirant a la dreta + so
        {
            Instantiate(leftBullet, firePost.position, Quaternion.identity);
            audioPlayer.clip = shootClip;
            audioPlayer.Play();
        }
    }
    }