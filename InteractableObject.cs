using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public Transform player;
    public float interactDistance = 1.0f; // ��ȣ�ۿ� �Ÿ� ����
    protected bool isDragging = false; // ������Ʈ�� �巡�� ������ ����
    protected Vector3 initialPositionOffset; // �巡�� ���� �� ��ġ ������
    protected Quaternion initialRotationOffset; // �巡�� ���� �� ȸ�� ������
    public bool isInteractable = false; // ������Ʈ�� ��ȣ�ۿ� ������ �������� ����
    protected bool wasInteractable = false; // ���� �����ӿ����� ��ȣ�ۿ� ���� ����
    protected Collider[] colliders; // ������Ʈ�� ���õ� ��� �ݶ��̴� �迭
    protected Vector3 previousPosition; // ������Ʈ�� ���� ��ġ ����
    protected Material outlineMaterial; // ������ ǥ�ø� ���� ����
    protected Material[] originalMaterials; // ���� ���� �迭 ����
    public LayerMask collisionMask; // �浹 ������ ���� ���̾� ����ũ

    protected virtual void Start() // �ʱ�ȭ �۾� ����
    {
        // ������Ʈ�� ��� �ݶ��̴��� ������
        colliders = GetComponentsInChildren<Collider>();
        // ������Ʈ�� �ʱ� ��ġ ����
        previousPosition = transform.position;
        // ������ ������ ���� �ε�
        outlineMaterial = Resources.Load<Material>("Materials/Environments/DrawOutline");
        // ������Ʈ�� Renderer ������Ʈ�� �����ͼ� ���� ���� �迭 ����
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterials = renderer.materials;
        }
    }

    protected virtual void Update() // �� �����Ӹ��� ȣ��Ǿ� ��ȣ�ۿ� ���¸� ������Ʈ
    {
        // �÷��̾���� �Ÿ� ��� �� ��ȣ�ۿ� ���� ���� ����
        float distance = Vector3.Distance(player.position, transform.position);
        isInteractable = distance <= interactDistance;

        // ��ȣ�ۿ� ���� ������ �� E Ű �Է� ó��
        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDragging();
        }

        // ���� ��ȣ�ۿ� ���� ���ο� ���Ͽ� ���� ������Ʈ
        if (isInteractable != wasInteractable)
        {
            UpdateInteractionState();
        }

        // ��ȣ�ۿ� ���°� false�� ������ ��� ��� ������ ����
        if (!isInteractable && wasInteractable)
        {
            RemoveOutline();
        }

        // ���� ��ȣ�ۿ� ���� ���¸� ���� ���·� ������Ʈ
        wasInteractable = isInteractable;
    }

    protected virtual void LateUpdate() // �� �������� �Ĺݺο� ȣ��Ǿ� �巡�� ���� ������Ʈ�� ��ġ�� ȸ���� ������Ʈ
    {
        if (isDragging)
        {
            // �÷��̾��� ��ġ�� ȸ���� ������� ������Ʈ�� ��ǥ ��ġ�� ȸ�� ���
            Vector3 targetPosition = player.position + player.rotation * initialPositionOffset;
            Quaternion targetRotation = player.rotation * initialRotationOffset;

            // �浹 ���� �� ó��
            if (!IsCollision(targetPosition))
            {
                // �浹�� ������ ������Ʈ ��ġ �� ȸ�� ������Ʈ
                transform.position = targetPosition;
                transform.rotation = targetRotation;
                // ���� ��ġ ������Ʈ
                previousPosition = transform.position;
            }
            else
            {
                // �浹�� ������ ���� ��ġ�� �ǵ���
                transform.position = previousPosition;
            }
        }
    }

    protected virtual void ToggleDragging() // �巡�� ���¸� ����ϰ� �ʿ��� �����͸� �ʱ�ȭ
    {
        // �巡�� ���� ���
        isDragging = !isDragging;
        if (isDragging)
        {
            // ���� ������ �� �÷��̾�� ������Ʈ ���� ��ġ�� ȸ�� ������ ����
            initialPositionOffset = Quaternion.Inverse(player.rotation) * (transform.position - player.position);
            initialRotationOffset = Quaternion.Inverse(player.rotation) * transform.rotation;
            // �ʱ� ��ġ ����
            previousPosition = transform.position;
        }
        else
        {
            // �巡�� ���� �� ��ȣ�ۿ� ���� ���� ����
            isInteractable = false;
            UpdateInteractionState();
        }
    }

    protected virtual void UpdateInteractionState() // ��ȣ�ۿ� ���¸� ������Ʈ�Ͽ� ������Ʈ�� �������� ǥ�� �Ǵ� ����
    {
        // ������Ʈ�� Renderer ������Ʈ�� ������
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // ���� ���� �迭�� ������
            Material[] materials = renderer.materials;
            List<Material> materialList = new List<Material>(materials);

            if (isInteractable)
            {
                // ���������� ǥ��
                if (!materialList.Contains(outlineMaterial))
                {
                    materialList.Add(outlineMaterial);
                }
            }
            else
            {
                // ������ ǥ�� ���� �� ���� ����� ����
                materialList.Remove(outlineMaterial);
                renderer.materials = originalMaterials;
            }

            // ������Ʈ�� ���� �迭�� Renderer�� ����
            renderer.materials = materialList.ToArray();
        }
    }

    protected void RemoveOutline() // �������� �����Ͽ� ���� ���·� ����
    {
        // ������Ʈ�� Renderer ������Ʈ�� ������
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // ���� ���� �迭�� ����
            renderer.materials = originalMaterials;
        }
    }

    protected bool IsCollision(Vector3 targetPosition) // ��ǥ ��ġ���� �浹 ���θ� Ȯ��
    {
        // �� �ݶ��̴����� �浹 ���� Ȯ��
        foreach (var collider in colliders)
        {
            // CheckBox�� ����Ͽ� �浹 ����
            if (Physics.CheckBox(targetPosition, collider.bounds.extents, transform.rotation, collisionMask))
            {
                return true;
            }
        }
        return false;
    }
}
