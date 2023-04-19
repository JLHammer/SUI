using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public GameObject CorrectModule;
    public float TimeCounter;
    public bool TaskDone;
    public Vector3 Finalposition;


    // Start is called before the first frame update
    void Start()
    {
        TaskDone = false;
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
        Debug.Log((CorrectModule.transform.eulerAngles.y + this.transform.eulerAngles.y + 250f) % 360f);
        Debug.Log(Vector3.Distance(this.transform.position, CorrectModule.transform.position));
        //Debug.Log(CorrectModule.transform.eulerAngles.y);
        if (TaskDone)
        {
            this.transform.position = Finalposition;
        }
        if (Vector3.Distance(this.transform.position, CorrectModule.transform.position) < 0.5f)
        {
            CorrectModule.transform.SetParent(this.transform);
            //CorrectModule.transform.position.
            TaskDone = true;
            Finalposition = this.transform.position;
        }
    }
}
