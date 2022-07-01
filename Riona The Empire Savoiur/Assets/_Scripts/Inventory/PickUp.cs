///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   PickUp.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/01/29
///   Description       : PickUp script is a container for pickup items
///   Revision History  : 1st ed. class created with 3 variables for pickup name , pickup sprite, pickup count
///                    
///----------------------------------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @Negar: please make scriptable objects for pick up items for scalability
public class PickUp
{
    public string name;
    public Sprite pickupImage;
    public int count;
}
