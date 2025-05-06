using System.Collections.Generic;
using UnityEngine;

public class DrawOnPlane : MonoBehaviour
{
    public LineRenderer linePrefab;
    private LineRenderer currentLine;
    private List<Vector3> points = new List<Vector3>();
    public LayerMask planeLayer;

    // 🆕 Keep track of all drawn lines
    private List<GameObject> drawnLines = new List<GameObject>();

    void Update()
    {
        if (!Cursor.visible) return;

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
        drawnLines.Add(currentLine.gameObject); // 🆕 Track this line
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

    // 🧹 Call this from CursorToggle when hiding the cursor
    public void ClearAllLines()
    {
        foreach (GameObject line in drawnLines)
        {
            if (line != null)
                Destroy(line);
        }

        drawnLines.Clear();
        currentLine = null;
        points.Clear();
    }
}