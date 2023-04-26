using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStateSetterModule : StateSetterModule
{
    internal override void OnUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            SetState(MotorState.CustomState0);
            return;
        }
        if (motor.characterController.isGrounded) SetState(MotorState.Grounded);
        else SetState(MotorState.InAir);


    }
}
