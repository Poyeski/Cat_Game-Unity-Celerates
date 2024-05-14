using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    
    public Transform Cat_Stand_Up_0;
    public void GoToItem(ItemData item)
    {
        Cat_Stand_Up_0.position = item.goToPoint.position;
        
    }


}
