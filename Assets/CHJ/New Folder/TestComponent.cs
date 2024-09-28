using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�

public class ButtonToggle : MonoBehaviour
{

    public Button buttonA; // A ��ư
    public Button buttonB; // B ��ư

    void Start()
    {
        // �ʱ� ����: A ��ư�� Ȱ��ȭ, B ��ư�� ��Ȱ��ȭ
        buttonA.gameObject.SetActive(true);
        buttonB.gameObject.SetActive(false);

        // A ��ư Ŭ�� �̺�Ʈ ����
        buttonA.onClick.AddListener(OnButtonAClicked);

        // B ��ư Ŭ�� �̺�Ʈ ����
        buttonB.onClick.AddListener(OnButtonBClicked);
    }

    // A ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    void OnButtonAClicked()
    {
        buttonA.gameObject.SetActive(false); // A ��ư ��Ȱ��ȭ
        buttonB.gameObject.SetActive(true);  // B ��ư Ȱ��ȭ
    }

    // B ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    void OnButtonBClicked()
    {
        buttonB.gameObject.SetActive(false); // B ��ư ��Ȱ��ȭ
        buttonA.gameObject.SetActive(true);  // A ��ư Ȱ��ȭ
    }
}
