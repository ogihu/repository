// Assets/Scripts/WheelchairController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairController : InteractableObject
{
    protected override void Start() // �ʱ�ȭ �۾� ����
    {
        base.Start(); // �⺻ �ʱ�ȭ �۾� ����
    }

    protected override void Update() // �� �����Ӹ��� ȣ��Ǿ� ��ȣ�ۿ� ���¸� ������Ʈ
    {
        base.Update(); // �⺻ ������Ʈ �۾� ����
    }

    protected override void LateUpdate() // �� �������� �Ĺݺο� ȣ��Ǿ� �巡�� ���� ��ü���� ��ġ�� ȸ���� ������Ʈ
    {
        base.LateUpdate(); // �⺻ LateUpdate �۾� ����
    }

    protected override void ToggleDragging() // �巡�� ���¸� ����ϰ� �ʿ��� �����͸� �ʱ�ȭ
    {
        base.ToggleDragging(); // �⺻ �巡�� ���� ��� ����
    }

    protected override void UpdateInteractionState() // ��ȣ�ۿ� ���¸� ������Ʈ�Ͽ� ��ü���� �������� ǥ�� �Ǵ� ����
    {
        base.UpdateInteractionState(); // �⺻ ��ȣ�ۿ� ���� ������Ʈ ����
    }
}
