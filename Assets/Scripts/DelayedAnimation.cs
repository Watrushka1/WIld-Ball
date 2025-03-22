using UnityEngine;
using System.Collections;

public class DelayedAnimation : MonoBehaviour
{
    [Tooltip("�������� ����� ������� �������� � ��������")]
    public float delay = 0f; // ������������� ��������

    [Tooltip("�������� ��������� �������� ��� ���������������")]
    public string animationStateName = "MyAnimation"; // �������� ��������

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("��������� Animator �� ������ �� ������� " + gameObject.name);
            return;
        }

        // ������������� ��������, ��������� �������� � 0
        animator.speed = 0f;

        // ��������� �������� ��� ������ �������� ����� ��������
        StartCoroutine(StartAnimationAfterDelay());
    }

    IEnumerator StartAnimationAfterDelay()
    {
        // ��� �������� ��������
        yield return new WaitForSeconds(delay);

        // ��������� ���������� ��������� ��������
        animator.Play(animationStateName);

        // ��������� ��������, ��������� �������� � 1
        animator.speed = 1f;
    }
}
