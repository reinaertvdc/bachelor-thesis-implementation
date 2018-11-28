using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour {
    private GameObject mainCamera;

    // Use this for initialization
    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update() {
        Vector3 newPos = mainCamera.transform.position;
        Vector3 oldPos = transform.position;
        Vector3 newRot = mainCamera.transform.eulerAngles;
        Vector3 oldRot = transform.eulerAngles;

        transform.position = new Vector3(newPos.x, oldPos.y, newPos.z);
        transform.eulerAngles = new Vector3(oldRot.x, newRot.y, oldRot.z);
    }
}
