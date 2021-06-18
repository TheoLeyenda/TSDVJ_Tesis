using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimation(string nameAnimation)
    {
        animator.Play(nameAnimation);
    }

    public void ChangeBuleanTrue(string name)
    {
        animator.SetBool(name, true);
    }

    public void ChangeBuleanFalse(string name)
    {
        animator.SetBool(name, false);
    }

    public void ToggleBulean(string name)
    {
        bool _state = animator.GetBool(name);

        if (_state)
            animator.SetBool(name, false);
        else
            animator.SetBool(name, true);
    }
}
