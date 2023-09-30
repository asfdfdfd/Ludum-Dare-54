using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RhythmItemTrigger : MonoBehaviour
{
    public UnityEvent OnRhythmItemCaptured = new UnityEvent();

    public UnityEvent OnRhythmItemMissed = new UnityEvent();

    private bool _hasTriggeredRhythmItem = false;
    
    private GameObject _triggeredRhythmItemGameObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RhythmItem"))
        {
            _triggeredRhythmItemGameObject = other.gameObject.transform.parent.gameObject;
            _hasTriggeredRhythmItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
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
        }
    }
}
