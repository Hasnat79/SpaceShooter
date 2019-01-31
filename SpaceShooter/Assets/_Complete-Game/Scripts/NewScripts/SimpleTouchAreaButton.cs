using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleTouchAreaButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

    private bool touched;
    private int pointerID;
    private bool canfire;

    private void Awake()
    {
        
        touched = false;

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canfire = true;

        }
    }
 


    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            canfire = false;
            touched = false;
        }
    }

    public bool CanFire()
    {
        return canfire;
    }
}
