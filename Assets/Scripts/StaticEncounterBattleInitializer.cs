using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticEncounterBattleInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _unit;
    [SerializeField] private AudioClip _battleTheme;
    private PlayerController _playerController;
    private LastEnemy _lastEnemyManager;
    private bool _playerNearby;
    private bool _battleIsActive;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _lastEnemyManager = FindObjectOfType<LastEnemy>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C) && _playerNearby && !_battleIsActive)
        {
            _playerController.enabled = false;
            _map.SetActive(false);
            Scene scene = SceneManager.GetActiveScene();
            _lastEnemyManager.SetMemory(_unit, _battleTheme);
            SceneManager.LoadScene("battleScene", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(scene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerNearby = false;
        }
    }
}
