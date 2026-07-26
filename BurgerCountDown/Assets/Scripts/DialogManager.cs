using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogManager : MonoBehaviour
{
    [SerializeField] private string[] sentences;
    [SerializeField] private float typeSpeed;
    [SerializeField] private float timeToSwitchCameras;

    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private TMP_Text textArea;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject bossCamera;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator bossAnimator;


    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private AudioClip typeSound;
    private readonly int talkingHash = Animator.StringToHash("Talking");
    private readonly int kneelingHash = Animator.StringToHash("Kneeling");


    private int index;
    private void SwitchCameras(GameObject camera1, GameObject camera2)
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
    }

    private void Start()
    {
        index = 0;
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        foreach(char ch in sentences[index].ToCharArray())
        {
            textArea.text += ch;
            SFXPlayer.Instance.PlaySFX(typeSound);
            yield return new WaitForSeconds(typeSpeed);
        }
        continueButton.SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ChangePlayerAnimations()
    {
        playerAnimator.Play(talkingHash);   
    }

    private void ChangeBossAnimations(int index)
    {
        if(index < 4)
        {
            bossAnimator.Play(talkingHash);
        }
        else
        {
            bossAnimator.Play(kneelingHash);
        }
    }

    public void NextSentence()
    {
        StartCoroutine(SwitchCamerasAndChangeAnimations());
    }

    private IEnumerator SwitchCamerasAndChangeAnimations()
    {
        
        continueButton.SetActive(false);
        backgroundImage.SetActive(false);
        if (index % 2 == 0)
        {
            SwitchCameras(playerCamera, bossCamera);
            ChangeBossAnimations(index);
        }
        else
        {
            SwitchCameras(bossCamera, playerCamera);
            ChangePlayerAnimations();
        }
        yield return new WaitForSeconds(timeToSwitchCameras);

        backgroundImage.SetActive(true);

        if (index < sentences.Length - 1)
        {
            textArea.text = "";
            index++;
            StartCoroutine(Type());
        }
        else
        {
            textArea.text = "";
            continueButton.SetActive(false);
        }
    }

    private void Update()
    {

        if (textArea.text == sentences[index] && index == sentences.Length - 1)
        {
            continueButton.SetActive(false);
            startGameButton.SetActive(true);
        }
    }
}
