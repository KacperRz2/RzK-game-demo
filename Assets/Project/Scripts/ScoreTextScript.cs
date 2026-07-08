using TMPro;
using Unity.Entities;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    public TMP_Text text;
    private int redScore = 0;
    private int blueScore = 0;

    public void UpdateScore(int red, int blue)
    {
        if(red != redScore || blue != blueScore)
        {
            (redScore, blueScore) = (red, blue);
            text.text = $"Red: {redScore}\tBlue: {blueScore}";
        }
    }
}

public partial class UpdateScoreUISystem : SystemBase
{
    private ScoreTextScript uiScript;

    protected override void OnCreate()
    {
        RequireForUpdate<ScoreBufferElement>();
    }
    protected override void OnUpdate()
    {
        if (uiScript == null)
        {
            uiScript = Object.FindFirstObjectByType<ScoreTextScript>();
            if (uiScript == null) return;
        }
        var buffer = SystemAPI.GetSingletonBuffer<ScoreBufferElement>();
        int redScore = buffer[buffer.Length - 1].redScore;
        int blueScore = buffer[buffer.Length - 1].blueScore;
        uiScript.UpdateScore(redScore, blueScore);
    }
}