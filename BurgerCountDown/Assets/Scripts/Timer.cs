using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int totalTimeInSeconds;
    private float timer;

    private int minutes;
    private int seconds;

    private void Awake()
    {
        timer = totalTimeInSeconds;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0);

        minutes = Mathf.RoundToInt(timer) / 60;

        seconds = Mathf.RoundToInt(timer) % 60;

        timerText.text = $"{minutes} : {seconds}";

        if(timer == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }


}
