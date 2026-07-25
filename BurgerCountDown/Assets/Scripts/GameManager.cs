using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private TMP_Text taskCounter;
    [SerializeField] private string[] taskDescriptions;
    [SerializeField] private TMP_Text taskDescriptionText;

    private readonly int writhingInPainHash = Animator.StringToHash("Writhing In Pain");

    

    private enum Task
    {
        Task_One,
        Task_Two,
        Task_Three
    }

    private Task task;

    private void Awake()
    {
        task = Task.Task_One;
    }

    private void Start()
    {
        bossAnimator.Play(writhingInPainHash);
        UpdateTaskCounter();
    }

    private void Update()
    {
        switch (task)
        {
            case Task.Task_One:
                break;
            case Task.Task_Two:
                break;
            case Task.Task_Three:
                break;
        }
    }

    private void UpdateTaskCounter()
    {
        taskCounter.text = $"Tasks : {((int)task)} / 3";
        taskDescriptionText.text = "Current Task :\n" + taskDescriptions[(int)task];
    }
}
