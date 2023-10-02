using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RhythmItemTrigger : MonoBehaviour
{
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

    private Vector3 _startScale;

    [SerializeField] private GameObject _vfxGotit;
    private ParticleSystem _particleSystemGotit;
    
    private void Awake()
    {
        _particleSystemGotit = _vfxGotit.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _soundPlayer = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundPlayer>();

        _healthManager = GameObject.FindWithTag("HealthItems").GetComponent<HealthManager>();

        _startScale = gameObject.transform.localScale;
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
        _meshRenderer.material.DOColor(_colorError, 0.1f);

        var targetScale = _startScale - new Vector3(0.1f, 0.1f, 0.1f);
        yield return gameObject.transform.DOScale(targetScale, 0.1f).WaitForCompletion();

        _meshRenderer.material.DOColor(_colorRegular, 0.1f);
        yield return gameObject.transform.DOScale(_startScale, 0.1f).WaitForCompletion();
    }
    
    private IEnumerator BlinkSuccess()
    {
        _meshRenderer.material.DOColor(_colorSuccess, 0.1f);

        var targetScale = _startScale + new Vector3(0.1f, 0.1f, 0.1f);
        yield return gameObject.transform.DOScale(targetScale, 0.1f).WaitForCompletion();

        _meshRenderer.material.DOColor(_colorRegular, 0.1f);
        yield return gameObject.transform.DOScale(_startScale, 0.1f).WaitForCompletion();
        
        _particleSystemGotit.Play(true);
    }    
}
