using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct InputMoveData : IComponentData
{
    public float3 direction;
    public float speed;
    public float trunSpeed;
}
