using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour {
    private AlarmButtonController[] displayPanel;
    private AlarmButtonController[] numberPanel;
    private AlarmDisplayController display;
    private Transform background;

    private enum State { Start, Logon, Menu };

    public void Start () {
        Transform contents = transform.GetChild(0);

        LoadPanel(contents.GetChild(0), ref displayPanel);
        LoadPanel(contents.GetChild(1), ref numberPanel);

        display = contents.GetChild(2).GetComponent<AlarmDisplayController>();
        background = contents.GetChild(3);

        background.GetComponent<MeshRenderer>().enabled = false;

        SetState(State.Start);
    }

    private void LoadPanel(Transform panel, ref AlarmButtonController[] target) {
        target = new AlarmButtonController[panel.childCount];

        for (int i = 0; i < panel.childCount; i++) {
            target[i] = panel.GetChild(i).GetComponent<AlarmButtonController>();
        }
    }

    private void SetState(State state) {
        Reset();

        switch (state) {
            case State.Start:
                displayPanel[0].SetState(AlarmButtonController.State.Continue);
                displayPanel[0].SetOnClick(delegate() { SetState(State.Logon); });
                break;
            case State.Logon:
                displayPanel[0].SetState(AlarmButtonController.State.Cancel);
                displayPanel[3].SetState(AlarmButtonController.State.Continue);
                foreach (var button in numberPanel) { button.SetState(AlarmButtonController.State.Enabled); }
                displayPanel[0].SetOnClick(delegate () { SetState(State.Start); });
                displayPanel[3].SetOnClick(delegate () { SetState(State.Menu); });
                break;
            case State.Menu:
                displayPanel[0].SetState(AlarmButtonController.State.Cancel);
                displayPanel[1].SetState(AlarmButtonController.State.Enabled);
                displayPanel[2].SetState(AlarmButtonController.State.Enabled);
                displayPanel[0].SetOnClick(delegate () { SetState(State.Start); });
                break;
        }
    }

    private void Reset() {
        foreach (AlarmButtonController button in displayPanel) { button.SetState(AlarmButtonController.State.Disabled); }
        foreach (AlarmButtonController button in numberPanel) { button.SetState(AlarmButtonController.State.Disabled); }
    }
}
