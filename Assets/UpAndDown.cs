using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public Quaternion originRotation;
    public bool TestStart;
    public bool TestEnd;
    public bool DataWritten;
    public float TimeCounterStart;
    public float TimeCounterEnd;

    public GameObject Manager;
    public GameObject CircleSocket;
    public GameObject TriangleSocket;
    public GameObject SquareSocket;
    public GameObject PentagonSocket;
    public GameObject HexagonSocket;

    // Start is called before the first frame update
    void Start()
    {
        originRotation = this.transform.rotation;
        TestStart = false;
        TestEnd = false;
        DataWritten = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0f, this.transform.position.y, -6.532f);
        this.transform.rotation = originRotation;
        if (TestEnd == false)
        {
            TimeCounterEnd += Time.deltaTime;
            if (this.transform.position.y < 4.5f && CircleSocket.GetComponent<Socket>().TaskDone == true)
            {
                TestEnd = true;
                string fileName = Manager.GetComponent<ManagerScript>().TestParticipantNumber.ToString();
                List<float> data = new List<float> { TimeCounterStart, CircleSocket.GetComponent<Socket>().TimeCounter, TriangleSocket.GetComponent<Socket>().TimeCounter, SquareSocket.GetComponent<Socket>().TimeCounter, PentagonSocket.GetComponent<Socket>().TimeCounter, HexagonSocket.GetComponent<Socket>().TimeCounter, TimeCounterEnd, Manager.GetComponent<ManagerScript>().NumberOfResets};
                WriteToCSV(fileName, data);
            }
        }
        if (TestStart == false)
        {
            TimeCounterStart += Time.deltaTime;
            if (this.transform.position.y > 4.5f)
            {
                TestStart = true;
            }
        }
        if (this.transform.position.y > 7f)
        {
            this.transform.position = new Vector3(0f, 7f, -6.532f);
        }
        if (this.transform.position.y < 3.62f)
        {
            this.transform.position = new Vector3(0f, 3.62f, -6.532f);
        }
    }

    public void WriteToCSV(string fileName, List<float> data)
    {
        string path = Application.dataPath;

        string filePath = Path.Combine(path, fileName);

        using (StreamWriter sw = new StreamWriter(filePath, false))
        {
            sw.WriteLine(string.Join(", ", data));
        }
    }
}
