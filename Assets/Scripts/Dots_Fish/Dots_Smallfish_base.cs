using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct Dots_Smallfish_base : IComponentData
{
    public float3 direction;
    public float Speed;
    public float trunSpeed;
}
