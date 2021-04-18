using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBehavior : MonoBehaviour
{
    public void Taken()
    {
        Destroy(gameObject);
    }
}
