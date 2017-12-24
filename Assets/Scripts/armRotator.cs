using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armRotator : MonoBehaviour {

    public VirtualJoyStick joystick2;
    private Vector3 input02;
    public int offsetZ;
    public bool direction;
    private PlayerControler player;

	// Use this for initialization
	void Start () {
        direction = true;
        player = FindObjectOfType<PlayerControler>();
	}
	
	// Update is called once per frame
	void Update () {
        if (joystick2.InputDirection != Vector3.zero )
        {
            input02 = new Vector3(joystick2.InputDirection.x, joystick2.InputDirection.z, 0);
        }
        Vector3 diffrence = input02 - Vector3.zero;
        diffrence.Normalize ();
        float rotZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;

        
        if (direction)
        {
            rotZ = -rotZ;
            offsetZ = 0;
        }

        if (!direction)
        {
            rotZ = -rotZ;
            offsetZ = 180;
        }
        
        transform.rotation = Quaternion.Euler(180f, 0f,(rotZ + offsetZ));
       /* if (rotZ > 0f && rotZ < 100f || rotZ < 0f && rotZ > -90f)
        {
            if (direction == false)
            {
                direction = true;

                Flip();
            }
        }

        if (rotZ > 100f && rotZ < 180f || rotZ < -90f && rotZ > -180f)

            if (direction == true)
            {
                direction = false;

                Flip();
            }*/

		
	}

    void Flip()
    {
        if (direction == false && player.playerDirection == true || direction == true && player.playerDirection == false)
        {
            player.Flip();
        }

        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;

        transform.localScale = theScale;
    }
}
