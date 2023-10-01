using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Game.Scripts;
using UnityEngine;

public class RhythmItemGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _rhythmItemPrefab;

    [SerializeField] private float _playerPrepareSec = 3.0f;

    [SerializeField] private TextAsset _musicLayout;
    
    private readonly float _bpm = 120;
    private readonly float _partsInBeat = 2;

    private float _msecInBeat;
    private float _msecInPart;
    private float _secInPart;
    
    private List<RhythmItemRecord> _rhythmItemRecords = new();

    private AudioSource _musicAudioSource;

    private AugmentedTimer _augmentedTimer;

    private RhythmItemTrigger _rhythmItemTrigger;

    private GameplayScenes _gameplayScenes;
    
    private void Start()
    {
        _rhythmItemTrigger = GameObject.FindWithTag("RhythmItemTrigger").GetComponent<RhythmItemTrigger>();
        
        _musicAudioSource = GameObject.FindWithTag("MusicPlayer").GetComponent<AudioSource>();

        _augmentedTimer = GameObject.FindWithTag("AugmentedTimer").GetComponent<AugmentedTimer>();

        _gameplayScenes = GameObject.FindWithTag("GameplayScenes").GetComponent<GameplayScenes>();
        
        var msecInMinute = 60 * 1000;
        
        _msecInBeat = msecInMinute / _bpm;
        _msecInPart = _msecInBeat / _partsInBeat;
        _secInPart = _msecInPart / 1000.0f;
        
        LoadRhythmItemRecords();
        
        StartCoroutine(GeneratorCoroutine());

        _gameplayScenes.LoadNextScene();
    }

    private void LoadRhythmItemRecords()
    {
        var csvParser = new CsvParser(new StringReader(_musicLayout.text), CultureInfo.InvariantCulture);
        while (csvParser.Read())
        {
            var row = csvParser.Record;
            if (row != null)
            {
                if (row[0].Length > 0)
                {
                    _rhythmItemRecords.Add(new RhythmItemRecord(true));
                }
                else
                {
                    _rhythmItemRecords.Add(new RhythmItemRecord(false));
                }
            }
        }
    }

    private IEnumerator GeneratorCoroutine()
    {
        var travelStartTime =_augmentedTimer.GetAugmentedTime() + _playerPrepareSec;
        
        _musicAudioSource.PlayScheduled(travelStartTime);
        
        foreach (var rhythmItemRecord in _rhythmItemRecords)
        {
            var rhythmItemGameObject = Instantiate(_rhythmItemPrefab, transform);
            var rhythmItem = rhythmItemGameObject.GetComponent<RhythmItem>();

            if (rhythmItemRecord.hasBeat)
            {
                // rhythmItem.Launch(travelDurationSec);
                rhythmItem.Launch(_augmentedTimer.GetAugmentedTime(), travelStartTime, _rhythmItemTrigger.gameObject.transform.position);                
            }

            travelStartTime += _secInPart;

            yield return new WaitForSeconds(_secInPart);            
        }
    }
}
