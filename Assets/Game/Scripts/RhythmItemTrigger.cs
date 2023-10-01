using System;
using UnityEngine;
using UnityEngine.Events;

public class RhythmItemTrigger : MonoBehaviour
{
    public UnityEvent OnRhythmItemCaptured = new UnityEvent();

    public UnityEvent OnRhythmItemMissed = new UnityEvent();

    private bool _hasTriggeredRhythmItem = false;
    
    private GameObject _triggeredRhythmItemGameObject;

    private SoundPlayer _soundPlayer;

    private void Start()
    {
        _soundPlayer = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundPlayer>();
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
            }
            else
            {
                OnRhythmItemMissed.Invoke();
                
                _soundPlayer.PlayFailedSound();
            }
        }
    }
}
