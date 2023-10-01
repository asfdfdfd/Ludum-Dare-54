using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scene01 : MonoBehaviour
{
    [SerializeField] private GameObject _abs01GameObject;
    [SerializeField] private GameObject _abs02GameObject;
    [SerializeField] private GameObject _lickerGameObject;
    
    private RhythmItemTrigger _rhythmItemTrigger;

    private Vector3 _abs01StartupPosition;
    private Vector3 _abs02StartupPosition;

    private Vector3 _lickerStartupPosition;

    private List<GameObject> _abses = new();

    private int _currentTargetAbsIndex = 0;
    
    private void Start()
    {
        _rhythmItemTrigger = GameObject.FindWithTag("RhythmItemTrigger").GetComponent<RhythmItemTrigger>();

        _rhythmItemTrigger.OnRhythmItemCaptured.AddListener(OnRhythmItemCaptured);
        _rhythmItemTrigger.OnRhythmItemMissed.AddListener(OnRhythmMissed);

        _abs01StartupPosition = _abs01GameObject.transform.position;
        _abs02StartupPosition = _abs02GameObject.transform.position;

        _lickerStartupPosition = _lickerGameObject.transform.position;
        
        _abses.Add(_abs01GameObject);
        _abses.Add(_abs02GameObject);
    }

    private void OnRhythmItemCaptured()
    {
        var targetAbs = _abses[_currentTargetAbsIndex];
        
        var lickerForwardTween = _lickerGameObject.transform.DOMove(targetAbs.transform.position, 0.3f);
        var lickerBackwardsTween = _lickerGameObject.transform.DOMove(_lickerStartupPosition, 0.3f);

        DOTween.Sequence().Append(lickerForwardTween).Append(lickerBackwardsTween);
        
        SwitchToNextTargetAbs();
    }

    private void SwitchToNextTargetAbs()
    {
        _currentTargetAbsIndex++;
        
        if (_currentTargetAbsIndex == _abses.Count)
        {
            _currentTargetAbsIndex = 0;
        }
    }

    private void OnRhythmMissed()
    {
        var abs01ForwardTween = _abs01GameObject.transform.DOMove(_lickerGameObject.transform.position, 0.3f);
        var abs01BackwardsTween = _abs01GameObject.transform.DOMove(_abs01StartupPosition, 0.3f);

        DOTween.Sequence().Append(abs01ForwardTween).Append(abs01BackwardsTween);
        
        var abs02ForwardTween = _abs02GameObject.transform.DOMove(_lickerGameObject.transform.position, 0.3f);
        var abs02BackwardsTween = _abs02GameObject.transform.DOMove(_abs02StartupPosition, 0.3f);
        
        DOTween.Sequence().Append(abs02ForwardTween).Append(abs02BackwardsTween);
    }
}
