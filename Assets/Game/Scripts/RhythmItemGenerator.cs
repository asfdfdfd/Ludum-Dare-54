using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmItemGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _rhythmItemPrefab;

    [SerializeField] private float _playerPrepareSec = 3.0f;

    private void Start()
    {
        StartCoroutine(GeneratorCoroutine());
    }

    private IEnumerator GeneratorCoroutine()
    {
        while (true)
        {
            var rhythmItemGameObject = Instantiate(_rhythmItemPrefab, transform);
            var rhythmItem = rhythmItemGameObject.GetComponent<RhythmItem>();

            rhythmItem.Launch();
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
