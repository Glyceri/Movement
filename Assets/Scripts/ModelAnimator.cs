using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ModelAnimator : MonoBehaviour
{
    [SerializeField] List<Animator> animators;

    public PlayableGraph playableGraph => animators[0].playableGraph;
    public float playbackTime { get => animators[0].playbackTime; set => Act((a) => a.playbackTime = value); }
    public float speed { get => animators[0].speed; set => Act((a) => a.speed = value); }
    public void SetTrigger(string trigger) => Act((a) => a.SetTrigger(trigger));
    public void SetBool(string name, bool value) => Act((a) => a.SetBool(name, value));
    public bool GetBool(string name) => animators[0].GetBool(name);
    public void SetFloat(string name, float value) => Act((a) => a.SetFloat(name, value));
    public float GetFloat(string name) => animators[0].GetFloat(name);
    public void SetInt(string name, int value) => Act((a) => a.SetInteger(name, value));
    public int GetInt(string name) => animators[0].GetInteger(name);
    public int parameterCount => animators[0].parameterCount;
    public UnityEngine.AnimatorControllerParameter[] parameters => animators[0].parameters;
    public Vector3 pivotPosition => animators[0].pivotPosition;
    public float pivotWeight => animators[0].pivotWeight;
    public void ResetTrigger(string name) => Act((a) => a.ResetTrigger(name));
    public bool applyRootMotion { get => animators[0].applyRootMotion; set => Act((a) => a.applyRootMotion = value); }
    public bool hasRootMotion { get => animators[0].hasRootMotion; }
    public void Play(string name) => Act((a) => a.Play(name));

    void Act(Action<Animator> animatorAction)
    {
        foreach (Animator animator in animators)
            animatorAction?.Invoke(animator);
    }
}
