using Unity.Entities;

public struct ScoreBufferElement : IBufferElementData
{
    public int redScore;
    public int blueScore;
}