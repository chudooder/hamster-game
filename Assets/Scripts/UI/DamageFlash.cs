using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Image _image; 
    [SerializeField] private AnimationCurve _transparancyCurve;
    [SerializeField] private float _flashTime = 1;
    private Coroutine _flashRoutine;
    
    void Start()
    {
        GameManager.instance.HealthListener += Flash;
    }

    public void Flash()
    {
        if (_flashRoutine != null) StopCoroutine(_flashRoutine);
        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float t = 0;
        
        while (t < 1)
        {
            _image.color=  new Color(_image.color.r, _image.color.g, _image.color.b, 
                                    Mathf.Lerp(0, 1, _transparancyCurve.Evaluate(t)));
            yield return null;
            t += Time.deltaTime / _flashTime;
        }
        _image.color=  new Color(_image.color.r, _image.color.g, _image.color.b, 
            Mathf.Lerp(0, 1, _transparancyCurve.Evaluate(1)));
    }
}
