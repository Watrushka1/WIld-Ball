using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �������� ������ �� ������ BallController
            BallController playerController = other.GetComponent<BallController>();
            if (playerController != null)
            {
                // �������� ����� Die() � ������
                playerController.Die();
            }
        }
    }
}
