using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject levelSelectPanel; // ������ � �������� ������ �������

    // �����, ���������� ��� ������� ������ Play
    public void OnPlayButtonPressed()
    {
        // ���������� ������ ������ �������
        levelSelectPanel.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        // �������� ������ ������ �������
        levelSelectPanel.SetActive(false);
    }

    public void LoadLevel(int levelIndex)
    {
        // ��������� ����� � ��������� ��������
        SceneManager.LoadScene(levelIndex);
    }
}
