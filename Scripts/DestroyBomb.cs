using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBomb : MonoBehaviour
{
    public int damage;
    public int damageAmount;
   private float health = 3;

    //if the object is out of view, destroy it
    private void OnBecameInvisible ()
    {
        Destroy(gameObject);
    }

    //if the object collides with Enemy, destroy it
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Enemy"))
        {
        health -= damageAmount;
       // rb.AddForce((player.transform.position - transform.position).normalized * speed);
        if(health <=0 )
        { 
            Destroy(gameObject);
        }
       
        Destroy(gameObject); //after 3 hits, destroy
        //ScoreManager.Instance.AddPoint();
        }
   }

   

}



