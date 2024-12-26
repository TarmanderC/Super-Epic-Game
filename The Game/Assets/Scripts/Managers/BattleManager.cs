using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] enemies;
    private bool isBattleActive = false;
    private Transform playerTransform;
    public Transform cameraTransform;

    private KnightMovement playerMovement;


    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;
    public Vector3[] playerPositions;
    public Vector3[] enemyPositions;

    public List<GameObject> queue;

    public GameObject actionPanel;

    // UI Control


    void Awake() {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerMovement = GameObject.Find("Player").GetComponent<KnightMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform.position = new Vector3(0,0,-10);

        actionPanel.SetActive(false);
    }

    public void StartBattle(GameObject[] players, GameObject[] enemies) {
        
        if (!isBattleActive) {
            actionPanel.SetActive(true);
            playerMovement.canMove = false;

            isBattleActive = true;
            cameraTransform.position = new Vector3(-25,-26,-10);

            this.players = players;
            this.enemies = enemies;

            InitializeUnits(players, enemies);
            addToQueue(players, enemies);

            printQueue();
            sortQueue();

            // change this
            Character currentCharacter = players[0].GetComponent<Character>();
            currentCharacter.StartTurn();
        }
    }



    private void printQueue() {
        String line = "";
        for (int i = 0; i < queue.Count; i++) {
            line += queue[i].GetComponent<Character>().characterName + "  " + queue[i].GetComponent<Character>().TU + ", ";
        }

        Debug.Log(line);
    }

    private void sortQueue() {
        // Sort the list by the TU variable in the Character component
        queue.Sort((a, b) =>
        {
            int tuA = a.GetComponent<Character>().TU;
            int tuB = b.GetComponent<Character>().TU;
            return tuA.CompareTo(tuB); // Compare TU values
        });

        // Log the sorted order
        Debug.Log("Sorted by TU:");
        foreach (var character in queue)
        {
            Debug.Log($"{character.GetComponent<Character>().characterName} - TU: {character.GetComponent<Character>().TU}");
        }
    }

    private void addToQueue(GameObject[] players, GameObject[] enemies) {
        for (int i = 0; i < players.Length; i++) {
            if (players[i] != null)
            {
                queue.Add(players[i]);
            }
        }
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i] != null)
            {
                queue.Add(enemies[i]);
            }
        }
    }

    private void InitializeUnits(GameObject[] players, GameObject[] enemies) {
        for (int i = 0; i < players.Length; i++) {
            if (players[i] != null)
            {
                GameObject player = Instantiate(players[i], playerPositions[i], Quaternion.identity);
                player.transform.SetParent(playerSpawnPoint, false);
            }
        }

        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i] != null)
            {
                GameObject enemy = Instantiate(enemies[i], enemyPositions[i], Quaternion.identity);
                enemy.transform.SetParent(enemySpawnPoint, false);
            }
        }
    }

    public void EndBattle() {
        isBattleActive = false;
        playerMovement.canMove = true;
        cameraTransform.position = playerTransform.position;
        
        Array.Clear(players, 0, players.Length);
        Array.Clear(enemies, 0, players.Length);
        
        Debug.Log("Battle ended!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
