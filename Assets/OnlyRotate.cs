using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyRotate : MonoBehaviour
{
    public Vector3 originPosition;
    public Quaternion originRotation;
    public Vector3 originRotationDegrees;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = this.transform.position;
        originRotation = this.transform.rotation;
        originRotationDegrees = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, 0);
        this.transform.position = originPosition;
        //this.transform.rotation = new Quaternion(originRotation.x, this.transform.eulerAngles.y, originRotation.z, originRotation.w);
        this.transform.eulerAngles = new Vector3(originRotationDegrees.x, this.transform.eulerAngles.y, originRotationDegrees.z);
    }
}
