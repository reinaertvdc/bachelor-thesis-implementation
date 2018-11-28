using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public abstract class CircuitComponentController : MonoBehaviour, IInputClickHandler {
    private CircuitController circuit;

    public void SetColor(Color color) {
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    public void SetMapColor(Color color) {
        foreach (Transform child in transform) {
            child.GetComponent<Renderer>().material.SetColor("_Color", color);
        }
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        circuit.SetSelected(this);
    }

    protected void Init() {
        circuit = GetComponentInParent<CircuitController>();
    }
}
