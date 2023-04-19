using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyRotate : MonoBehaviour
{
    public Vector3 originPosition;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, 0);
        this.transform.position = originPosition;
    }
}
