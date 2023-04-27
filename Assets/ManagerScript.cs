using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public int TestParticipantNumber;

    private Vector3 ButtonOrigin;
    private Quaternion ButtonRotation;

    public GameObject CircleObject;
    private Vector3 CircleOrigin;
    private Quaternion CircleRotation;
    public GameObject TriangleObject;
    private Vector3 TriangleOrigin;
    private Quaternion TriangleRotation;
    public GameObject SquareObject;
    private Vector3 SquareOrigin;
    private Quaternion SquareRotation;
    public GameObject PentagonObject;
    private Vector3 PentagonOrigin;
    private Quaternion PentagonRotation;
    public GameObject HexagonObject;
    private Vector3 HexagonOrigin;
    private Quaternion HexagonRotation;

    public float ResetCoolDown;
    public float NumberOfResets;

    // Start is called before the first frame update
    void Start()
    {
        ButtonOrigin = this.transform.position;
        ButtonRotation = this.transform.rotation;

        CircleOrigin = CircleObject.transform.position;
        CircleRotation = CircleObject.transform.rotation;

        TriangleOrigin = TriangleObject.transform.position;
        TriangleRotation = TriangleObject.transform.rotation;

        SquareOrigin = SquareObject.transform.position;
        SquareRotation = SquareObject.transform.rotation;

        PentagonOrigin = PentagonObject.transform.position;
        PentagonRotation = PentagonObject.transform.rotation;

        HexagonOrigin = HexagonObject.transform.position;
        HexagonRotation = HexagonObject.transform.rotation;

        ResetCoolDown = 0f;
        NumberOfResets = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ResetCoolDown -= Time.deltaTime;
        if (ResetCoolDown >= 0f)
        {
            this.transform.position = new Vector3(ButtonOrigin.x, ButtonOrigin.y, ButtonOrigin.z);
            this.transform.rotation = ButtonRotation;
        }
        if (ResetCoolDown < 0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, ButtonOrigin.y, ButtonOrigin.z);
            this.transform.rotation = ButtonRotation;
            if (this.transform.position.x > -3.7f)
            {
                this.transform.position = ButtonOrigin;
            }
            if (this.transform.position.x < -4f)
            {
                this.transform.position = ButtonOrigin;

                CircleObject.transform.position = CircleOrigin;
                CircleObject.transform.rotation = CircleRotation;

                TriangleObject.transform.position = TriangleOrigin;
                TriangleObject.transform.rotation = TriangleRotation;

                SquareObject.transform.position = SquareOrigin;
                SquareObject.transform.rotation = SquareRotation;

                PentagonObject.transform.position = PentagonOrigin;
                PentagonObject.transform.rotation = PentagonRotation;

                HexagonObject.transform.position = HexagonOrigin;
                HexagonObject.transform.rotation = HexagonRotation;

                ResetCoolDown = 3f;
                NumberOfResets += 1;
            }
        }
    }
}
