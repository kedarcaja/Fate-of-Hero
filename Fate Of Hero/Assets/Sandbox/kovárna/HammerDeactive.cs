using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HammerDeactive : StateMachineBehaviour
{

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        animator.gameObject.SetActive(false);
        FindObjectOfType<HItPosition>().isChoosingPosition = true;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
