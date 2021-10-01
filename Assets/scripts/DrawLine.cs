using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
   // public EdgeCollider2D edgeCollier;
    public List<Vector3> fingerPosition;

    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = new Vector3(Input.mousePosition.x,0,cam.pixelHeight-Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {

            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (Vector2.Distance(tempFingerPos, fingerPosition[fingerPosition.Count - 1]) > 0.1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }
    void CreateLine()
    {
        


        Vector3 mousePos = new Vector3(Input.mousePosition.x, 0, cam.pixelHeight-Input.mousePosition.y);
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        //edgeCollier = currentLine.GetComponent<EdgeCollider2D>();
        fingerPosition.Clear();
        fingerPosition.Add(Camera.main.ScreenToWorldPoint(mousePos));
        fingerPosition.Add(Camera.main.ScreenToWorldPoint(mousePos));
        lineRenderer.SetPosition(0, fingerPosition[0]);
        lineRenderer.SetPosition(1, fingerPosition[1]);
        //edgeCollier.points = fingerPosition.ToArray();


    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPosition.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        //edgeCollier.points = fingerPosition.ToArray();
    }
}
