using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scene03 : MonoBehaviour
{
    [SerializeField] private GameObject _monkeyPlate01GameObject;
    [SerializeField] private GameObject _monkeyPlate02GameObject;
    [SerializeField] private GameObject _characterGameObject;

    [SerializeField] private float _lickerLeftDelta;
    [SerializeField] private float _lickerRightDelta;
    
    [SerializeField] private float _abs01Delta;
    [SerializeField] private float _abs02Delta;
    
    [SerializeField] private float _abs01DeltaFail;
    [SerializeField] private float _abs02DeltaFail;
    
    private RhythmItemTrigger _rhythmItemTrigger;

    private Vector3 _abs01StartupPosition;
    private Vector3 _abs02StartupPosition;

    private Vector3 _characterStartupPosition;

    private int _currentTargetAbsIndex = 0;

    private void Start()
    {
        _rhythmItemTrigger = GameObject.FindWithTag("RhythmItemTrigger").GetComponent<RhythmItemTrigger>();

        _rhythmItemTrigger.OnRhythmItemCaptured.AddListener(OnRhythmItemCaptured);
        _rhythmItemTrigger.OnRhythmItemMissed.AddListener(OnRhythmMissed);

        _abs01StartupPosition = _monkeyPlate01GameObject.transform.position;
        _abs02StartupPosition = _monkeyPlate02GameObject.transform.position;

        _characterStartupPosition = _characterGameObject.transform.position;
    }

    private void OnRhythmItemCaptured()
    {
        var lickerForwardTween = _characterGameObject.transform.DOScaleY(0.6f, 0.1f);
        var lickerBackwardsTween = _characterGameObject.transform.DOScaleY(1.0f, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
        var abs01ForwardTween = _monkeyPlate01GameObject.transform.DOMoveX(_abs01StartupPosition.x + _abs01Delta, 0.1f);
        var abs01BackwardsTween = _monkeyPlate01GameObject.transform.DOMoveX(_abs01StartupPosition.x, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        var abs02ForwardTween = _monkeyPlate02GameObject.transform.DOMoveX(_abs02StartupPosition.x - _abs02Delta, 0.1f);
        var abs02BackwardsTween = _monkeyPlate02GameObject.transform.DOMoveX(_abs02StartupPosition.x, 0.1f);
        
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardsTween);        
    }

    private void OnRhythmMissed()
    {
        var lickerForwardTween = _characterGameObject.transform.DOScaleX(0.6f, 0.1f);
        var lickerBackwardsTween = _characterGameObject.transform.DOScaleX(1.0f, 0.1f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
        var abs01ForwardTween = _monkeyPlate01GameObject.transform.DOMoveX(_abs01StartupPosition.x + _abs01DeltaFail, 0.1f);
        var abs01BackwardsTween = _monkeyPlate01GameObject.transform.DOMoveX(_abs01StartupPosition.x, 0.1f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        var abs02ForwardTween = _monkeyPlate02GameObject.transform.DOMoveX(_abs02StartupPosition.x - _abs02DeltaFail, 0.1f);
        var abs02BackwardsTween = _monkeyPlate02GameObject.transform.DOMoveX(_abs02StartupPosition.x, 0.1f);
        
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardsTween);
    }
}
