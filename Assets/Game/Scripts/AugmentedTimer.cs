using System;
using UnityEngine;

public class AugmentedTimer : MonoBehaviour
{
    private double _dspTimePrev = Double.MinValue;
    
    private double _augmentedClock = 0.0f;

    private void Awake()
    {
        _augmentedClock = AudioSettings.dspTime;
    }

    private void Update()
    {
        if (_dspTimePrev != AudioSettings.dspTime)
        {
            _dspTimePrev = AudioSettings.dspTime;
            _augmentedClock = _dspTimePrev;
        }
        else
        {
            _augmentedClock += Time.unscaledDeltaTime;
        }
    }

    public double GetAugmentedTime()
    {
        return _augmentedClock;
    }
}
