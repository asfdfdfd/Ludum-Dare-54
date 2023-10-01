using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scene04 : MonoBehaviour
{
    [SerializeField] private GameObject _hammerGameObject;
    [SerializeField] private GameObject _corpseGameObject;
    [SerializeField] private GameObject _heroGameObject;
    
    [SerializeField] private float _heroSuccessX;
    
    [SerializeField] private float _heroFailY;
    
    [SerializeField] private float _hammerSuccessY;
    [SerializeField] private float _hammerFailY;
    
    private RhythmItemTrigger _rhythmItemTrigger;

    private Vector3 _hammerStartupPosition;

    private Vector3 _lickerStartupPosition;

    private int _currentTargetAbsIndex = 0;

    private void Start()
    {
        _rhythmItemTrigger = GameObject.FindWithTag("RhythmItemTrigger").GetComponent<RhythmItemTrigger>();

        _rhythmItemTrigger.OnRhythmItemCaptured.AddListener(OnRhythmItemCaptured);
        _rhythmItemTrigger.OnRhythmItemMissed.AddListener(OnRhythmMissed);

        _hammerStartupPosition = _hammerGameObject.transform.position;

        _lickerStartupPosition = _heroGameObject.transform.position;
    }

    private void OnRhythmItemCaptured()
    {
        float targetAbsPositionX = 0.0f;
        
        // if (_currentTargetAbsIndex == 0)
        // {
        //     targetAbsPositionX = _heroSuccessLeftDelta;
        // }
        // else
        // {
        //     targetAbsPositionX = _heroSuccessRightDelta;
        // }
        
        var lickerForwardTween = _heroGameObject.transform.DOMoveX(_heroSuccessX, 0.1f);
        var lickerBackwardsTween = _heroGameObject.transform.DOMoveX(_lickerStartupPosition.x, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
        var abs01ForwardTween = _hammerGameObject.transform.DOMoveY(_hammerFailY, 0.1f);
        var abs01BackwardsTween = _hammerGameObject.transform.DOMoveY(_hammerStartupPosition.y, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        SwitchToNextTargetAbs();
    }

    private void SwitchToNextTargetAbs()
    {
        _currentTargetAbsIndex++;
        
        if (_currentTargetAbsIndex == 2)
        {
            _currentTargetAbsIndex = 0;
        }
    }

    private void OnRhythmMissed()
    {
        var lickerForwardTween = _heroGameObject.transform.DOMoveY(_heroFailY, 0.1f);
        var lickerBackwardsTween = _heroGameObject.transform.DOMoveY(_lickerStartupPosition.y, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
        var abs01ForwardTween = _hammerGameObject.transform.DOMoveY(_hammerSuccessY, 0.1f);
        var abs01BackwardsTween = _hammerGameObject.transform.DOMoveY(_hammerStartupPosition.y, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
    }
}
