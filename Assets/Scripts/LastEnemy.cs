using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEnemy : MonoBehaviour
{
    private GameObject _enemy;
    private AudioClip _battleTheme;

    public void SetMemory(GameObject enemy, AudioClip theme)
    {
        _enemy = enemy;
        _battleTheme = theme;
    }

    public (GameObject, AudioClip) GetMemory()
    {
        return (_enemy, _battleTheme);
    }
}
