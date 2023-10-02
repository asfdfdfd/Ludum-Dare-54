using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scene01 : Scene
{
    [SerializeField] private GameObject _abs01GameObject;
    [SerializeField] private GameObject _abs02GameObject;
    [SerializeField] private GameObject _lickerGameObject;

    [SerializeField] private float _lickerLeftX;
    [SerializeField] private float _lickerRightX;
    
    [SerializeField] private float _abs01X;
    [SerializeField] private float _abs02X;
    
    private RhythmItemTrigger _rhythmItemTrigger;

    private Vector3 _abs01StartupPosition;
    private Vector3 _abs02StartupPosition;

    private Vector3 _lickerStartupPosition;

    private int _currentTargetAbsIndex = 0;

    private Vector3 _abs01StartScale;
    private Vector3 _abs02StartScale;
    
    private void Start()
    {
        _rhythmItemTrigger = GameObject.FindWithTag("RhythmItemTrigger").GetComponent<RhythmItemTrigger>();

        _rhythmItemTrigger.OnRhythmItemCaptured.AddListener(OnRhythmItemCaptured);
        _rhythmItemTrigger.OnRhythmItemMissed.AddListener(OnRhythmMissed);

        _abs01StartupPosition = _abs01GameObject.transform.localPosition;
        _abs02StartupPosition = _abs02GameObject.transform.localPosition;

        _lickerStartupPosition = _lickerGameObject.transform.localPosition;

        _abs01StartScale = _abs01GameObject.transform.localScale;
        _abs02StartScale = _abs02GameObject.transform.localScale;
    }

    public override void Blink()
    {
        var abs01EndScale = _abs01StartScale - new Vector3(0.02f, 0.02f, 0.02f);
        var abs01ForwardTween = _abs01GameObject.transform.DOScale(abs01EndScale, 0.25f);
        var abs01BackwardTween = _abs01GameObject.transform.DOScale(_abs01StartScale, 0.25f);
        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardTween);
        
        var abs02EndScale = _abs02StartScale - new Vector3(0.02f, 0.02f, 0.02f);
        var abs02ForwardTween = _abs02GameObject.transform.DOScale(abs02EndScale, 0.25f);
        var abs02BackwardTween = _abs02GameObject.transform.DOScale(_abs02StartScale, 0.25f);
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardTween);        
    }

    private void OnRhythmItemCaptured()
    {
        float targetAbsPositionX;
        
        if (_currentTargetAbsIndex == 0)
        {
            targetAbsPositionX = _lickerLeftX;
        }
        else
        {
            targetAbsPositionX = _lickerRightX;
        }
        
        var lickerForwardTween = _lickerGameObject.transform.DOLocalMoveX(targetAbsPositionX, 0.1f);
        var lickerBackwardsTween = _lickerGameObject.transform.DOLocalMoveX(_lickerStartupPosition.x, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
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
        var abs01ForwardTween = _abs01GameObject.transform.DOLocalMoveX(_abs01X, 0.1f);
        var abs01BackwardsTween = _abs01GameObject.transform.DOLocalMoveX(_abs01StartupPosition.x, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        var abs02ForwardTween = _abs02GameObject.transform.DOLocalMoveX(_abs02X, 0.1f);
        var abs02BackwardsTween = _abs02GameObject.transform.DOLocalMoveX(_abs02StartupPosition.x, 0.1f);
        
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardsTween);

        var lickerScaleForward = _lickerGameObject.transform.DOScaleZ(0.002f, 0.1f);
        var lickerScaleBackward = _lickerGameObject.transform.DOScaleZ(0.01f, 0.1f);
        
        DOTween.Sequence().Append(lickerScaleForward).Append(lickerScaleBackward);
    }
}
