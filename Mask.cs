
/*u
 sing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    // 자식으로 설정할 오브젝트
    public GameObject targetObject;

    // 새로운 부모 오브젝트
    public Transform newParent;

    // 사용할 키
    public KeyCode keyToPress = KeyCode.E;

    // 이동할 위치
    public Vector3 newPosition = new Vector3(0.214f, -0.002f, -0.001f); // 예시 위치 (0, 1, 0)

    // 회전 값
    public Vector3 newRotation = new Vector3(-89.96f, 0, 90.046f); // 예시 회전 (0, 90, 0)

    // 크기 값
    public Vector3 newScale = new Vector3(1.091504f, 0.8167786f, 1.132167f); // 예시 크기 (1, 1, 1)

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (targetObject != null && newParent != null)
            {
                // 오브젝트의 부모를 변경합니다.
                targetObject.transform.SetParent(newParent);

                // 오브젝트의 위치를 변경합니다.
                targetObject.transform.localPosition = newPosition;

                // 오브젝트의 회전을 변경합니다.
                targetObject.transform.localRotation = Quaternion.Euler(newRotation);

                // 오브젝트의 크기를 변경합니다.
                targetObject.transform.localScale = newScale;

                Debug.Log(targetObject.name + " has been reparented to " + newParent.name +
                          ", moved to position " + newPosition +
                          ", rotated to " + newRotation +
                          ", and scaled to " + newScale);
            }
            else
            {
                Debug.LogWarning("Target object or new parent is not assigned.");
            }
        }
    }
}

*/