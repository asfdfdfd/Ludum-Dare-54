using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scene02 : MonoBehaviour
{
    [SerializeField] private GameObject _abs01GameObject;
    [SerializeField] private GameObject _abs02GameObject;
    [SerializeField] private GameObject _lickerGameObject;

    [SerializeField] private float _lickerLeftDelta;
    [SerializeField] private float _lickerRightDelta;
    
    [SerializeField] private float _abs01Delta;
    [SerializeField] private float _abs02Delta;
    
    private RhythmItemTrigger _rhythmItemTrigger;

    private Vector3 _abs01StartupPosition;
    private Vector3 _abs02StartupPosition;

    private Vector3 _lickerStartupPosition;

    private int _currentTargetAbsIndex = 0;

    private void Start()
    {
        _rhythmItemTrigger = GameObject.FindWithTag("RhythmItemTrigger").GetComponent<RhythmItemTrigger>();

        _rhythmItemTrigger.OnRhythmItemCaptured.AddListener(OnRhythmItemCaptured);
        _rhythmItemTrigger.OnRhythmItemMissed.AddListener(OnRhythmMissed);

        _abs01StartupPosition = _abs01GameObject.transform.position;
        _abs02StartupPosition = _abs02GameObject.transform.position;

        _lickerStartupPosition = _lickerGameObject.transform.position;
    }

    private void OnRhythmItemCaptured()
    {
        float targetAbsPositionX;
        float targetAbsRotationY;
        
        if (_currentTargetAbsIndex == 0)
        {
            targetAbsPositionX = _lickerLeftDelta;
            targetAbsRotationY = -90;
        }
        else
        {
            targetAbsPositionX = _lickerRightDelta;
            targetAbsRotationY = 90;
        }
        
        var lickerForwardTween = _lickerGameObject.transform.DOMoveX(_lickerStartupPosition.x + targetAbsPositionX, 0.1f);
        var lickerBackwardsTween = _lickerGameObject.transform.DOMoveX(_lickerStartupPosition.x, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);

        _lickerGameObject.transform.DOLocalRotate(new Vector3(0.0f, targetAbsRotationY, 0.0f), 0.1f);
        
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
        var abs01ForwardTween = _abs01GameObject.transform.DOMoveX(_abs01StartupPosition.x + _abs01Delta, 0.1f);
        var abs01BackwardsTween = _abs01GameObject.transform.DOMoveX(_abs01StartupPosition.x, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        var abs02ForwardTween = _abs02GameObject.transform.DOMoveX(_abs02StartupPosition.x - _abs02Delta, 0.1f);
        var abs02BackwardsTween = _abs02GameObject.transform.DOMoveX(_abs02StartupPosition.x, 0.1f);
        
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardsTween);
    }
}
