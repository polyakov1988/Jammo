using System;
using UnityEngine;

[Serializable]
public class RunningStateConfig
{
    [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }
    [field: SerializeField, Range(1, 2)] public float FastSpeedRate { get; private set; }
    [field: SerializeField, Range(0, 1)] public float WalkRate { get; private set; }
}
