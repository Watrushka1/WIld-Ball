using UnityEngine;

public class Gem : MonoBehaviour
{
    private Animator animator;
    private bool isCollected = false;

    public ParticleSystem pickupEffect; // ������ �� ������� ������
    public AudioClip pickupSound; // (�����������) ���� ��� �������

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator �� ������ �� ������� " + gameObject.name);
        }

        if (pickupEffect == null)
        {
            // ������� ������� ������ ����� �������� ��������
            pickupEffect = GetComponentInChildren<ParticleSystem>(true); // �������� ����� � ���������� ��������
            if (pickupEffect == null)
            {
                Debug.LogWarning("������� ������ �� ������� �� ������� " + gameObject.name);
            }
        }

        // ��������� ������� ������ ��� ������
        if (pickupEffect != null)
        {
            pickupEffect.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true;

            // ������������� ������ ������
            if (pickupEffect != null)
            {
                // ����������� ������ �� ������, ����� �� �� ����������� ������ � ���
                pickupEffect.transform.parent = null;

                // ������������� ������� ������� �� ������� ����
                pickupEffect.transform.position = transform.position;

                // ���������� ������� ������
                pickupEffect.gameObject.SetActive(true);

                // ������������� ������
                pickupEffect.Play();

                // ���������� ������� ������ ����� ���������� ���������������
                Destroy(pickupEffect.gameObject, pickupEffect.main.duration + pickupEffect.main.startLifetime.constantMax);
            }

            // ������������� ���� ��� �������
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // ��������� �������� ������������
            if (animator != null)
            {
                animator.SetTrigger("Collected");
            }

            // ��������� ���������, ����� ������������� ��������� ������������
            GetComponent<Collider>().enabled = false;

            // ��������� �������� ��� ����������� ������� ����� ��������
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    System.Collections.IEnumerator DestroyAfterAnimation()
    {
        // ��� ������������ �������� ������������
        float disappearAnimationLength = 1.0f; // ������� �������� ������������ ��������
        yield return new WaitForSeconds(disappearAnimationLength);

        // ���������� ������ ������
        Destroy(gameObject);
    }
}
