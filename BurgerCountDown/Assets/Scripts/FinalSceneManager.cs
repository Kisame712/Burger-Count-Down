using UnityEngine;
using UnityEngine.SceneManagement;
using Evo.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class FinalSceneManager : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        exitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        });
    }
}
