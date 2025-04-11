using System.Collections.Generic;
using UnityEngine;

public class DrawOnPlane : MonoBehaviour
{
    public LineRenderer linePrefab; // Assign a LineRenderer prefab in Inspector
    private LineRenderer currentLine;
    private List<Vector3> points = new List<Vector3>();
    public LayerMask planeLayer; // Assign the layer of your plane

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartNewLine();
        }

        if (Input.GetMouseButton(0) && currentLine != null)
        {
            DrawLine();
        }
    }

    void StartNewLine()
    {
        currentLine = Instantiate(linePrefab);
        points.Clear();
    }

    void DrawLine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planeLayer))
        {
            Vector3 newPoint = hit.point;
            if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], newPoint) > 0.01f)
            {
                points.Add(newPoint);
                currentLine.positionCount = points.Count;
                currentLine.SetPositions(points.ToArray());
            }
        }
    }
}
