using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    public Animator blackBackground;

    public void FadeIn()
    {
        // todo: implement FadeIn animation
        blackBackground.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        blackBackground.SetTrigger("FadeOut");
    }
}
