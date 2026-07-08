using UnityEngine;
using Unity.Entities;
using Unity.NetCode;

public struct Score : IComponentData
{
    
}

public struct ScoreBufferElement : IBufferElementData
{
    [GhostField]public int redScore;
    
    [GhostField]public int blueScore;
}

[DisallowMultipleComponent]
public class ScoreAuthoring : MonoBehaviour
{
    class ScoreBaker : Baker<ScoreAuthoring>
    {
        public override void Bake(ScoreAuthoring authoring)
        {   
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<Score>(entity);
            AddBuffer<ScoreBufferElement>(entity);
            AppendToBuffer(entity, new ScoreBufferElement{redScore = 0, blueScore = 0});
        }
    }
}
