using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class FeedforwardController : MonoBehaviour {
    public readonly HashSet<CircuitController> circuits = new HashSet<CircuitController>();
    public Camera camera;

    private readonly List<string> typeNames = new List<string>();
    private readonly List<Feedforward> types = new List<Feedforward>();
    private readonly List<bool> typesActive = new List<bool>();

    private Canvas map;

    private CircuitController selectedCircuit;
    private CircuitComponentController selectedComponent;

    public void SetSelected(CircuitController circuit, CircuitComponentController component) {
        selectedCircuit = circuit;
        selectedComponent = component;

        foreach (var type in types) {
            type.SetSelected(circuit, component);
        }
    }

    public void StartTracking() {
        VuforiaBehaviour.Instance.enabled = true;
    }

    public void StopTracking() {
        VuforiaBehaviour.Instance.enabled = false;
    }

    public void ClearSelected() {
        foreach (var type in types) {
            type.SetSelected(null, null);
        }
    }

    public void ShowMap() {
        map.gameObject.SetActive(true);
    }

    public void HideMap() {
        map.gameObject.SetActive(false);
    }

    private void SetType(string type, bool state) {
        for (int i = 0; i < types.Count; i++) {
            if (typeNames.ElementAt(i) == type) {
                typesActive[i] = state;

                if (state) {
                    types.ElementAt(i).Enable();
                } else {
                    types.ElementAt(i).Disable();
                }
            }
        }
    }

    private void Start() {
        foreach (var circuit in (CircuitController[])FindObjectsOfType(typeof(CircuitController))) { circuits.Add(circuit); circuit.feedforwardController = this; }

        map = (Canvas)FindObjectOfType(typeof(Canvas));
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        typeNames.AddRange(new List<string> { "highlight", "maphighlight", "arrows", "halos", "colors" });
        types.AddRange(new List<Feedforward> { new HighlightFeedforward(this), new MapHighlightFeedforward(this), new ArrowsFeedforward(this), new HalosFeedforward(this), new ColorsFeedforward(this) });

        for (int i = 0; i < types.Count; i++) {
            typesActive.Add(false);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            foreach (var typeName in typeNames) { SetType(typeName, false); }
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            SetType("highlight", true);
        } if (Input.GetKeyDown(KeyCode.B)) {
            SetType("maphighlight", true);
        } if (Input.GetKeyDown(KeyCode.C)) {
            SetType("arrows", true);
        } if (Input.GetKeyDown(KeyCode.D)) {
            SetType("halos", true);
        } if (Input.GetKeyDown(KeyCode.E)) {
            SetType("colors", true);
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            HideMap();
        } if (Input.GetKeyDown(KeyCode.X)) {
            ShowMap();
        } if (Input.GetKeyDown(KeyCode.Q)) {
            foreach (var room in FindObjectsOfType<RoomController>()) { room.SetVisible(true); }
        } if (Input.GetKeyDown(KeyCode.S)) {
            foreach (var room in FindObjectsOfType<RoomController>()) { room.SetVisible(true); }
        } if (Input.GetKeyDown(KeyCode.V)) {
            ClearSelected();
        } if (Input.GetKeyDown(KeyCode.O)) {
            StartTracking();
        } if (Input.GetKeyDown(KeyCode.P)) {
            StopTracking();
        }

        for (int i = 0; i < types.Count; i++) {
            if (typesActive.ElementAt(i)) {
                types.ElementAt(i).Update();
            }
        }
    }
}
