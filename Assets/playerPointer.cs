using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPointer : MonoBehaviour
{   
    // Update is called once per frame
    void Update ()
    {
        if (GvrControllerInput.AppButtonDown)
        {
            print("Click App button down");
        }

        if (GvrControllerInput.ClickButtonDown)
        { 
            print("Click Touchpad");
        }

        /*if (GvrControllerInput.IsTouching)
        {
            print("Position Vector: " + GvrControllerInput.TouchPos);
        }*/
    }
    public void clickedGreenButton() {
        print("Clicked the big green button");
    }
}