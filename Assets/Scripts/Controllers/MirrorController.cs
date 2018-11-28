using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MirrorController : MonoBehaviour {
    private Transform imageTarget;
    private bool isRendering = true;

    private float x = +3.35f;
    private float y = -0.23f;
    private float z = -1.08f;

	// Use this for initialization
	void Start () {
        imageTarget = transform.parent.Find("ImageTarget");
	}
	
	// Update is called once per frame
	void Update () {
        if (VuforiaBehaviour.Instance.enabled) {
            Vector3 p = imageTarget.position;

            if (p.x == 0 && p.y == 0 && p.z == 0) {
                if (isRendering) {
                    transform.GetChild(0).gameObject.SetActive(false);
                    isRendering = false;
                }

                return;
            }

            if (Input.GetKeyDown(KeyCode.F)) {
                x -= 0.01f;
                Debug.Log("" + x + "  " + y + "  " + z);
            }
            if (Input.GetKeyDown(KeyCode.G)) {
                x += 0.01f;
                Debug.Log("" + x + "  " + y + "  " + z);
            }
            if (Input.GetKeyDown(KeyCode.H)) {
                y -= 0.01f;
                Debug.Log("" + x + "  " + y + "  " + z);
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                y += 0.01f;
                Debug.Log("" + x + "  " + y + "  " + z);
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                z -= 0.01f;
                Debug.Log("" + x + "  " + y + "  " + z);
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                z += 0.01f;
                Debug.Log("" + x + "  " + y + "  " + z);
            }

            p.x += x;
            p.y += y;
            p.z += z;

            transform.SetPositionAndRotation(p, imageTarget.rotation);

            if (!isRendering) {
                transform.GetChild(0).gameObject.SetActive(true);
                isRendering = true;
            }
        }
	}
}
