using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingBeingMoved : MonoBehaviour
{
    public GameObject JoyStickObject;
    public Vector3 OriginalPosition;


    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = JoyStickObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(OriginalPosition, JoyStickObject.transform.position) > 0.1f)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.position -= (OriginalPosition - JoyStickObject.transform.position) * Time.deltaTime;
        }
        // this.transform.position += (OriginalPosition - JoyStickObject.transform.position) * Time.deltaTime;
        else
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
