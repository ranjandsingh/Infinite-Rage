using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour { 
    public LayerMask collisionMask;
    public float bulletspeed = 10f;
    public int Damage;
    private armRotator armDirection;
    private bool direction;
    void Awake()
    {
        armDirection = FindObjectOfType<armRotator>();
        direction = armDirection.direction;
    }
    void FixedUpdate()
    {
        float moveDistance = bulletspeed * Time.deltaTime;
        CheckCollisions(moveDistance);
        if (direction)
        {
            transform.Translate(Vector3.right * moveDistance);
        }
        if (!direction)
        {
            transform.Translate(Vector3.left * moveDistance);

        }

        Destroy(gameObject, 2f);

    }
    void CheckCollisions(float moveDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward , moveDistance);
        if (hit.collider != null)
        {
            Health playerHealth = hit.transform.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(Damage);
                
            }
            GameObject.Destroy(gameObject);
        }      
        //collided = Physics2D.OverlapCircle(groundCheck.position, hitRadius, collisionMask);
        
    }

    
    
}
