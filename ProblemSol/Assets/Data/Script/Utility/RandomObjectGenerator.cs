using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomObjectGenerator : MonoBehaviour
{
    public GameObject TargetObject;
    public int ObjectNumber = 0;

#if UNITY_EDITOR
    [CustomEditor(typeof(RandomObjectGenerator))]
    public class RandomObjectGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RandomObjectGenerator generator = (RandomObjectGenerator)target;
            if (GUILayout.Button("Generate Objects"))
            {
                generator.GenerateObjects();
            }
        }
    }
#endif

    public void GenerateObjects()
    {
        // 이 곳에 Object를 생성하고 배치하는 코드를 작성하세요.
        for (int i = 0; i < ObjectNumber; i++)
        {
            // 랜덤한 위치를 생성합니다.
            Vector3 randomPosition = new Vector3(
                Random.Range(transform.position.x - transform.localScale.x * 0.5f, transform.position.x + transform.localScale.x * 0.5f),
                Random.Range(transform.position.y - transform.localScale.y * 0.5f, transform.position.y + transform.localScale.y * 0.5f),
                Random.Range(transform.position.z - transform.localScale.z * 0.5f, transform.position.z + transform.localScale.z * 0.5f)
            );

            // TargetObject를 생성하고 랜덤한 위치에 배치합니다.
            GameObject newObject = Instantiate(TargetObject, randomPosition, Quaternion.identity);
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Box 형태의 가이드라인을 그립니다.
        Handles.color = Color.yellow;

        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        
        using (new Handles.DrawingScope(cubeTransform))
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
#endif
}
