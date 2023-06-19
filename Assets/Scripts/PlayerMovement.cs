using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float movementSpeed = 100;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0) {

            //Vector3 vector3 = new (transform.position.x, transform.position.y, transform.position.z);
            //vector3.x += horizontal*Time.deltaTime* movementSpeed;
            //Vector3 vector3 = horizontal * movementSpeed * Time.deltaTime* transform.forward;
            //transform.Rotate(Vector3.up, horizontal * movementSpeed * Time.deltaTime); // = Vector3.MoveTowards(transform.position, vector3, Time.deltaTime);
            //Debug.Log("Horizontal");
            Vector3 v3 = transform.rotation.eulerAngles; 
            v3.y += horizontal * movementSpeed * Time.deltaTime;
            //rb.MoveRotation(Quaternion.Euler(v3));
            transform.rotation = Quaternion.Euler(v3);
        }


        if ( vertical != 0)
        {
            Vector3 vector3 = transform.position + vertical *movementSpeed * Time.deltaTime * transform.forward;
            transform.position = Vector3.MoveTowards(transform.position, vector3, 0.01f);
            //Debug.Log("Vertical: from" +transform.position+" to: "+vector3);
            //rb.AddForce(transform.forward * vertical*movementSpeed);
           
        }
    }
}
