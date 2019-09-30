using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform Player;
    public float cameraDistacia = 30.0f;

    public bool limite;

    public Vector3 minCamera;
    public Vector3 maxCamera;

    
    public

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistacia);
    }

    void Update()
    {
        transform.position = new Vector3(Player.position.x + 5, Player.position.y, transform.position.z);

        if (limite)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamera.x, maxCamera.x),
                Mathf.Clamp(transform.position.y, minCamera.y, maxCamera.y),
                Mathf.Clamp(transform.position.z, minCamera.z, maxCamera.z));
        }
    }
}
