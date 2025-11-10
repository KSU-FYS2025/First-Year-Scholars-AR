using UnityEngine;
using UnityEngine.Splines;

public class CamRecognition : MonoBehaviour
{
    public Transform[] waypoints;

    public SplineContainer splineContainer;

    [Tooltip("Capture a snapshot every _ Unity meter traveled")]
    public float captureInterval;
    private float totalDistance = 0f;
    private bool captured = false;

    private void Awake()
    {
        Spline spline = splineContainer.Splines[0];

        for (int i = 0; i < waypoints.Length; i++)
        {
            BezierKnot newKnot = new BezierKnot(waypoints[i].position);

            spline.Add(newKnot);
        }

        spline.Closed = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!captured)
        {

        }
        else if (captured)
        {

        }

    }
}
