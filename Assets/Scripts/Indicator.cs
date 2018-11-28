using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Indicator {
    private static readonly Vector3 VIEWPORT_CENTER = new Vector3(0.5f, 0.5f, 0);
    private static readonly Vector2 VIEWPORT_RANGE = new Vector2(0.5f, 0.5f);
    private static readonly Vector2 VIEWING_RANGE = new Vector2(0.4f, 0.4f);
    private static float MIN_INDICATOR_SIZE = 0.08f;
    private static readonly float DRAWING_DEPTH = 1;

    public GameObject target;

    private readonly Camera viewport;
    private readonly LineRenderer lineRenderer;
    private readonly int numPoints;
    private bool isVisible;

    private static float WithinRange(float value, float min, float max) {
        return Mathf.Min(Mathf.Max(value, min), max);
    }

    public Indicator(Camera viewport, GameObject target, int numPoints) {
        this.viewport = viewport;
        this.target = target;
        this.numPoints = numPoints;

        lineRenderer = new GameObject().AddComponent<LineRenderer>();
        lineRenderer.loop = true;
        DrawingMaterial = new Material(Shader.Find("Sprites/Default"));
        DrawingLineWidth = 0.004f;
        DrawingColor = Color.red;

        IsVisible = false;
    }

    public Material DrawingMaterial {
        get { return lineRenderer.material; }
        set { lineRenderer.material = value; }
    }

    public float DrawingLineWidth {
        get { return lineRenderer.startWidth; }
        set { lineRenderer.startWidth = lineRenderer.endWidth = value; }
    }

    public Color DrawingColor {
        get { return lineRenderer.startColor; }
        set { lineRenderer.startColor = lineRenderer.endColor = value; }
    }

    public bool IsVisible {
        get { return isVisible; }

        set {
            if (value) {
                lineRenderer.positionCount = numPoints;
            } else {
                lineRenderer.positionCount = 0;
            }

            isVisible = value;
        }
    }

    public void Update() {
        if (IsVisible) {
            Vector3 targetPos = WorldToViewport(target.transform.position);

            Vector2 to = ((targetPos.z > 0) ? targetPos : -targetPos);
            Vector2 from = new Vector2(WithinRange(to.x, -VIEWING_RANGE.x, VIEWING_RANGE.x), WithinRange(to.y, -VIEWING_RANGE.y, VIEWING_RANGE.y));

            if (Mathf.Abs(to.x) <= VIEWPORT_RANGE.x && Mathf.Abs(to.y) <= VIEWPORT_RANGE.y) {
                if (targetPos.z >= 0) {
                    from.y = to.y + MIN_INDICATOR_SIZE;
                } else {
                    to *= to.magnitude + 2 / to.magnitude;

                    from = new Vector2(WithinRange(to.x, -VIEWING_RANGE.x, VIEWING_RANGE.x), WithinRange(to.y, -VIEWING_RANGE.y, VIEWING_RANGE.y));
                }

                Render(from, to);
            } else {
                Vector2 difference = to - from;

                if (targetPos.z >= 0) {
                    difference *= 1.2f / (difference.magnitude + 1);
                } else {
                    difference = difference.normalized + difference.normalized * (1.2f / (difference.magnitude + 1));
                }

                Render(from, from + difference);
            }
        }
    }

    public void Destroy() {
        GameObject.Destroy(lineRenderer.gameObject);
    }

    protected abstract void Render(Vector2 from, Vector2 to);

    private void SetPosition(int index, Vector2 position) {
        Vector3 position3d = new Vector3(position.x, position.y, DRAWING_DEPTH);

        lineRenderer.SetPosition(index, ViewportToWorld(position3d));
    }

    private Vector3 WorldToViewport(Vector3 position) {
        return viewport.WorldToViewportPoint(position) - VIEWPORT_CENTER;
    }

    private Vector3 ViewportToWorld(Vector3 position) {
        return viewport.ViewportToWorldPoint(position + VIEWPORT_CENTER);
    }

    public class Halo : Indicator {
        private readonly float lineLength;

        public Halo(Camera viewport, GameObject target) : base(viewport, target, 128) {
            lineLength = Mathf.PI * 2 / numPoints;
        }

        protected override void Render(Vector2 from, Vector2 to) {
            Vector2 center = to;
            Vector2 difference = to - from;
            difference.y /= viewport.aspect;
            float radius = Vector2.Distance(Vector2.zero, difference);

            float value = 0;

            for (int i = 0; i < numPoints; i++) {
                Vector2 position;
                position.x = center.x + (radius * Mathf.Cos(value));
                position.y = center.y + (radius * Mathf.Sin(value) * viewport.aspect);

                SetPosition(i, position);

                value += lineLength;
            }
        }
    }

    public class Arrow : Indicator {
        public Arrow(Camera viewport, GameObject target) : base(viewport, target, 3) { }

        protected override void Render(Vector2 from, Vector2 to) {
            Vector2 difference = to - from;

            SetPosition(0, to);
            SetPosition(1, from + new Vector2(-difference.y, difference.x) / Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2)) * 0.02f);
            SetPosition(2, from + new Vector2(-difference.y, difference.x) / Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2)) * -0.02f);
        }
    }
}
