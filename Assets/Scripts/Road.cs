using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Road : MonoBehaviour {

    [SerializeField]
    private Material material;

    private MeshFilter meshFilter;

    [SerializeField]
    public List<Transform> roadPoints;

	void Start () {
        GenerateMesh();
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
	}
    

    void OnDrawGizmos()
    {
        if (roadPoints != null)
        {
            Transform lastT = null;
            foreach (Transform t in roadPoints)
            {
                Gizmos.color = Color.cyan;
                if (lastT != null) Gizmos.DrawLine(lastT.position, t.position);
                Gizmos.DrawRay(t.position, t.right * 50);
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(t.position, 3);
                lastT = t;
            }
        }
    }

    private void GenerateMesh()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = new Mesh();

        var shapeVertices = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(5, 0.5f),
                new Vector2(5, 0.5f),
                new Vector2(25, 0.7f),
                new Vector2(45, 0.5f),
                new Vector2(45, 0.5f),
                new Vector2(50, 0)
            };

        var points = new Point[roadPoints.Count];
        // TODO: Vector3 extension ToPositionArray
        Vector3[] samplePoints = new Vector3[roadPoints.Count];
        for (int i = 0; i < samplePoints.Length; ++i) {
            samplePoints[i] = roadPoints[i].transform.position;
        }

        float[] samples = Spline.GenerateSamples(samplePoints);

        for (int i = 0; i < roadPoints.Count; ++i)
        {
            points[i] = new Point(
                roadPoints[i].position,
                roadPoints[i].rotation,
                samples.Sample((float)i / roadPoints.Count) / 10);
        }

        Spline.Extrude(
            meshFilter.sharedMesh,
            new Shape()
            {
                vertices = shapeVertices,
                normals = new Vector2[] {
                    Vector2.up,
                    Vector2.up,
                    Vector2.up,
                    Vector2.up,
                    Vector2.up,
                    Vector2.up,
                    Vector2.up
                },
                lines = new int[]
                {
                    0, 1,
                    1, 2,
                    2, 3,
                    3, 4,
                    4, 5,
                    5, 6
                },
                uValues = new float[]
                {
                    0, 0.1f, 0.1f, 0.5f, 0.9f, 0.9f, 1
                }
            },
            points
            );
    }
}
