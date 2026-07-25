using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;

    private readonly int writhingInPainHash = Animator.StringToHash("Writhing In Pain");

    private void Start()
    {
        bossAnimator.Play(writhingInPainHash);
    }
}
