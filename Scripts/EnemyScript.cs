using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    public int health;
    private Rigidbody2D rb;
    public float stopDistance;

    
    //Basic AI for enemy to chase the player
    [HideInInspector] public Transform player;
    public float speed;


    // if the hralth is less than 0, it will be killed 
    public void TakeDamage( int damageAmount)
    {
        health -= damageAmount;
        //rb.AddForce((player.transform.position - transform.position).normalized * speed);
        if(health <=0 )
        { 
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player !=null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
            }
        }
    }

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }


}
