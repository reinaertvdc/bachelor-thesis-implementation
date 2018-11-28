using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IndicatorFeedforward : Feedforward {
    private readonly HashSet<Indicator> indicators = new HashSet<Indicator>();

    public IndicatorFeedforward(FeedforwardController controller) : base(controller) { }

    public override void Enable() {
        base.Enable();

        foreach (var indicator in indicators) {
            indicator.IsVisible = true;
        }
    }

    public override void Disable() {
        base.Disable();

        foreach (var indicator in indicators) {
            indicator.IsVisible = false;
            indicator.Destroy();
        }

        indicators.Clear();
    }

    public override void Update() {
        if (enabled) {
            foreach (var indicator in indicators) {
                indicator.Update();
            }
        }
    }

    public override void SetSelected(CircuitController circuit, CircuitComponentController component) {
        base.SetSelected(circuit, component);

        foreach (var indicator in indicators) {
            indicator.IsVisible = false;
            indicator.Destroy();
        }

        indicators.Clear();

        if (selectedCircuit != null) {
            foreach (var device in selectedCircuit.devices) {
                indicators.Add(createIndicator(viewport, device.gameObject));
            }
        }

        Enable();
    }

    protected abstract Indicator createIndicator(Camera viewport, GameObject target);
}
