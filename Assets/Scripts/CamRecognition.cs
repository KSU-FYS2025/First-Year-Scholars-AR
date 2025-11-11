using UnityEngine;
using UnityEngine.Splines;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.UI;

public class CamRecognition : MonoBehaviour
{
    public Transform[] waypoints;

    public SplineContainer splineContainer;

    [Tooltip("Capture a snapshot every _ second")]
    public float captureInterval;
    public bool capturing = true;
    private CinemachineSplineDolly splineDolly;

    [Header("Capture snapshot")]
    public Camera cam;
    public RawImage rawImage;

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
        splineDolly = GetComponent<CinemachineSplineDolly>();

        StartCoroutine(Capture());

        RenderTexture rt = new RenderTexture(1920, 1080, 24);
        cam.targetTexture = rt;
        rawImage.texture = rt;
    }

    void Update()
    {
        //splineDolly.CameraPosition
    }

    private IEnumerator Capture()
    {
        while (capturing)
        {
            yield return new WaitForSeconds(captureInterval);
            Debug.Log("Captured");
        }
    }
}
