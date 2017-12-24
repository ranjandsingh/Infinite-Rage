using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBullet : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {

        GameObject hit = collision.gameObject;
        Debug.Log("we hit" + hit.transform.name);
        Health health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
