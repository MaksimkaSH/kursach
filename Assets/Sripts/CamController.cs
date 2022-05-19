using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Camera-Control/CamController")]
public class CamController : MonoBehaviour
{
    public enum RotationAxes { MouseXandY, MouseX, MouseY };
    public RotationAxes axes = RotationAxes.MouseXandY;
    //чувствительность
    public float sensitivityX=2;
    public float sensitivityY=1;
    //максимальный угол вращения
    public float minX = -360;
    public float maxX = 360;
    public float minY = -360;
    public float maxY = 360;

    //текущий угол вращения
    float rotX = 0;
    float rotY = 0;

    //основной тип врещения
    Quaternion originalRot;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            
        }
        originalRot = transform.localRotation;

    }

    public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360) angle += -360F;
        if (angle > 360) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseXandY)
        {
            rotX += Input.GetAxis("Mouse X") * sensitivityX;
            rotY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotX = ClampAngle(rotX, minX, maxX);
            rotY = ClampAngle(rotY, minY, maxY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotY, -Vector3.right);
            transform.localRotation = originalRot * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotX += Input.GetAxis("Mouse X") * sensitivityX;
            rotX = ClampAngle(rotX, minX, maxX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotX, Vector3.up);
            transform.localRotation = originalRot * xQuaternion;
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotY = ClampAngle(rotY, minY, maxY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotY, Vector3.right);
            transform.localRotation = originalRot * yQuaternion;
        }
    }
}
