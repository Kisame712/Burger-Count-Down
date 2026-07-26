using UnityEngine;
using TMPro;

public class TrashObjectsParent : MonoBehaviour
{
    [SerializeField] private TMP_Text trashObjectsText;
    [SerializeField] private TMP_Text sauceObjectsText;
    private int children;

    private int pickedTrash;

    private void Start()
    {
        children = transform.childCount;
        TrashObject.OnAnyTrashDestroyed += TrashObject_OnAnyTrashDestroyed;
        UpdateTrashText();
    }

    private void OnDestroy()
    {
        TrashObject.OnAnyTrashDestroyed -= TrashObject_OnAnyTrashDestroyed;
    }

    private void TrashObject_OnAnyTrashDestroyed()
    {
        pickedTrash++;
        UpdateTrashText();
        if(pickedTrash == children)
        {
            trashObjectsText.gameObject.SetActive(false);
            GameManager.Instance.TaskComplete();
            sauceObjectsText.gameObject.SetActive(true);
        }
    }

    private void UpdateTrashText()
    {
        trashObjectsText.text = $"Trash picked : {pickedTrash} / {children}";
    }
}
