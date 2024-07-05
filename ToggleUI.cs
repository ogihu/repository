using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public GameObject uiPanel; // 드래그 앤 드롭으로 메인 패널을 연결
    public Button phoneButton; // 드래그 앤 드롭으로 전화 버튼을 연결
    public Button kakaoButton; // 드래그 앤 드롭으로 카카오톡 버튼을 연결
    public GameObject[] checkBoxes; // 드래그 앤 드롭으로 체크박스를 연결

    private bool isPanelVisible = false;

    void Start()
    {
        // 처음에는 메인 패널을 비활성화
        uiPanel.SetActive(false);

        // 체크박스들을 비활성화
        SetCheckBoxesActive(false);

        // 버튼 클릭 이벤트 설정
        phoneButton.onClick.AddListener(ShowCheckBoxes);
        kakaoButton.onClick.AddListener(ShowCheckBoxes);
    }

    void Update()
    {
        // P 키가 눌렸는지 확인
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 패널의 활성화 상태를 토글
            isPanelVisible = !isPanelVisible;
            uiPanel.SetActive(isPanelVisible);
        }
    }

    void ShowCheckBoxes()
    {
        // 전화 버튼과 카카오톡 버튼을 비활성화
        phoneButton.gameObject.SetActive(false);
        kakaoButton.gameObject.SetActive(false);

        // 체크박스들을 활성화
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
