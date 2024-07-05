using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public Transform player;
    public float interactDistance = 1.0f; // 상호작용 거리 조정
    protected bool isDragging = false; // 오브젝트가 드래그 중인지 여부
    protected Vector3 initialPositionOffset; // 드래그 시작 시 위치 오프셋
    protected Quaternion initialRotationOffset; // 드래그 시작 시 회전 오프셋
    public bool isInteractable = false; // 오브젝트가 상호작용 가능한 상태인지 여부
    protected bool wasInteractable = false; // 이전 프레임에서의 상호작용 가능 상태
    protected Collider[] colliders; // 오브젝트와 관련된 모든 콜라이더 배열
    protected Vector3 previousPosition; // 오브젝트의 이전 위치 저장
    protected Material outlineMaterial; // 빨간색 표시를 위한 소재
    protected Material[] originalMaterials; // 원래 소재 배열 저장
    public LayerMask collisionMask; // 충돌 감지를 위한 레이어 마스크

    protected virtual void Start() // 초기화 작업 수행
    {
        // 오브젝트의 모든 콜라이더를 가져옴
        colliders = GetComponentsInChildren<Collider>();
        // 오브젝트의 초기 위치 저장
        previousPosition = transform.position;
        // 빨간색 윤곽선 소재 로드
        outlineMaterial = Resources.Load<Material>("Materials/Environments/DrawOutline");
        // 오브젝트의 Renderer 컴포넌트를 가져와서 원래 소재 배열 저장
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterials = renderer.materials;
        }
    }

    protected virtual void Update() // 매 프레임마다 호출되어 상호작용 상태를 업데이트
    {
        // 플레이어와의 거리 계산 및 상호작용 가능 여부 설정
        float distance = Vector3.Distance(player.position, transform.position);
        isInteractable = distance <= interactDistance;

        // 상호작용 가능 상태일 때 E 키 입력 처리
        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDragging();
        }

        // 이전 상호작용 가능 여부와 비교하여 상태 업데이트
        if (isInteractable != wasInteractable)
        {
            UpdateInteractionState();
        }

        // 상호작용 상태가 false로 변했을 경우 즉시 윤곽선 제거
        if (!isInteractable && wasInteractable)
        {
            RemoveOutline();
        }

        // 현재 상호작용 가능 상태를 이전 상태로 업데이트
        wasInteractable = isInteractable;
    }

    protected virtual void LateUpdate() // 매 프레임의 후반부에 호출되어 드래그 중인 오브젝트의 위치와 회전을 업데이트
    {
        if (isDragging)
        {
            // 플레이어의 위치와 회전을 기반으로 오브젝트의 목표 위치와 회전 계산
            Vector3 targetPosition = player.position + player.rotation * initialPositionOffset;
            Quaternion targetRotation = player.rotation * initialRotationOffset;

            // 충돌 감지 및 처리
            if (!IsCollision(targetPosition))
            {
                // 충돌이 없으면 오브젝트 위치 및 회전 업데이트
                transform.position = targetPosition;
                transform.rotation = targetRotation;
                // 이전 위치 업데이트
                previousPosition = transform.position;
            }
            else
            {
                // 충돌이 있으면 이전 위치로 되돌림
                transform.position = previousPosition;
            }
        }
    }

    protected virtual void ToggleDragging() // 드래그 상태를 토글하고 필요한 데이터를 초기화
    {
        // 드래그 상태 토글
        isDragging = !isDragging;
        if (isDragging)
        {
            // 끌기 시작할 때 플레이어와 오브젝트 간의 위치와 회전 오프셋 저장
            initialPositionOffset = Quaternion.Inverse(player.rotation) * (transform.position - player.position);
            initialRotationOffset = Quaternion.Inverse(player.rotation) * transform.rotation;
            // 초기 위치 저장
            previousPosition = transform.position;
        }
        else
        {
            // 드래그 중지 시 상호작용 가능 상태 해제
            isInteractable = false;
            UpdateInteractionState();
        }
    }

    protected virtual void UpdateInteractionState() // 상호작용 상태를 업데이트하여 오브젝트의 윤곽선을 표시 또는 제거
    {
        // 오브젝트의 Renderer 컴포넌트를 가져옴
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // 현재 소재 배열을 가져옴
            Material[] materials = renderer.materials;
            List<Material> materialList = new List<Material>(materials);

            if (isInteractable)
            {
                // 빨간색으로 표시
                if (!materialList.Contains(outlineMaterial))
                {
                    materialList.Add(outlineMaterial);
                }
            }
            else
            {
                // 빨간색 표시 제거 및 원래 소재로 복원
                materialList.Remove(outlineMaterial);
                renderer.materials = originalMaterials;
            }

            // 업데이트된 소재 배열을 Renderer에 설정
            renderer.materials = materialList.ToArray();
        }
    }

    protected void RemoveOutline() // 윤곽선을 제거하여 원래 상태로 복원
    {
        // 오브젝트의 Renderer 컴포넌트를 가져옴
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // 원래 소재 배열로 복원
            renderer.materials = originalMaterials;
        }
    }

    protected bool IsCollision(Vector3 targetPosition) // 목표 위치에서 충돌 여부를 확인
    {
        // 각 콜라이더에서 충돌 여부 확인
        foreach (var collider in colliders)
        {
            // CheckBox를 사용하여 충돌 감지
            if (Physics.CheckBox(targetPosition, collider.bounds.extents, transform.rotation, collisionMask))
            {
                return true;
            }
        }
        return false;
    }
}
