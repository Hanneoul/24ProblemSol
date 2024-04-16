using UnityEngine;

public class HighLightObject : MonoBehaviour
{
    public Material highlightMaterial; // ���̶���Ʈ�� ��Ƽ����
    private Camera mainCamera; // ���� ī�޶�

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // ���� ī�޶��� ���������� ������
        FrustumPlanes frustum = new FrustumPlanes(mainCamera);

        // ��� ���� �ִ� ��� MeshRenderer�� ������
        MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            // MeshRenderer�� ī�޶� �������� ���� �ִ��� Ȯ��
            if (frustum.IsInsideFrustum(renderer.bounds))
            {
                // ���̶���Ʈ�� ��Ƽ���� ����
                renderer.material = highlightMaterial;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            // ���� ī�޶��� ���������� �����ͼ� �ð������� ǥ��
            FrustumPlanes frustum = new FrustumPlanes(mainCamera);
            frustum.DrawFrustum(Color.yellow);
        }
    }
}

// �������� �÷��� Ŭ����
public class FrustumPlanes
{
    private readonly Plane[] planes;

    public FrustumPlanes(Camera camera)
    {
        planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public bool IsInsideFrustum(Bounds bounds)
    {
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

    // �����Ϳ��� ���������� �ð������� ǥ���ϱ� ���� �Լ�
    public void DrawFrustum(Color color)
    {
        Gizmos.color = color;

        Matrix4x4 matrix = Matrix4x4.identity;
        Gizmos.matrix = matrix;

        Vector3 cameraCenter = Camera.main.transform.position + Camera.main.transform.forward * Camera.main.farClipPlane * 0.5f;
        foreach (var plane in planes)
        {
            
            Gizmos.DrawFrustum(cameraCenter, Camera.main.fieldOfView, 1000f, 0.1f, 1.777f);
        }
    }
}

// ī�޶� ��ũ��Ʈ�� Ȯ�� �޼���
public static class CameraExtensions
{
    // ī�޶� ���� �÷����� ��ȯ�ϱ� ���� Ȯ�� �޼���
    public static Plane GetCameraSpacePlane(this Plane plane)
    {
        Matrix4x4 m = Camera.main.worldToCameraMatrix;
        Vector3 normal = -plane.normal;
        Vector3 position = plane.normal * plane.distance;
        normal = m.MultiplyVector(normal).normalized;
        position = m.MultiplyPoint(position);
        return new Plane(normal, position);
    }
}
