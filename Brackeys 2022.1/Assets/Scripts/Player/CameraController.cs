using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    //In dit script wordt alleen de cinemachineCore input as waardes aangepast
    //dit gebeurt alleen wanneer de rechtermuisknopactie is ingedrukt
    Input input; //input script 
    private GameObject playerObj;
    private CinemachineFreeLook cam;
    [SerializeField]
    [Range(0,10)]
    private float idleRotateSpeed;
    [SerializeField]
    [Range(0,20)]
    private float delayTotIdleRotation;//hoelang de speler niet zelf de camera heeft bestuurd tot camera wordt gereset
    private float delay;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player"); //playerObject
        input = new Input();
        input.Player.Enable();
        cam = GetComponent<CinemachineFreeLook>();
        delay = Time.time + delayTotIdleRotation;
    }
    private void Update()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        Vector2 vector2 = input.Player.Look.ReadValue<Vector2>();
        if (input.Player.RightClick.ReadValue<float>() == 1) //als ingedrukt
        {
            delay = Time.time + delayTotIdleRotation;
            if (axisName == "Mouse X") //als X
            {
                return vector2.x;
            }
            else if (axisName == "Mouse Y") //als Y
            {
                return vector2.y;
            }
        }
        else //als geen knop wordt ingedrukt resetten naar player rotation
        {
            //x positie resetten
            if (axisName == "Mouse X") 
            {
                if (Time.time > delay)
                {
                    //automatisch naar player rotatie rotaten gaat nog niet helemaal soepel .
                    //waardes:
                    //Debug.Log(playerObj.transform.rotation.y);// van -1 tot +1
                    //Debug.Log(cam.m_XAxis.Value/180);// van -1 tot +1
                    //Debug.Log(playerObj.transform.rotation.y - (cam.m_XAxis.Value/180));
                    if (playerObj.transform.rotation.y - (cam.m_XAxis.Value / 180) < -0.1)
                    {
                        return idleRotateSpeed;
                    }
                    else if (playerObj.transform.rotation.y - (cam.m_XAxis.Value / 180) > 0.1)
                    {
                        return -idleRotateSpeed;
                    }
                    else
                    {
                        delay = Time.time + delayTotIdleRotation;
                    }
                }
            }
        }
        return 0;
    }
}
