using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Mathematics;

public struct Cube : IComponentData
{
    
}

[DisallowMultipleComponent]
public class CubeAuthoring : MonoBehaviour
{
    public Color colour = Color.red;
    class CubeBaker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {   
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<Cube>(entity);
            AddComponent(entity, new URPMaterialPropertyBaseColor 
            { 
                Value = new float4(authoring.colour.r, authoring.colour.g, authoring.colour.b, authoring.colour.a) 
            });
        }
    }
}
