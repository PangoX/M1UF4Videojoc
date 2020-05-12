using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip jumpClip;
    Animator anim;
    Rigidbody2D rb;

    public GameObject leftBullet, rightBullet;
    Vector2 bulletPost;
    public float fireRate = 0.5f;
    float nexFire = 0.0f;

    public float speedX;
    public float jumpSpeedY;
    bool facingRight, Jumping;
    float speed;



    // Start is called before the first frame update
    void Start()
    {
        Jumping = false;
        facingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioPlayer = GetComponent<AudioSource>();
        rb.freezeRotation = true; // Mantener personaje estable
       // Bullet = transform.Find("Bullet");

    }

    // Update is called once per frame
    void Update()
    {
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
    void MovePlayer(float playerSpeed)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GROUND")
        {
            anim.SetInteger("Status", 0);
            Jumping = false;
        }

        if (collision.gameObject.tag == "WATER")
        {
            anim.SetInteger("Status", 3);
            UpdateState("Player-Die");
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

    public void Fire()
    {

        bulletPost = transform.position;

       if (!facingRight)
        {
            Instantiate(rightBullet, bulletPost, Quaternion.identity);
        }
       if (facingRight)
        {
            Instantiate(leftBullet, bulletPost, Quaternion.identity);
        }
    }
    }