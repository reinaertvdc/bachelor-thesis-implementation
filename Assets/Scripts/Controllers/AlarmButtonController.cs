using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class AlarmButtonController : MonoBehaviour, IInputClickHandler {
    public enum State { Disabled, Enabled, Continue, Cancel };
    public delegate void OnClick();

    private State state;
    private OnClick onClick;

    public void Start() {
        SetState(State.Disabled);
    }

    public void SetState(State state) {
        Color? color = null;

        switch (state) {
            case State.Disabled: SetOnClick(null); break;
            case State.Enabled: color = Color.gray; break;
            case State.Continue: color = Color.green; break;
            case State.Cancel: color = Color.red; break;
        }

        if (color != null) {
            SetColor((Color)color);
        } else {
            SetInvisible();
        }

        this.state = state;
    }

    public void SetOnClick(OnClick onClick) {
        this.onClick = onClick;
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        if (onClick != null) { onClick(); }
    }

    protected void SetInvisible() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    protected void SetColor(Color color) {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }
}
