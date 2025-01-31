﻿using System;

using UnityEngine;

using MelonLoader;

using Il2CppInterop.Runtime.InteropTypes.Fields;
using Il2CppInterop.Runtime.Attributes;

namespace FluidLab;

[RegisterTypeInIl2Cpp]
public class FlowVolume : MonoBehaviour
{
    public FlowVolume(IntPtr intPtr) : base(intPtr) { }

    public Il2CppValueField<Vector3> center;

    public Il2CppValueField<Vector3> size;

    public Il2CppValueField<Vector3> flow;

    public Vector3 Velocity
    {
        get
        {
            var transform = this.transform;

            return transform.rotation * flow;
        }
    }

    [HideFromIl2Cpp]
    public event Action OnEnabledEvent, OnDisabledEvent;

    private void OnEnable()
    {
        OnEnabledEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnDisabledEvent?.Invoke();
    }
}