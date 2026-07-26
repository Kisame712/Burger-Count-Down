using UnityEngine;
using System;
public class SauceSpawnPoint : MonoBehaviour
{
    private bool hasSauce;
    public static event Action OnAnySauceBottlePlaced;

    private void Awake()
    {
        hasSauce = false;
    }

    public bool TableHasSauce()
    {
        return hasSauce;
    }

    public void SetHasSauce(bool hasSauce)
    {
        this.hasSauce = hasSauce;
    }

    public void BottlePlaced()
    {
        OnAnySauceBottlePlaced?.Invoke();
    }
}
