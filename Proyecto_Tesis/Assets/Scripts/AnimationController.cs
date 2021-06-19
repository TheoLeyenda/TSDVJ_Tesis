using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    enum TypeInitAnimation
    {
        Bool,
        Trigger,
        Float,
        Int,
    }
    [SerializeField] private Animator animator;
    [SerializeField] private TypeInitAnimation typeInitAnimation;
    [SerializeField] private bool useInitAnimation;
    [SerializeField] private string nameInitAnimation;
    [SerializeField] private bool valueBoolInitAnimation;
    [SerializeField] private float valueFloatInitAnimation;
    [SerializeField] private int valueIntInitAnimation;

    void Start()
    {
        if (useInitAnimation)
        {
            switch (typeInitAnimation)
            {
                case TypeInitAnimation.Bool:
                    animator.SetBool(nameInitAnimation, valueBoolInitAnimation);
                    break;
                case TypeInitAnimation.Float:
                    animator.SetFloat(nameInitAnimation, valueFloatInitAnimation);
                    break;
                case TypeInitAnimation.Int:
                    animator.SetInteger(nameInitAnimation, valueIntInitAnimation);
                    break;
                case TypeInitAnimation.Trigger:
                    animator.SetTrigger(nameInitAnimation);
                    break;
            }
        }
    }

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

        //Debug.Log(_state);
        if (_state)
            animator.SetBool(name, false);
        else
            animator.SetBool(name, true);
    }
}
