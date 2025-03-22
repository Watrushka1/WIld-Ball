using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = false;

    void Update()
    {
        // ��������� ������� ������� P
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // ���� ���� �� �����, ������� � �����
            }
            else
            {
                PauseGame(); // ���� ���� �� �� �����, ������ �� �����
            }
        }
    }

    // ����� ��� ��������� ���� �� �����
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // ������������� �����
    }

    // ����� ��� ������������� ����
    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // ������������ �����
    }

    // �����, ���������� ��� ������� ������ "�������������"
    public void OnRestartButtonPressed()
    {
        Time.timeScale = 1f; // ������������ ����� ����� ������������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // �����, ���������� ��� ������� ������ "����� � ����"
    public void OnExitButtonPressed()
    {
        Time.timeScale = 1f; // ������������ ����� ����� �������
        SceneManager.LoadScene(0); // ������������, ��� ������� ����� ����� ������ 0
    }
}
