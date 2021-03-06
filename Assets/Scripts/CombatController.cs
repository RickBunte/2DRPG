﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CombatState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class CombatController : MonoBehaviour
{
    public CombatState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    [SerializeField] private Image _enemyBattleSprite;

    PlayerStatsController playerUnit;
    EnemyStatController enemyUnit;

    public Text healthText;
    public Text energyTokenText;
    public GameObject dialoguePanel;

    public GameObject attackButton;
    public GameObject restButton;

    private AudioSource _audioSource;
    private LastEnemy _information;
    private GameObject _player;

    private int turn;
    private bool lowHealthLineIsSaid;

    // Start is called before the first frame update
    void Awake()
    {
        state = CombatState.START;
        turn = 1;
        lowHealthLineIsSaid = false;
        _audioSource = FindObjectOfType<Persistant>().GetComponent<AudioSource>();
        _information = FindObjectOfType<LastEnemy>();
        SetEnemyInfo(_information.GetMemory().Item1, _information.GetMemory().Item2);
        StartCoroutine(PrepareCombat());
        _player = FindObjectOfType<PlayerController>().gameObject;
        _player.SetActive(false);
    }

    public void SetEnemyInfo(EnemyStatController enemyStats, AudioClip _battleTheme)
    {
        enemyUnit = enemyStats;
        enemyUnit.ResetHealth();
        _enemyBattleSprite.sprite = enemyUnit.GetBattleSprite();
        _audioSource.Stop();
        _audioSource.clip = _battleTheme;
        _audioSource.Play();
    }

    void ToggleButtons(bool boolean)
    {
        attackButton.SetActive(boolean);
        restButton.SetActive(boolean);
    }

    IEnumerator PrepareCombat()
    {
        ToggleButtons(false);
        GameObject playerGameObject = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGameObject.GetComponent<PlayerStatsController>();

        healthText.text = "Health: " + playerUnit.currentHealth + "/" + playerUnit.maxHealth;
        energyTokenText.text = "Energy: " + playerUnit.currentEnergyTokens + "/" + playerUnit.maxEnergyTokens;
        dialoguePanel.GetComponentInChildren<Text>().text = enemyUnit.enemyEncounterLine;

        yield return new WaitForSeconds(2f);

        state = CombatState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        ToggleButtons(true);
        if (!dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(true);
        }
        bool playerHealthLevelCheck = (float)playerUnit.currentHealth / playerUnit.maxHealth <= 0.25f;
        if (playerHealthLevelCheck)
        {
            dialoguePanel.GetComponentInChildren<Text>().text = enemyUnit.playerLowHealthLine;
        }
        else
        {
            dialoguePanel.GetComponentInChildren<Text>().text = enemyUnit.playerTurnLine;
        }   
    }

    IEnumerator PlayerRest()
    {
        ToggleButtons(false);
        playerUnit.Rest();
        healthText.text = "Health: " + playerUnit.currentHealth + "/" + playerUnit.maxHealth;
        energyTokenText.text = "Energy: " + playerUnit.currentEnergyTokens + "/" + playerUnit.maxEnergyTokens;
        dialoguePanel.GetComponentInChildren<Text>().text = "You take a rest!";


        attackButton.SetActive(false);
        restButton.SetActive(false);
        yield return new WaitForSeconds(2f);

        state = CombatState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerAttack()
    {
        bool playerEnergyTokenCheck = playerUnit.currentEnergyTokens > 0;
        if (playerEnergyTokenCheck)
        {
            playerUnit.ConsumeEnergyToken(1);
            dialoguePanel.GetComponentInChildren<Text>().text = "You strike " + enemyUnit.GetName();
        }
        else
        {
            dialoguePanel.GetComponentInChildren<Text>().text = "You are out of energy!\nYou should rest a little.";
            yield return new WaitForSeconds(2f);
            PlayerTurn();
            yield break;
        }
        bool enemyDefeated = enemyUnit.TakeDamage(playerUnit.power);
        healthText.text = "Health: " + playerUnit.currentHealth + "/" + playerUnit.maxHealth;
        energyTokenText.text = "Energy: " + playerUnit.currentEnergyTokens + "/" + playerUnit.maxEnergyTokens;
        attackButton.SetActive(false);
        restButton.SetActive(false);
        yield return new WaitForSeconds(2f);
        if (enemyDefeated)
        {
            state = CombatState.WON;
            EndBattle();
        }
        else
        {
            state = CombatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            yield break;
        }
    }

    public void OnPlayerRest()
    {
        if (state != CombatState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerRest());
    }

    public void OnPlayerAttack()
    {
        if(state != CombatState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator EnemyTurn()
    {
        bool firstTurn = turn == 1;
        bool enemyHealthCheck = enemyUnit.GetHealthPercentage() <= 0.25f;
        if (firstTurn)
        {
            dialoguePanel.GetComponentInChildren<Text>().text = enemyUnit.enemyFirstTurnLine;
        }
        if (enemyHealthCheck && !lowHealthLineIsSaid)
        {
            dialoguePanel.GetComponentInChildren<Text>().text = enemyUnit.enemyLowHealthLine;
            lowHealthLineIsSaid = true;
        }
        yield return new WaitForSeconds(2f);
        dialoguePanel.SetActive(false);
        bool playerDefeated = playerUnit.TakeDamage(enemyUnit.GetDamage());
        healthText.text = "Health: " + playerUnit.currentHealth + "/" + playerUnit.maxHealth;
        yield return new WaitForSeconds(2f);
        if (playerDefeated)
        {
            state = CombatState.LOST;
            EndBattle();
        }
        else
        {
            state = CombatState.PLAYERTURN;
            turn++;
            PlayerTurn();
            yield break;
        }

    }

    void EndBattle()
    {
        if(state == CombatState.WON)
        {
            dialoguePanel.GetComponentInChildren<Text>().text = enemyUnit.enemyDefeatLine;
            dialoguePanel.GetComponentInChildren<Text>().text = "You have won!";
        }
        else
        {
            dialoguePanel.GetComponentInChildren<Text>().text = "You have lost!";
        }
    }
}
