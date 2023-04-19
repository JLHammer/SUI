using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public Quaternion originRotation;
    // Start is called before the first frame update
    void Start()
    {
        originRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(-0.02f, this.transform.position.y, -6.83f);
        this.transform.rotation = originRotation;
    }
}
