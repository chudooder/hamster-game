using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    void Start()
    {
        GameManager.instance.ScoreListener += SetText;
    }

    private void SetText() {
        _text.text = GameManager.instance.CurrentScore.ToString();
    }

    private void OnDestroy() {
        GameManager.instance.ScoreListener -= SetText;
    }
}
