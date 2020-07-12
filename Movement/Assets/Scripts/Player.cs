using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Ray ray;
    Enemy target = null;
    Bullets playerGun;

    // Start is called before the first frame update
    void Start()
    {
        playerGun = this.GetComponent<Bullets>();
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
                //Debug.Log("Targeting!");
                target = hit.transform.gameObject.GetComponent<Enemy>();
                target.Targeted(true);
                //Debug.Log("Target Acquired!");
                if (Input.GetMouseButtonDown(0) && !hit.transform.GetComponent<Enemy>().selected)
                {
                    playerGun.Shoot(hit.transform.gameObject);
                    hit.transform.GetComponent<Enemy>().selected = true;
                }
            }
            else
            {
                if(target != null)
                {
                    target.Targeted(false);
                    //Debug.Log("Target Lost!");
                }
                
            }
        }
    }
}
