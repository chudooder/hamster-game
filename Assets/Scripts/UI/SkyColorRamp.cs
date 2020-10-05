using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColorRamp : MonoBehaviour
{
    [SerializeField] private float maxScore;
    [SerializeField] private Color[] colors;

    private void Update() {
        int score = GameManager.instance.CurrentScore;
        float gt = score / maxScore;
        int c1i = Mathf.FloorToInt(gt * colors.Length) % colors.Length;
        int c2i = (c1i + 1) % colors.Length;
        Color color = Color.Lerp(colors[c1i], colors[c2i], (score / maxScore * colors.Length)  % 1);
        Camera.main.backgroundColor = color;
    }
}
