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
    class CubeBaker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {   
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<Cube>(entity);
            AddComponent(entity, new URPMaterialPropertyBaseColor 
            { 
                Value = new float4(1.0F, 1.0F, 1.0F, 1.0F) 
            });
        }
    }
}
