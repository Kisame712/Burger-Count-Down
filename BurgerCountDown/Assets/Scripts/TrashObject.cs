using UnityEngine;
using System;
public class TrashObject : MonoBehaviour
{
    public static event Action OnAnyTrashDestroyed;

    private void OnDestroy()
    {
        OnAnyTrashDestroyed?.Invoke();
    }
}
