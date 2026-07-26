using UnityEngine;
using TMPro;
public class TrayParent : MonoBehaviour
{
    [SerializeField] private TMP_Text traysFoundText;

    private int traysCollected;
    private int totalTrays;

    private void Awake()
    {
        traysCollected = 0;
        totalTrays = transform.childCount;
    }

    private void Start()
    {
        TrayObject.OnAnyTrayCollected += TrayObject_OnAnyTrayCollected;
        UpdateText();
    }

    private void TrayObject_OnAnyTrayCollected()
    {
        traysCollected++;
        UpdateText();
        if(traysCollected == totalTrays)
        {
            GameManager.Instance.TaskComplete();
        }
    }

    private void OnDestroy()
    {
        TrayObject.OnAnyTrayCollected -= TrayObject_OnAnyTrayCollected;
    }

    private void UpdateText()
    {
        traysFoundText.text = $"Trays Found : {traysCollected} / {totalTrays}";
    }
}
