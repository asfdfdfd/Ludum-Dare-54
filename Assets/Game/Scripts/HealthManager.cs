using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    private int _health;
    
    private void Start()
    {
        _health = transform.childCount;
    }

    public void Hit()
    {
        // Destroy(transform.GetChild(0).gameObject);

        _health--;
        
        if (_health == 0)
        {
            // SceneManager.LoadScene("GameOverScene");
        }
    }
}
