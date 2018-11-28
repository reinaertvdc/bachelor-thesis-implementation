using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitController : MonoBehaviour {
    public FeedforwardController feedforwardController;

    public readonly HashSet<DeviceController> devices = new HashSet<DeviceController>();
    public readonly HashSet<PanelController> panels = new HashSet<PanelController>();

    public void SetColor(Color color) {
        foreach (var device in GetComponentsInChildren<DeviceController>()) { device.SetColor(color); }
        foreach (var panel in GetComponentsInChildren<PanelController>()) { panel.SetColor(color); }
    }

    public void SetMapColor(Color color) {
        foreach (var device in GetComponentsInChildren<DeviceController>()) { device.SetMapColor(color); }
        foreach (var panel in GetComponentsInChildren<PanelController>()) { panel.SetMapColor(color); }
    }

    public void ResetColor() {
        SetColor(Color.white);
    }

    public void ResetMapColor() {
        SetMapColor(Color.white);
    }

    public void Highlight() {
        SetColor(Color.red);
    }

    public void MapHighlight() {
        SetMapColor(Color.red);
    }

    public void HighlightPanels() {
        foreach (var panel in GetComponentsInChildren<PanelController>()) { panel.SetColor(Color.red); panel.SetMapColor(Color.red); }
    }

    public void SetSelected(CircuitComponentController component) {
        feedforwardController.SetSelected(this, component);
    }

    private void Start() {
        foreach (var device in GetComponentsInChildren<DeviceController>()) { devices.Add(device); }
        foreach (var panel in GetComponentsInChildren<PanelController>()) { panels.Add(panel); }
    }
}
