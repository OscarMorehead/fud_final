using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPMovement : MonoBehaviour
{

    public float force = 1f;
    public float damping = 1f;
    public float smoothing = 1f;
    public float returnForce = 10f;

    public Vector3 velocity;
    public Vector3 smoothedVelocity;
    public Vector3 returnVelocity;

    public Vector3 defaultLocation;

    // Start is called before the first frame update
    void Start()
    {
        defaultLocation = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * force * Time.deltaTime;

        returnVelocity += velocity * returnForce * Time.deltaTime;

        //velocity -= returnVelocity * Time.deltaTime;
        velocity -= velocity * damping * Time.deltaTime;

        smoothedVelocity = Vector3.Lerp(smoothedVelocity, velocity, smoothing * Time.deltaTime);

        transform.localPosition = defaultLocation + smoothedVelocity;
    }
}
