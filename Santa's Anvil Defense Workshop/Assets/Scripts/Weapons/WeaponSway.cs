using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway settings")]
    public float swayMultiplier;
    public float smooth;

     // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * swayMultiplier;
        float MouseY = Input.GetAxis("Mouse Y") * swayMultiplier;

        Quaternion rotX = Quaternion.AngleAxis(-MouseY, Vector3.right);
        Quaternion rotY = Quaternion.AngleAxis(MouseX, Vector3.up);

        Quaternion targetRot = rotX * rotY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation , targetRot, smooth * Time.deltaTime);
    }
}
