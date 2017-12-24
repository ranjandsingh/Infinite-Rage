using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControler : NetworkBehaviour
{


    public GameObject bullatTrailPrefab;
    public GameObject PlayerCamera;
    public VirtualJoyStick moveJoystick;
    public GameObject Body;
    public Rigidbody2D myRigidbody;
    public GunProperties Gun;    
    public Transform TrailSpawn;
    public GameObject MainCamera;
    private Transform pBody;
    public bool playerDirection;
    public Quaternion bulletRotation;
	public int damage;
    
   

    // Use this for initialization
    void Start()
    {
        //moveJoystick = FindObjectOfType<VirtualJoyStick>();
        myRigidbody = GetComponent<Rigidbody2D>();
        Gun = FindObjectOfType<GunProperties>();
        PlayerCamera.SetActive(false);
        pBody = transform.Find("Body");
        playerDirection = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        damage = Gun.damage;   
        if (!isLocalPlayer)
        {
            return;
            MainCamera.SetActive(false);
        }
        PlayerCamera.SetActive(true);


        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        if (moveJoystick.InputDirection != Vector3.zero)
        {
             x = moveJoystick.InputDirection.x *Time.deltaTime * 250.0f;
             z = moveJoystick.InputDirection.z * Time.deltaTime * 330.0f;
        }


        myRigidbody.velocity = new Vector2(x, z);
        //transform.Rotate(0, x, 0);
        //transform.Translate(x, z, 0);

        
       // BulletFire.SetActive(true);

    }

    [Command]
     public void CmdFire()
    {
      
        Vector2 firePoint = new Vector2(Gun.firePoint.position.x, Gun.firePoint.position.y);
        Vector2 fireDirection = new Vector2(Gun.fireDirection.position.x, Gun.fireDirection.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePoint,fireDirection-firePoint,Gun.Range);
           
         GameObject bullet = (GameObject)Instantiate(bullatTrailPrefab, TrailSpawn.position, TrailSpawn.rotation);         
        NetworkServer.Spawn(bullet);
        Destroy(bullet.gameObject, 7.5f);
    }



    public override void OnStartLocalPlayer()
    {

        Body.GetComponent<MeshRenderer>().material.color = Color.blue;
        //BulletFire.SetActive(true);
        
       
    }

    public void Flip()
    {
        Gun.Flip();
        playerDirection = !playerDirection;
        Vector3 theScale = pBody.localScale;

        theScale.x *= -1f;

        pBody.localScale = theScale;
    }

}
