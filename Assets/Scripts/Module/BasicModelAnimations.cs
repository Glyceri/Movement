using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicModelAnimations : MotorModule
{
    [SerializeField] ModelAnimator animator;

    public override void OnLateUpdate(MotorState state)
    {
        float velocity = motor.getSanitizedVelocity.magnitude;

        animator.SetFloat("Speed", velocity);
        animator.SetFloat("AnimationSpeed", Utils.Map(velocity, 0, 1.5f, 0, 1));
        animator.SetFloat("YVelocity", motor.charVelocity.y);
        animator.SetBool("Grounded", state.IsGrounded());
        if (state.IsInAir()) {
            float timeInAir = animator.GetFloat("TimeInAir");
            animator.SetFloat("TimeInAir", timeInAir + Time.deltaTime);
        }
        else
        {
            animator.SetFloat("TimeInAir", 0);
        }
    }
}
