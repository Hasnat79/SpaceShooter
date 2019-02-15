using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float smoothing;
    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;
    
    
    //adding fire option into movement area
    private bool canfire;

    private void Awake()
    {
        direction = Vector2.zero;
        touched = false;

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            Time.timeScale = 1f;
            touched = true;
            pointerID = data.pointerId;
            //Set out start point
            origin = data.position;
            canfire = true;
        }
    }
    public void OnDrag(PointerEventData data)
    {
        //compare the difference between out start point and current pointer pos
        if (data.pointerId == pointerID)
        {


            Vector2 currentPosition = data.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }
    }


    public void OnPointerUp(PointerEventData data)
    {
        
        if (data.pointerId == pointerID)
        {
            
           
            direction = Vector2.zero;
            touched = false;
            canfire = false;
            Time.timeScale = 0.199f;
        }
    }
    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection,direction,smoothing);
        return smoothDirection;
    }
    public bool CanFire()
    {
        return canfire;
    }
}
