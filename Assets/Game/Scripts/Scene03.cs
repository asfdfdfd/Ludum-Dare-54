using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scene03 : Scene
{
    [SerializeField] private GameObject _monkeyPlate01GameObject;
    [SerializeField] private GameObject _monkeyPlate02GameObject;
    [SerializeField] private GameObject _characterGameObject;
    [SerializeField] private GameObject _monkeyGameObject;
    
    [SerializeField] private Vector3 _absLeftSuccess;
    [SerializeField] private Vector3 _absRightSuccess;
    
    [SerializeField] private Vector3 _absLeftFail;
    [SerializeField] private Vector3 _absRightFail;
    
    private RhythmItemTrigger _rhythmItemTrigger;

    private Vector3 _abs01StartupPosition;
    private Vector3 _abs02StartupPosition;

    private int _currentTargetAbsIndex = 0;

    private Vector3 _monkeyStartScale;

    private void Start()
    {
        _rhythmItemTrigger = GameObject.FindWithTag("RhythmItemTrigger").GetComponent<RhythmItemTrigger>();

        _rhythmItemTrigger.OnRhythmItemCaptured.AddListener(OnRhythmItemCaptured);
        _rhythmItemTrigger.OnRhythmItemMissed.AddListener(OnRhythmMissed);

        _abs01StartupPosition = _monkeyPlate01GameObject.transform.localPosition;
        _abs02StartupPosition = _monkeyPlate02GameObject.transform.localPosition;

        _monkeyStartScale = _monkeyGameObject.transform.localScale;
    }

    public override void Blink()
    {
        var abs01EndScale = _monkeyStartScale - new Vector3(0.003f, 0.003f, 0.003f);
        var abs01ForwardTween = _monkeyGameObject.transform.DOScale(abs01EndScale, 0.25f);
        var abs01BackwardTween = _monkeyGameObject.transform.DOScale(_monkeyStartScale, 0.25f);
        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardTween);
    }

    private void OnRhythmItemCaptured()
    {
        var lickerForwardTween = _characterGameObject.transform.DOScaleY(0.6f, 0.1f);
        var lickerBackwardsTween = _characterGameObject.transform.DOScaleY(1.0f, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
        var abs01ForwardTween = _monkeyPlate01GameObject.transform.DOLocalMove(_absLeftSuccess, 0.1f);
        var abs01BackwardsTween = _monkeyPlate01GameObject.transform.DOLocalMove(_abs01StartupPosition, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        var abs02ForwardTween = _monkeyPlate02GameObject.transform.DOLocalMove(_absRightSuccess, 0.1f);
        var abs02BackwardsTween = _monkeyPlate02GameObject.transform.DOLocalMove(_abs02StartupPosition, 0.1f);
        
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardsTween);        
    }

    private void OnRhythmMissed()
    {
        var lickerForwardTween = _characterGameObject.transform.DOScaleX(0.6f, 0.1f);
        var lickerBackwardsTween = _characterGameObject.transform.DOScaleX(1.0f, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
        var abs01ForwardTween = _monkeyPlate01GameObject.transform.DOLocalMove(_absLeftFail, 0.1f);
        var abs01BackwardsTween = _monkeyPlate01GameObject.transform.DOLocalMove(_abs01StartupPosition, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        var abs02ForwardTween = _monkeyPlate02GameObject.transform.DOLocalMove( _absRightFail, 0.1f);
        var abs02BackwardsTween = _monkeyPlate02GameObject.transform.DOLocalMove(_abs02StartupPosition, 0.1f);
        
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardsTween);
    }
}
