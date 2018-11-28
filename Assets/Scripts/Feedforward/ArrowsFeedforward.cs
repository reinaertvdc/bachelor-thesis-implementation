using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsFeedforward : IndicatorFeedforward {
    public ArrowsFeedforward(FeedforwardController controller) : base(controller) { }

    protected override Indicator createIndicator(Camera viewport, GameObject target) {
        return new Indicator.Arrow(viewport, target);
    }
}
