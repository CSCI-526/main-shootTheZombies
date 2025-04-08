using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BulletGuideLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject arrowPrefab;
    public Transform bulletStart;
    public float reflectLength = 500f;
    public LayerMask wallLayer;
    public float maxTime = 5f;
    public Color lineColor = Color.green;
    private List<GameObject> arrowInstances = new List<GameObject>();
    private float timer = 0f;
    private Coroutine arrowAnimCoroutine; 

    void Start()
    {
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        arrowAnimCoroutine = StartCoroutine(ArrowAnimation());
    }

    void Update()
    {
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
            DrawGuidedLine();
        }
        else
        {
            StopDrawing();
        }
    }

    void DrawGuidedLine()
    {
        Vector3 start = bulletStart.position;
        Vector3 mouseWorld = GetMouseWorldPosition();
        Vector3 dir = (mouseWorld - start).normalized;

        List<Vector3> points = new List<Vector3>();
        points.Add(start);

        RaycastHit2D hit = Physics2D.Raycast(start, dir, reflectLength, LayerMask.GetMask("Wall"));
        if (hit.collider != null && (hit.collider.name == "LeftWall" || hit.collider.name == "RightWall"))
        {
            Vector3 hitPoint = hit.point;
            points.Add(hitPoint);

            Vector3 normal = hit.normal;
            dir = Vector3.Reflect(dir, normal);
            start = hitPoint;
            // Debug.Log("hitPoint " + hitPoint);
            Vector3 endPoint = start;
            float step = 0.1f;
            for (int i = 0; i < 1000; i++) 
            {
                endPoint += dir * step;
                if (endPoint.y > 10f || Mathf.Abs(endPoint.x) > 7.5f)
                {
                    points.Add(endPoint);
                    // Debug.Log("endPoint " + endPoint);
                    break;
                }
            }
        }
        else
        {
            Vector3 endPoint = start;
            float step = 0.1f;
            for (int i = 0; i < 1000; i++) 
            {
                endPoint += dir * step;
                if (endPoint.y > 10f)
                {
                    points.Add(endPoint);
                    // Debug.Log("endPoint " + endPoint);
                    break;
                }
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());

        SpawnArrows(points);
    }

    void SpawnArrows(List<Vector3> path)
    {
        foreach (var arrow in arrowInstances)
        {
            Destroy(arrow);
        }
        arrowInstances.Clear();

        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 from = path[i];
            Vector3 to = path[i + 1];
            float segmentLength = Vector3.Distance(from, to);
            int arrowCount = Mathf.FloorToInt(segmentLength / 1.5f);

            for (int j = 0; j < arrowCount; j++)
            {
                float t = (float)j / arrowCount;
                Vector3 pos = Vector3.Lerp(from, to, t);
                Quaternion rot = Quaternion.LookRotation(Vector3.forward, (to - from).normalized);
                GameObject arrow = Instantiate(arrowPrefab, pos, rot);
                arrow.transform.SetParent(transform);
                arrowInstances.Add(arrow);
            }
        }
    }

    IEnumerator ArrowAnimation()
    {
        while (timer < maxTime)
        {
            foreach (var arrow in arrowInstances)
            {
                arrow.transform.localScale = Vector3.one * (3.0f + Mathf.PingPong(Time.time * 2f, 1.0f));
            }
            yield return null;
        }
    }

    void StopDrawing()
    {
        lineRenderer.positionCount = 0;
        foreach (var arrow in arrowInstances)
        {
            Destroy(arrow);
        }
        arrowInstances.Clear();

        if (arrowAnimCoroutine != null)
        {
            StopCoroutine(arrowAnimCoroutine);
            arrowAnimCoroutine = null;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}