using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemyController : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject EndPoint;

    public float enemySpeed;
    private bool facingRight;



    // Start is called before the first frame update
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
}
