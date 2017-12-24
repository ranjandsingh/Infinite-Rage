using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProperties : MonoBehaviour {

    public int damage = 10;
    public float firerate = 4;
    public float Range = 100f;
    public float timeToFire = 0f;
    public Transform firePoint;
    public Transform fireDirection;
    public PlayerControler myPlayer;
    public float bulletSpeed = 150;
    //public bool startFire;


	// Use this for initialization
	void Start () {
        myPlayer = FindObjectOfType<PlayerControler>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
        //if (startFire)
         //   Shoot();

		
	}

    public void Shoot()
    {
        if (Time.time >= timeToFire)
        {
            myPlayer.CmdFire();
            timeToFire = Time.time + 1 / firerate;
        }
    }
    public void Flip()
    {
        firePoint.Rotate(Vector3.forward, 180f);
    }
}
