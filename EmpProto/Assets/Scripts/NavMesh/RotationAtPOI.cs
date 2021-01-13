using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAtPOI : MonoBehaviour
{
    public GameObject rotTransform;
    Transform thisTransform;

    public NavWander nav;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(nav.isBusy && !nav.isWalking)
        {
            thisTransform.transform.rotation = nav.pOI[nav.currentPOI].transform.rotation;
        }
    }
}
