using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TorchVision : MonoBehaviour
{
    [SerializeField] private float viewRadius = 6f;

    [SerializeField] private int rayCount = 200;

    [SerializeField] private LayerMask obstacleMask;

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        DrawVision();
    }

    void DrawVision()
    {
        float angleStep = 360f / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2];

        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = i * angleStep;

            Vector3 dir = DirFromAngle(angle);

            RaycastHit2D hit =
                Physics2D.Raycast(
                    transform.position,
                    dir,
                    viewRadius,
                    obstacleMask);

            Vector3 vertex;

            if (hit.collider != null)
            {
                vertex = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertex = dir * viewRadius;
            }

            vertices[i + 1] = vertex;

            if (i < rayCount)
            {
                int triIndex = i * 3;

                triangles[triIndex] = 0;
                triangles[triIndex + 1] = i + 1;
                triangles[triIndex + 2] = i + 2;
            }
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    Vector3 DirFromAngle(float angle)
    {
        float rad =
            angle * Mathf.Deg2Rad;

        return new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad));
    }
}
