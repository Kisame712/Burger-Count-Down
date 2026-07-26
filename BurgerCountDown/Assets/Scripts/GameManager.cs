using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private TMP_Text taskCounter;
    [SerializeField] private string[] taskDescriptions;
    [SerializeField] private TMP_Text taskDescriptionText;

    private readonly int writhingInPainHash = Animator.StringToHash("Writhing In Pain");

    public static GameManager Instance { private set; get; }

    private enum Task
    {
        Task_One,
        Task_Two,
        Task_Three
    }

    private Task task;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one instane of GameManager - " + transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        task = Task.Task_One;
    }

    private void Start()
    {
        bossAnimator.Play(writhingInPainHash);
        UpdateTaskCounter();
    }

    public void TaskComplete()
    {
        switch (task)
        {
            case Task.Task_One:
                task = Task.Task_Two;
                UpdateTaskCounter();
                break;
            case Task.Task_Two:
                task = Task.Task_Three;
                UpdateTaskCounter();
                break;
            case Task.Task_Three:
                SceneManager.LoadScene("Win");
                break;
        }

    }

    public int GetCurrentTask()
    {
        return (int)task;
    }

    private void UpdateTaskCounter()
    {
        taskCounter.text = $"Tasks : {((int)task)} / 3";
        taskDescriptionText.text = "Current Task :\n" + taskDescriptions[(int)task];
    }
}
