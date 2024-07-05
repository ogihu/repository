// Assets/Scripts/WheelchairController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairController : InteractableObject
{
    protected override void Start() // 초기화 작업 수행
    {
        base.Start(); // 기본 초기화 작업 수행
    }

    protected override void Update() // 매 프레임마다 호출되어 상호작용 상태를 업데이트
    {
        base.Update(); // 기본 업데이트 작업 수행
    }

    protected override void LateUpdate() // 매 프레임의 후반부에 호출되어 드래그 중인 휠체어의 위치와 회전을 업데이트
    {
        base.LateUpdate(); // 기본 LateUpdate 작업 수행
    }

    protected override void ToggleDragging() // 드래그 상태를 토글하고 필요한 데이터를 초기화
    {
        base.ToggleDragging(); // 기본 드래그 상태 토글 수행
    }

    protected override void UpdateInteractionState() // 상호작용 상태를 업데이트하여 휠체어의 윤곽선을 표시 또는 제거
    {
        base.UpdateInteractionState(); // 기본 상호작용 상태 업데이트 수행
    }
}
