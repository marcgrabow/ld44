using _42.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShakeBehavior : MonoBehaviour {

    private Camera _cam;
    public float Duration = 0.3f;
    public float Strength = 0.5f;
    public int Vibrato = 10;
    public float LightStrength = 0.1f;
    public int LightVibrato = 5;

    void Awake () {
        _cam = GetComponent<Camera>();
        Events.Register(this);		
	}

    [EventListener(Assets.EventEnum.AddPoint)]
    void ApplyLightShake()
    {
        _cam.DOShakePosition(Duration, LightStrength, LightVibrato);
    }

    [EventListener(Assets.EventEnum.RemoveHealth)]
    void ApplyStrongShake()
    {
        _cam.DOShakePosition(Duration, Strength, Vibrato);
    }
	
}
