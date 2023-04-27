using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public GameObject CorrectModule;
    public GameObject MachineSphere;
    public float TimeCounter;
    public bool TaskDone;
    public Vector3 Finalposition;
    public Vector3 FinalRotation;
    public bool ComponentsDeleted;
    public Component[] components;
    public Vector3 TempRotation;
    public Vector3 DirectionTest;
    public float Testing;
    public Material TaskDoneColor;


    // Start is called before the first frame update
    void Start()
    {
        TaskDone = false;
        ComponentsDeleted = false;
        components = CorrectModule.GetComponents<Component>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TaskDone)
        {
            TimeCounter += Time.deltaTime;
        }
        //((CorrectModule.transform.eulerAngles.y + this.transform.eulerAngles.y + 250f) % 360f) < 20f &&
        //this.transform.position = new Vector3(-2f, this.transform.position.y, 5f);
        //Debug.Log((CorrectModule.transform.eulerAngles.y + this.transform.eulerAngles.y + 250f) % 360f);
        //Debug.Log(Vector3.Distance(this.transform.position, CorrectModule.transform.position));
        //Debug.Log(CorrectModule.transform.eulerAngles.y);
        //Debug.Log(CorrectModule.transform.rotation.x);
        TempRotation = CorrectModule.transform.eulerAngles + new Vector3(-90f, 0, 0);
        //Debug.Log(Quaternion.Angle(Quaternion.Euler(MachineSphere.transform.eulerAngles), Quaternion.Euler(TempRotation)));
        DirectionTest = (MachineSphere.transform.position - CorrectModule.transform.position).normalized;
        Testing = Vector3.Dot(DirectionTest, CorrectModule.transform.forward);
        Debug.Log(Testing);
        if (TaskDone)
        {
            //this.transform.position = Finalposition;
            CorrectModule.transform.localPosition = Finalposition;
            CorrectModule.transform.localEulerAngles = FinalRotation;
        }
        if (Vector3.Distance(this.transform.position, CorrectModule.transform.position) < 0.7f && Testing > 0.9f && ComponentsDeleted == false)
        {
            ComponentsDeleted = true;
            CorrectModule.transform.SetParent(this.transform);
            //CorrectModule.transform.position.
            TaskDone = true;
            Finalposition = CorrectModule.transform.localPosition;
            FinalRotation = CorrectModule.transform.localEulerAngles;
            CorrectModule.GetComponent<BoxCollider>().enabled = false;
            CorrectModule.GetComponent<MeshRenderer>().material = TaskDoneColor;
        }
    }
}
