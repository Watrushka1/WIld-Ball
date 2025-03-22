using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator; // ������ �� ��������� Animator
    private bool isPlayerNearby = false; // ����, �����������, ��� ����� �����
    private bool isOpen = false; // ��������� ����� (�������/�������)

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator �� ������ �� �����!");
            }
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    void ToggleDoor()
    {
        isOpen = !isOpen; // ����������� ��������� �����

        // ������������� �������� � Animator
        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen);
        }

        // ����� �������� ���� ��������/�������� �����
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            // ����� �������� ��������� "������� E, ����� ������� �����"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            // ������ ���������
        }
    }
}
