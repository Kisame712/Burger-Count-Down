using UnityEngine;
using System;
public class TrayObject : MonoBehaviour
{
    public static event Action OnAnyTrayCollected;
    private void OnDestroy()
    {
        OnAnyTrayCollected?.Invoke();
    }
}
