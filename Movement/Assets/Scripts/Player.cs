using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Ray ray;
    Enemy target = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Targeting!");
                target = hit.transform.gameObject.GetComponent<Enemy>();
                target.Targeted(true);
                Debug.Log("Target Acquired!");
                if (Input.GetMouseButtonDown(0) && !hit.transform.GetComponent<Enemy>().selected)
                {
                    Debug.Log("BANG BANG MUTHAFUKA");

                }
            }
            else
            {
                if(target != null)
                {
                    target.Targeted(false);
                    Debug.Log("Target Lost!");
                }
                
            }
        }
    }
}
