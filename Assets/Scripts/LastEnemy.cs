using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEnemy : MonoBehaviour
{
    #region Properties
    private EnemyStatController _enemy;
    private AudioClip _battleTheme;
    #endregion

    #region Methods
    public void SetMemory(EnemyStatController enemy, AudioClip theme)
    {
        _enemy = enemy;
        _battleTheme = theme;
    }

    public (EnemyStatController, AudioClip) GetMemory()
    {
        return (_enemy, _battleTheme);
    }
    #endregion
}
