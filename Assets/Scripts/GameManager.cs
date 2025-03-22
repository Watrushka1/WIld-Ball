using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �������� ��� ������� � GameManager �� ������ ��������
    public static GameManager instance;

    // ������
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject gameWonPanel; // ����� ������ ��� ������ � ����

    // ������ ���������� ������
    public ParticleSystem levelCompleteEffect;

    private bool isPaused = false;

    void Awake()
    {
        // ����������� ��������
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ��������� ������ ��� ������
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
        if (gameWonPanel != null)
            gameWonPanel.SetActive(false);

        // ����������, ��� ������ ���������� ������ �������� ��� ������
        if (levelCompleteEffect != null)
        {
            levelCompleteEffect.Stop();
        }
    }

    void Update()
    {
        // ��������� ������� ������� Esc ��� �����
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
        if (pausePanel != null)
            pausePanel.SetActive(true);
        Time.timeScale = 0f; // ������������� �����
    }

    // ����� ��� ������������� ����
    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null)
            pausePanel.SetActive(false);
        Time.timeScale = 1f; // ������������ �����
    }

    // ����� ���������� ��� ���������
    public void GameOver()
    {
        // ���������� ������ "�� ���������"
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // ����� ���������� ��� ���������� ������
    public void LevelComplete()
    {
        // ������������� �����
        //Time.timeScale = 0f;
        isPaused = true;

        // ������������� ������ ���������� ������
        if (levelCompleteEffect != null)
        {
            levelCompleteEffect.Play();
        }

        // ���������, �������� �� ������� ����� ���������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 6) // �������� 5 �� ������ ����� ��������� �����
        {
            // ��� ��������� �������, ���������� ������ ������
            if (gameWonPanel != null)
            {
                gameWonPanel.SetActive(true);
            }
        }
        else
        {
            // �� ��������� �������, ���������� ������ ���������� ������
            if (levelCompletePanel != null)
            {
                levelCompletePanel.SetActive(true);
            }
        }
    }

    // ����� ��� ����������� �������� ������
    public void RestartLevel()
    {
        Time.timeScale = 1f; // ������������ �����
        isPaused = false;

        // ������������� ������� �������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ����� ��� �������� ���������� ������
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // ������������ �����
        isPaused = false;

        // �������� ������ ������� �����
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���������, ���������� �� ��������� �������
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // ��������� ��������� �������
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // ���� ��� ��������� �������, �������������� �� ������ ������� �����
            LoadFirstLevel();
        }
    }

    // ����� ��� �������� ������ ������� �����
    public void LoadFirstLevel()
    {
        Time.timeScale = 1f; // ������������ �����
        isPaused = false;

        // �������� 1 �� ������ ����� ������ ������� �����
        SceneManager.LoadScene(2);
    }

    // �����, ���������� ��� ������� ������ "����� � ����"
    public void OnExitButtonPressed()
    {
        Time.timeScale = 1f; // ������������ ����� ����� �������
        isPaused = false;

        // ��������� �� ����� � �������� 0 (������� ����)
        SceneManager.LoadScene(0);
    }
}
