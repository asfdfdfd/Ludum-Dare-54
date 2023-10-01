using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RhythmItem : MonoBehaviour
{
    private AugmentedTimer _augmentedTimer;
    
    private void Start()
    {
        _augmentedTimer = GameObject.FindWithTag("AugmentedTimer").GetComponent<AugmentedTimer>();
    }

    public void Launch(double travelStartTimeSec, double travelFinishTimeSec, Vector3 targetPosition)
    {
        var distance = transform.position.x - targetPosition.x;
        var speed = distance / (travelFinishTimeSec - travelStartTimeSec);
        StartCoroutine(LaunchCoroutine(travelStartTimeSec, travelFinishTimeSec, targetPosition, speed));
    }

    private IEnumerator LaunchCoroutine(double travelStartTimeSec, double travelFinishTimeSec, Vector3 targetPosition, double speed)
    {
        var travelFinishTimeSecNormalized = travelFinishTimeSec - travelStartTimeSec;
        
        _augmentedTimer = GameObject.FindWithTag("AugmentedTimer").GetComponent<AugmentedTimer>();
        
        var startPosition = gameObject.transform.position;
        
        while (true)
        {
            var augmentedTime = _augmentedTimer.GetAugmentedTime();
            var augmentedTimeNormalized = augmentedTime - travelStartTimeSec;
            var ratio = (float) (augmentedTimeNormalized / travelFinishTimeSecNormalized);
            if (ratio >= 1)
            {
                break;
            }
            var lerpedPosition = Vector3.Lerp(startPosition, targetPosition, ratio);
            gameObject.transform.position = lerpedPosition;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.DOMoveX(transform.position.x - 100.0f, (float) speed).SetSpeedBased(true).SetEase(Ease.Linear);
    }
}
