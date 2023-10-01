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
        StartCoroutine(LaunchCoroutine(travelStartTimeSec, travelFinishTimeSec, targetPosition));
        
        // var startX = transform.position.x;
        //
        // gameObject.transform
        //     .DOMove(Vector3.zero, travelDurationSec)
        //     .SetEase(Ease.Linear);        

        // var forwardTween = gameObject.transform
        //     .DOMoveX(startX * -1, 3.0f * 2.0f)
        //     .SetEase(Ease.Linear);

        // var nextTween = gameObject.transform
        //     .DOMoveX(startX * -1, 5.5f)
        //     .SetEase(Ease.Linear);
        //
        // DOTween.Sequence().Append(forwardTween).Append(nextTween)
        //     .OnComplete(() => { Destroy(gameObject); });
    }

    private IEnumerator LaunchCoroutine(double travelStartTimeSec, double travelFinishTimeSec, Vector3 targetPosition)
    {
        var travelFinishTimeSecNormalized = travelFinishTimeSec - travelStartTimeSec;
        
        _augmentedTimer = GameObject.FindWithTag("AugmentedTimer").GetComponent<AugmentedTimer>();
        
        var startPosition = gameObject.transform.position;
        
        while (true)
        {
            var augmentedTime = _augmentedTimer.GetAugmentedTime();
            var augmentedTimeNormalized = augmentedTime - travelStartTimeSec;
            var lerpedPosition = Vector3.Lerp(startPosition, targetPosition, (float) (augmentedTimeNormalized / travelFinishTimeSecNormalized));
            gameObject.transform.position = lerpedPosition;
            yield return new WaitForEndOfFrame();
        }
    }
}
