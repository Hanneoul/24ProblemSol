using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1; // 프러스텀 내부에 있는 오브젝트에 적용될 머티리얼
    public Material material2; // 프러스텀 밖에 있는 오브젝트에 적용될 머티리얼

    private Camera thisCamera;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (thisCamera == null)
        {
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // 카메라의 프러스텀을 가져오기
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

        // 모든 씬에 있는 모든 Renderer 가져오기
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Vector3 size = renderer.bounds.size;
            float diameter = Mathf.Max(size.x, Mathf.Max(size.y, size.z));

            // Renderer의 중심점이 프러스텀 내에 있는지 확인
            if (frustum.IsInsideFrustum(renderer.bounds.center, diameter/2.0f))
            {
                // 프러스텀 내에 있는 경우 Material1 적용
                renderer.material = material1;
            }
            else
            {
                // 프러스텀 밖에 있는 경우 Material2 적용
                renderer.material = material2;
            }
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

    public bool IsInsideFrustum(Vector3 point, float sideSize)
    {
        foreach (var plane in planes)
        {
            if (!plane.GetSide(point))
            {
                return false;
            }            
        }
        return true;
    }
}