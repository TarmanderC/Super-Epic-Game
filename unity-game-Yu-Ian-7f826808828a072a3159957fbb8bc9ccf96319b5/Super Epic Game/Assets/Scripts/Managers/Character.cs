using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private BattleManager bm;
    public String characterName;
    public int maxHealth = 100;
    public int currentHealth;
    public int TU = 0;
    public bool isPlayer;
    private Transform attackButtonContainer;

    public List<Attack> availableAttacks;
    public String currentAttack = "";
    public int selectedEnemy;

    void Awake() {
        bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    public void InitializeCharacter() {
        currentHealth = maxHealth;
    }

    public void StartTurn() {
        addButtons();

        currentAttack = "";
    }

    public void addButtons() {
        attackButtonContainer = GameObject.Find("ActionButtons").GetComponent<Transform>();

        for (int i = 0; i < availableAttacks.Count; i++) {
            GameObject button = Instantiate(availableAttacks[i].attackButton);
            button.transform.SetParent(attackButtonContainer, false);
        }
    }

    public void setCurrentAttack(String name) {
        currentAttack = name;

        Debug.Log("New change to attack! " + currentAttack);
    }

    public void setCurrentEnemy(int index) {
        selectedEnemy = index;

        if (currentAttack.Equals("basic")) {
            Attack(bm.enemies[selectedEnemy].GetComponent<Character>());
        } else if (currentAttack.Equals("skill")) {
            UseSkill(bm.enemies[selectedEnemy].GetComponent<Character>());
        }
    }

    public void cancelAttack() {
        setCurrentAttack("");
    }

    public void Attack(Character target) {
        int damage = UnityEngine.Random.Range(10, 20);
        target.TakeDamage(damage);
        Debug.Log($"{characterName} attacks {target.characterName} for {damage} damage!");
        selectedEnemy = -1;
    }

    public void UseSkill(Character target) {
        int damage = UnityEngine.Random.Range(20, 30);
        target.TakeDamage(damage);
        Debug.Log($"{characterName} uses skill on {target.characterName} for {damage} damage!");
        selectedEnemy = -1;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;

        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log($"{characterName} has been defeated!");
        } else {
            Debug.Log($"{characterName} now has {currentHealth}/{maxHealth} HP remaining.");
        }
    }

    public bool IsAlive() {
        return currentHealth > 0;
    }

}
