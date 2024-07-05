using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public GameObject uiPanel; // �巡�� �� ������� ���� �г��� ����
    public Button phoneButton; // �巡�� �� ������� ��ȭ ��ư�� ����
    public Button kakaoButton; // �巡�� �� ������� īī���� ��ư�� ����
    public GameObject[] checkBoxes; // �巡�� �� ������� üũ�ڽ��� ����

    private bool isPanelVisible = false;

    void Start()
    {
        // ó������ ���� �г��� ��Ȱ��ȭ
        uiPanel.SetActive(false);

        // üũ�ڽ����� ��Ȱ��ȭ
        SetCheckBoxesActive(false);

        // ��ư Ŭ�� �̺�Ʈ ����
        phoneButton.onClick.AddListener(ShowCheckBoxes);
        kakaoButton.onClick.AddListener(ShowCheckBoxes);
    }

    void Update()
    {
        // P Ű�� ���ȴ��� Ȯ��
        if (Input.GetKeyDown(KeyCode.P))
        {
            // �г��� Ȱ��ȭ ���¸� ���
            isPanelVisible = !isPanelVisible;
            uiPanel.SetActive(isPanelVisible);
        }
    }

    void ShowCheckBoxes()
    {
        // ��ȭ ��ư�� īī���� ��ư�� ��Ȱ��ȭ
        phoneButton.gameObject.SetActive(false);
        kakaoButton.gameObject.SetActive(false);

        // üũ�ڽ����� Ȱ��ȭ
        SetCheckBoxesActive(true);
    }

    void SetCheckBoxesActive(bool isActive)
    {
        foreach (GameObject checkBox in checkBoxes)
        {
            checkBox.SetActive(isActive);
        }
    }
}
