using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    private int _health;

    [SerializeField] private GameObject _prefabVfx;
    
    private void Start()
    {
        _health = transform.childCount;
    }

    public void Hit()
    {
        var gameObjectToDestroy = transform.GetChild(_health - 1).gameObject;
        var gameObjectToDestroyPosition = gameObjectToDestroy.transform.position;
        
        Destroy(gameObjectToDestroy);
        
        var gameObjectVfx = Instantiate(_prefabVfx);
        gameObjectVfx.transform.position = gameObjectToDestroyPosition;

        _health--;
        
        if (_health == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
