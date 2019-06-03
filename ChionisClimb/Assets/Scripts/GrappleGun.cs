using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    [SerializeField]
    //Stores a reference to a camera
    Camera cam;

    [SerializeField]
    //Stores a reference to the grapple prefab
    GameObject grapplePrefab;

    [SerializeField]
    //Stores the active grapple
    GameObject grapple;

    [SerializeField]
    //Range of grapple
    float grappleRange = 10f;

    [SerializeField]
    //Stores mouse state
    bool mouse1Pressed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the left mouse button.  Will need to hold down to maintain grapple.
        if(Input.GetMouseButton(0))
        {
            mouse1Pressed = true;
            if (!grapple)
            {
                ShootRay();
            }
            else
            {
                Debug.Log("GrappleExists!");
            }
            
        }
        else
        {
            mouse1Pressed = false;
            Destroy(grapple);
        }
    }

    //Project a ray to determine shot
    void ShootRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, grappleRange))
        {
            Debug.Log("Hit!");
            grapple = Instantiate(grapplePrefab, hit.point, Quaternion.identity, transform);
            if (grapple.GetComponent<SpringJoint>())
            {
                Debug.Log("Hit2");
                grapple.GetComponent<SpringJoint>().connectedBody = GetComponent<Rigidbody>();
            }
        }
        else
        {
            
        }

    }

}
