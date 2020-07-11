using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;

public class WallManager : MonoBehaviour
{
    [SerializeField]
    float unitsToMove;

    // Start is called before the first frame update

    public void StepForward()
    {
        Vector3 movement = new Vector3(0,0,-unitsToMove);
        this.transform.position += movement;
    }

    public float getUnitsToMove()
    {
        return unitsToMove;
    }
}
