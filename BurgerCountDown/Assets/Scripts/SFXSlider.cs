using UnityEngine;
using Evo.UI;
public class SFXSlider : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.5f);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    private void Load()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}
