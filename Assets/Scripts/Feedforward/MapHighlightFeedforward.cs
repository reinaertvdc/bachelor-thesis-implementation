﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHighlightFeedforward : Feedforward {
    public MapHighlightFeedforward(FeedforwardController controller) : base(controller) { }

    public override void SetSelected(CircuitController circuit, CircuitComponentController component) {
        base.SetSelected(circuit, component);

        if (enabled) {
            if (selectedCircuit != null) { selectedCircuit.MapHighlight(); }
        }
    }
}