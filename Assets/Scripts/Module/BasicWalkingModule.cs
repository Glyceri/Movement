using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpModule))]
public class BasicWalkingModule : MotorModule
{
    [SerializeField] float _maxSpeed = 5f;
    [SerializeField] float _maxRunSpeed = 10f;
    [SerializeField] float _accelerationSpeed = 2f;
    [SerializeField] AnimationCurve _accelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] float _deccelerationSpeed = 3f;
    [SerializeField] AnimationCurve _deccelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] AnimationCurve _speedCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    [JoystickCircle]
    [SerializeField] 
    Vector2 input;

    public override void OnUpdate(MotorState state)
    {
        CameraRig rig = motor.cameraRig;
        if (rig == null) return;

        Vector3 forw = rig.forward;
        forw.y = 0;
        forw.Normalize();
        Vector3 right = rig.right;
        right.y = 0;
        right.Normalize();


        float speed = _maxSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
            speed = _maxRunSpeed;

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.magnitude > 1) input.Normalize();

        Vector2 speedInput = input * speed;

        motor.AddVelocity(forw * speedInput.y);
        motor.AddVelocity(right * speedInput.x);
    }
}
