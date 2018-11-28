using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedforward {
    protected readonly FeedforwardController controller;
    protected readonly Camera viewport;
    protected CircuitController selectedCircuit;
    protected bool enabled = false;

    public Feedforward(FeedforwardController controller) {
        this.controller = controller;
        viewport = controller.camera;
    }

    public virtual void Enable() {
        enabled = true;

        if (selectedCircuit != null) {
            selectedCircuit.HighlightPanels();
        }
    }

    public virtual void Disable() {
        enabled = false;

        foreach (var circuit in controller.circuits) {
            circuit.ResetColor();
            circuit.ResetMapColor();
        }
    }

    public virtual void SetSelected(CircuitController circuit, CircuitComponentController component) {
        if (selectedCircuit != null) {
            selectedCircuit.ResetColor();
            selectedCircuit.ResetMapColor();
        }

        if (circuit != null && circuit != selectedCircuit) {
            selectedCircuit = circuit;
            selectedCircuit.HighlightPanels();
        } else {
            selectedCircuit = null;
        }
    }

    public virtual void Update() { }
}
