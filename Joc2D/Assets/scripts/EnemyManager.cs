using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

     public float enemyHealth;
     private float curHealth;

    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BULLET")
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
