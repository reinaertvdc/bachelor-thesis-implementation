using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalosFeedforward : IndicatorFeedforward {
    public HalosFeedforward(FeedforwardController controller) : base(controller) { }

    protected override Indicator createIndicator(Camera viewport, GameObject target) {
        return new Indicator.Halo(viewport, target);
    }
}
