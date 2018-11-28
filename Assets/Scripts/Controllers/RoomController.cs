using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
    private Transform walls;

    private void Start() {
        walls = transform.GetChild(0);
    }

    public void SetVisible(bool state) {
        for (int i = 0; i < walls.childCount; i++) {
            walls.GetChild(i).GetComponent<MeshRenderer>().enabled = state;
            walls.GetChild(i).GetChild(1).GetComponent<MeshRenderer>().enabled = state;
        }
    }

    public void SetEnabled(bool state) {
        for (int i = 0; i < walls.childCount; i++) {
            walls.GetChild(i).gameObject.SetActive(state);
            walls.GetChild(i).GetChild(1).gameObject.SetActive(state);
        }
    }
}
