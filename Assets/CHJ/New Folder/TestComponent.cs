using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�

public class ButtonToggle : MonoBehaviour
{

    public List<Button> buttonAList; // A ��ư�� ����Ʈ�� ����
    public Button buttonB; // B ��ư
    public Button quitButton; // ���α׷� ���Ḧ ���� ��ư
    public Text messageText; // �� ��° A ��ư Ŭ�� �� ��Ÿ�� �ؽ�Ʈ

    void Start()
    {
        // �ʱ� ����: A ��ư�鸸 Ȱ��ȭ, B ��ư�� ��Ȱ��ȭ, �ؽ�Ʈ ��Ȱ��ȭ
        foreach (Button buttonA in buttonAList)
        {
            buttonA.gameObject.SetActive(true);
            // �� A ��ư�� Ŭ�� �̺�Ʈ ���
            buttonA.onClick.AddListener(OnButtonAClicked);
        }
        buttonB.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ

        // B ��ư Ŭ�� �̺�Ʈ ����
        buttonB.onClick.AddListener(OnButtonBClicked);

        // ���� ��ư Ŭ�� �̺�Ʈ ����
        quitButton.onClick.AddListener(QuitApplication);
    }

    // A ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    void OnButtonAClicked()
    {
        // ��� A ��ư�� ��Ȱ��ȭ
        foreach (Button buttonA in buttonAList)
        {
            buttonA.gameObject.SetActive(false);
        }

        // �� ��° A ��ư Ŭ�� �� �ؽ�Ʈ Ȱ��ȭ
        if (buttonAList[1].IsActive())
        { // �� ��° ��ư�� �ε����� 1
            messageText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
        }

        // B ��ư Ȱ��ȭ
        buttonB.gameObject.SetActive(true);
    }

    // B ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    void OnButtonBClicked()
    {
        // B ��ư ��Ȱ��ȭ
        buttonB.gameObject.SetActive(false);
        // ��� A ��ư�� �ٽ� Ȱ��ȭ
        foreach (Button buttonA in buttonAList)
        {
            buttonA.gameObject.SetActive(true);
        }
        // �ؽ�Ʈ ��Ȱ��ȭ
        messageText.gameObject.SetActive(false);
    }

    // ���α׷� ���� �޼���
    void QuitApplication()
    {
        print("���α׷� ����");
        Application.Quit();
    }
}