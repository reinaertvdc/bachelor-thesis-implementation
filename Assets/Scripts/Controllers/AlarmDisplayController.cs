using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmDisplayController : MonoBehaviour {
    Transform background;
    Transform text;
    Transform textTop;
    Transform[] textBottom = new Transform[4];
    Transform[] lines = new Transform[3];

	void Start () {
        background = transform.Find("Background");
        text = transform.Find("Text");
        textTop = text.Find("Top");

        for (int i = 0; i < textBottom.Length; i++) {
            textBottom[i] = text.Find("" + (i + 1));
        }

        for (int i = 0; i < lines.Length; i++) {
            lines[i] = text.Find("Line" + (i + 1));
        }

        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>()) {
            meshRenderer.enabled = false;
        }
    }
}
