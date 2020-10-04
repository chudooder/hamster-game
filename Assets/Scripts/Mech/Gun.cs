using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletPrefab;
    public Transform bulletSpawnPoint;

    [SerializeField] private float _barrelForce = .3f;
    [SerializeField] private float _barrelResetTime = .3f;
    [SerializeField] private AnimationCurve _barrelRecoilCurve;
    [SerializeField] private AnimationCurve _barrelRestoreCurve;
    [SerializeField] private Transform _barrel;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private AudioSource _audioSource;

    private float _barrelStartX;
    private float _barrelY;
    private float _barrelEndX;

    private Coroutine _barrelRoutine;
    private Coroutine _flashRoutine;
    private float _t = 0;
    private AudioClip _audioClip;

    private float _flashStartX;
    private float _flashStartY;


    void Awake()
    {
        _barrelStartX = _barrel.transform.localPosition.x;
        _barrelY = _barrel.transform.localPosition.y;
        _barrelEndX = _barrelStartX - _barrelForce;
        
        _muzzleFlash.SetActive(false);
        _audioClip = _audioSource.clip;

        _flashStartX = _muzzleFlash.transform.localPosition.x;
        _flashStartY = _muzzleFlash.transform.localPosition.y;
    }

    // Update is called once per frame
    public void Shoot()
    {
        Transform bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                
        if (_barrelRoutine != null) StopCoroutine(_barrelRoutine);
        _barrelRoutine = StartCoroutine(BarrelRoutine());
    
        if (_flashRoutine != null) StopCoroutine(_flashRoutine);
        _flashRoutine = StartCoroutine(FlashRoutine());

        _audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        _audioSource.PlayOneShot(_audioClip);
    }

    private IEnumerator BarrelRoutine()
    {
        float beginX = _barrel.localPosition.x;
        float t = 0;

        
        while (t < 1)
        {
            _barrel.localPosition = Vector2.Lerp(new Vector2(beginX, _barrelY), new Vector2(_barrelEndX, _barrelY),
                _barrelRecoilCurve.Evaluate(t));
            yield return null;
            
            t += Time.deltaTime / _barrelResetTime / 2;
        }
        
        t = 0;
        while (t < 1)
        {
            _barrel.localPosition = Vector2.Lerp(new Vector2(_barrelEndX, _barrelY), new Vector2(_barrelStartX, _barrelY),
                _barrelRestoreCurve.Evaluate(t));
            yield return null;
            t += Time.deltaTime / _barrelResetTime / 2;
        }
    }

    IEnumerator FlashRoutine()
    {
        _muzzleFlash.transform.localPosition = new Vector2(_flashStartX - (_barrelStartX - _barrel.localPosition.x), _flashStartY);
        
        _muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(.05f);
        _muzzleFlash.SetActive(false);
    }
}
