using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1; // �������� ���ο� �ִ� ������Ʈ�� ����� ��Ƽ����
    public Material material2; // �������� �ۿ� �ִ� ������Ʈ�� ����� ��Ƽ����

    private Camera thisCamera;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (thisCamera == null)
        {
            Debug.LogError("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ī�޶��� ���������� ��������
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

        // ��� ���� �ִ� ��� Renderer ��������
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            
            // Renderer�� �߽����� �������� ���� �ִ��� Ȯ��
            if (frustum.IsInsideFrustum(renderer.bounds.center))
            {
                // �������� ���� �ִ� ��� Material1 ����
                renderer.material = material1;
            }
            else
            {
                // �������� �ۿ� �ִ� ��� Material2 ����
                renderer.material = material2;
            }
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

    public bool IsInsideFrustum(Vector3 point)
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