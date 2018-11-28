using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsFeedforward : Feedforward {
    public ColorsFeedforward(FeedforwardController controller) : base(controller) { }

    private static Color[] colors = {
        Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow,
        new Color(1.0f, 0.5f, 0.0f), new Color(0.5f, 0.0f, 1.0f)
    };

    public override void Enable() {
        base.Enable();

        int i = 0;

        foreach (var circuit in controller.circuits) {
            circuit.SetColor(colors[i]);
            circuit.SetMapColor(colors[i++]);
        }
    }
}
