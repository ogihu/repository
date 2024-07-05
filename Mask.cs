
/*u
 sing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    // �ڽ����� ������ ������Ʈ
    public GameObject targetObject;

    // ���ο� �θ� ������Ʈ
    public Transform newParent;

    // ����� Ű
    public KeyCode keyToPress = KeyCode.E;

    // �̵��� ��ġ
    public Vector3 newPosition = new Vector3(0.214f, -0.002f, -0.001f); // ���� ��ġ (0, 1, 0)

    // ȸ�� ��
    public Vector3 newRotation = new Vector3(-89.96f, 0, 90.046f); // ���� ȸ�� (0, 90, 0)

    // ũ�� ��
    public Vector3 newScale = new Vector3(1.091504f, 0.8167786f, 1.132167f); // ���� ũ�� (1, 1, 1)

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (targetObject != null && newParent != null)
            {
                // ������Ʈ�� �θ� �����մϴ�.
                targetObject.transform.SetParent(newParent);

                // ������Ʈ�� ��ġ�� �����մϴ�.
                targetObject.transform.localPosition = newPosition;

                // ������Ʈ�� ȸ���� �����մϴ�.
                targetObject.transform.localRotation = Quaternion.Euler(newRotation);

                // ������Ʈ�� ũ�⸦ �����մϴ�.
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