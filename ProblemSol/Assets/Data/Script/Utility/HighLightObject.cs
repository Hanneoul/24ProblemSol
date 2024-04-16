using UnityEngine;

public class HighLightObject : MonoBehaviour
{
    public Material highlightMaterial; // 하이라이트할 머티리얼
    private Camera mainCamera; // 메인 카메라

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 메인 카메라의 프러스텀을 가져옴
        FrustumPlanes frustum = new FrustumPlanes(mainCamera);

        // 모든 씬에 있는 모든 MeshRenderer를 가져옴
        MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            // MeshRenderer가 카메라 프러스텀 내에 있는지 확인
            if (frustum.IsInsideFrustum(renderer.bounds))
            {
                // 하이라이트할 머티리얼 적용
                renderer.material = highlightMaterial;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            // 메인 카메라의 프러스텀을 가져와서 시각적으로 표시
            FrustumPlanes frustum = new FrustumPlanes(mainCamera);
            frustum.DrawFrustum(Color.yellow);
        }
    }
}

// 프러스텀 플레인 클래스
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

    // 에디터에서 프러스텀을 시각적으로 표시하기 위한 함수
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

// 카메라 스크립트의 확장 메서드
public static class CameraExtensions
{
    // 카메라 공간 플레인을 반환하기 위한 확장 메서드
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
