using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Evo.UI;
public class DialogManager : MonoBehaviour
{
    [SerializeField] private string[] sentences;
    [SerializeField] private float typeSpeed;
    [SerializeField] private float timeToSwitchCameras;
    [SerializeField] private float timeToLoadScene;

    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private TMP_Text textArea;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject bossCamera;


    
    private void SwitchCameras(GameObject camera1, GameObject camera2)
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        for(int i =0; i< sentences.Length; i++)
        {
            textArea.text = "";
            string currentSentence = sentences[i];
            foreach(char ch in currentSentence)
            {
                textArea.text += ch;
                yield return new WaitForSeconds(typeSpeed);
            }
            yield return new WaitForSeconds(1.5f);
            if(i == sentences.Length - 1)
            {
                break;
            }
            if(i % 2 == 0)
            {
                SwitchCameras(playerCamera, bossCamera);
            }
            else
            {
                SwitchCameras(bossCamera, playerCamera);
            }
            backgroundImage.SetActive(false);
            yield return new WaitForSeconds(timeToSwitchCameras);
            backgroundImage.SetActive(true);
        }

        yield return new WaitForSeconds(timeToLoadScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
