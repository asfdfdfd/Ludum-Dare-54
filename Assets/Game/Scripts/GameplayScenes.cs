using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayScenes : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gamePlayScenesPrefabs;

    private int _currentGameplaySceneIndex = -1;

    private GameObject _currentGamePlayScene = null;
    
    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        _currentGameplaySceneIndex++;

        if (_currentGameplaySceneIndex == _gamePlayScenesPrefabs.Count)
        {
            _currentGameplaySceneIndex = 0;
        }

        if (_currentGamePlayScene != null)
        {
            yield return _currentGamePlayScene.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear).WaitForCompletion();
        } 
        
        Destroy(_currentGamePlayScene);

        _currentGamePlayScene = Instantiate(_gamePlayScenesPrefabs[_currentGameplaySceneIndex], gameObject.transform);

        yield return null;
    }

    public void Blink()
    {
        if (_currentGamePlayScene != null)
        {
            var scene = _currentGamePlayScene.GetComponent<Scene>();

            if (scene != null)
            {
                scene.Blink();
            }
        }
    }
}
