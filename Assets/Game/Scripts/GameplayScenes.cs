using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScenes : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gamePlayScenesPrefabs;

    private int _currentGameplaySceneIndex = -1;
    
    public void LoadNextScene()
    {
        _currentGameplaySceneIndex++;

        if (_currentGameplaySceneIndex == _gamePlayScenesPrefabs.Count)
        {
            _currentGameplaySceneIndex = 0;
        }

        foreach (Transform childTransform in gameObject.transform)
        {
            Destroy(childTransform.gameObject);
        }

        var gamePlaySceneGameObject = Instantiate(_gamePlayScenesPrefabs[_currentGameplaySceneIndex], gameObject.transform);
    }
}
