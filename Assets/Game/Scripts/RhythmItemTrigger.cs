using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RhythmItemTrigger : MonoBehaviour
{
    [SerializeField] private Material _materialRegular;
    [SerializeField] private Material _materialError;
    [SerializeField] private Material _materialSuccess;
    
    public UnityEvent OnRhythmItemCaptured = new UnityEvent();

    public UnityEvent OnRhythmItemMissed = new UnityEvent();

    private bool _hasTriggeredRhythmItem = false;
    
    private GameObject _triggeredRhythmItemGameObject;

    private SoundPlayer _soundPlayer;

    private HealthManager _healthManager;

    private Tweener _tweenerMaterial;

    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private Color _colorRegular;
    [SerializeField] private Color _colorError;
    [SerializeField] private Color _colorSuccess;
    
    private void Start()
    {
        _soundPlayer = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundPlayer>();

        _healthManager = GameObject.FindWithTag("HealthItems").GetComponent<HealthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RhythmItem"))
        {
            if (!_hasTriggeredRhythmItem)
            {
                _triggeredRhythmItemGameObject = other.gameObject.transform.parent.gameObject;
                _hasTriggeredRhythmItem = true;
            }
            else
            {
                _triggeredRhythmItemGameObject = other.gameObject.transform.parent.gameObject;
                
                OnRhythmItemMissed.Invoke();
                
                _soundPlayer.PlayFailedSound();

                _healthManager.Hit();
                
                StartCoroutine(BlinkError());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_hasTriggeredRhythmItem)
        {
            _hasTriggeredRhythmItem = false;
            _triggeredRhythmItemGameObject = null;

            OnRhythmItemMissed.Invoke();
            
            _soundPlayer.PlayFailedSound();
            
            _healthManager.Hit();
            
            StartCoroutine(BlinkError());
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (_hasTriggeredRhythmItem)
            {
                _hasTriggeredRhythmItem = false;
                Destroy(_triggeredRhythmItemGameObject);
                _triggeredRhythmItemGameObject = null;
                
                OnRhythmItemCaptured.Invoke();
                
                StartCoroutine(BlinkSuccess());

                // _meshRenderer.material = _materialSuccess;
            }
            else
            {
                OnRhythmItemMissed.Invoke();
                
                _soundPlayer.PlayFailedSound();
                
                _healthManager.Hit();

                StartCoroutine(BlinkError());
                
                // _meshRenderer.material = _materialError;
            }
        }
    }

    private IEnumerator BlinkError()
    {
        yield return _meshRenderer.material.DOColor(_colorError, 0.1f).WaitForCompletion();
        yield return _meshRenderer.material.DOColor(_colorRegular, 0.1f).WaitForCompletion();
    }
    
    private IEnumerator BlinkSuccess()
    {
        yield return _meshRenderer.material.DOColor(_colorSuccess, 0.1f).WaitForCompletion();
        yield return _meshRenderer.material.DOColor(_colorRegular, 0.1f).WaitForCompletion();
    }    
}
