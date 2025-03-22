using UnityEngine;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �������� ����� GameOver �� GameManager
            GameManager.instance.LevelComplete();
        }
    }
}
