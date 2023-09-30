using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RhythmItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        gameObject.transform.DOMoveX(0.0f, 12.0f).SetSpeedBased(true).SetEase(Ease.Linear);
    }
}
