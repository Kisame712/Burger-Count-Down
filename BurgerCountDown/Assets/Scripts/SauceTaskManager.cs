using UnityEngine;
using TMPro;
public class SauceTaskManager : MonoBehaviour
{
    [SerializeField] private TMP_Text sauceObjectsText;
    [SerializeField] private TMP_Text traysFoundText;

    private int totalBottlesToBePlaced;
    private int currentBottles;

    private void Awake()
    {
        totalBottlesToBePlaced = transform.childCount;
        currentBottles = 0;
    }

    private void Start()
    {
        SauceSpawnPoint.OnAnySauceBottlePlaced += SauceSpawnPoint_OnAnySauceBottlePlaced;
        UpdateText();
    }

    private void OnDestroy()
    {
        SauceSpawnPoint.OnAnySauceBottlePlaced -= SauceSpawnPoint_OnAnySauceBottlePlaced;
    }

    private void SauceSpawnPoint_OnAnySauceBottlePlaced()
    {
        currentBottles++;
        UpdateText();
        if(currentBottles == totalBottlesToBePlaced)
        {
            sauceObjectsText.gameObject.SetActive(false);
            GameManager.Instance.TaskComplete();
            traysFoundText.gameObject.SetActive(true);
        }
    }

    private void UpdateText()
    {
        sauceObjectsText.text = $"Bottles placed : {currentBottles} / {totalBottlesToBePlaced}";
    }
}
