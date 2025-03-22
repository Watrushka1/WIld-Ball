using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Animator animator; // ������ �� ��������� Animator
    public GameObject trapToDisable; // �������, ������� ����� ���������
    public GameObject hintCanvas; // ������ �� Canvas � ����������

    private bool isPlayerNearby = false; // ����, �����������, ��� ����� �����
    private bool isActivated = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator �� ������ �� ������!");
            }
        }

        // ����������, ��� ��������� ������ ��� ������
        if (hintCanvas != null)
        {
            hintCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ActivateButton();
        }
    }

    void ActivateButton()
    {
        if (isActivated) return;

        isActivated = true;

        // ������������� �������� �������
        if (animator != null)
        {
            animator.SetTrigger("Pressed");
        }

        // ��������� �������
        if (trapToDisable != null)
        {
            trapToDisable.SetActive(false);
        }

        // �������� ��������� ����� ������� ������
        if (hintCanvas != null)
        {
            hintCanvas.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            // ���������� ���������
            if (hintCanvas != null)
            {
                hintCanvas.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            // �������� ���������
            if (hintCanvas != null)
            {
                hintCanvas.SetActive(false);
            }
        }
    }
}
