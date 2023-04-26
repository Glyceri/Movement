using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicModelModule : MotorModule
{
    public override void OnLateUpdate(MotorState state)
    {
        float velocity = motor.getSanitizedVelocity.magnitude;

        if(velocity > float.Epsilon)
        {
            if (motor.getSanitizedVelocity.magnitude >=0.9f)
            motor.model.rotation = (Quaternion.FromToRotation(Vector3.forward, motor.getSanitizedVelocity.normalized));
            Vector3 euler = motor.model.rotation.eulerAngles;
            euler = new Vector3(0, euler.y, 0);
            motor.model.rotation = Quaternion.Euler(euler);
        }
    }
}
