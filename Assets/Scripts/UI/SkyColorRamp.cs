using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColorRamp : MonoBehaviour
{
    [SerializeField] private float maxScore;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private void Update() {
        int score = GameManager.instance.CurrentScore;
        Color color = Color.Lerp(startColor, endColor, score / maxScore);
        Camera.main.backgroundColor = color;
    }
}
