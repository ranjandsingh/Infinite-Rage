using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;
    public Vector3 InputDirection { set; get; }
    public bool Joystik4Player;
    public GunProperties Gun;
    private float x;
    private float y;
    

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
        Gun = FindObjectOfType<GunProperties>();
    }

    void Update()
    {
        if (x >= 1.0f || x <= -1.0f || y >= 1.0f || y <= -1.0f)
            if(!Joystik4Player)
                Gun.Shoot();
            
    }

	public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

              x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
              y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputDirection = new Vector3(x, 0, y);
            //add fire option here replace the function called inputDirection.normalized with custom fuunctions
   
            if (InputDirection.magnitude > 1)
            {
                fire();
            }
            else
            {
                InputDirection = InputDirection;
            }

            joystickImg.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3), InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
            
        }
        //Debug.Log("Draging");
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
       OnDrag(ped);
        

        
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        x = 0;
        y = 0;
       // Gun.startFire = false;
    }

    void fire()
    {
        if (Joystik4Player)
        {
            InputDirection = InputDirection.normalized;
        }
        else
        {
           // Gun.startFire = true;
            Gun.Shoot();
            InputDirection = InputDirection.normalized;
           
        }
    }

}
