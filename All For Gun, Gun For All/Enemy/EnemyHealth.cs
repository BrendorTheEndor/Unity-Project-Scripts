using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public enum enemyType { big, small };

    [SerializeField] int hitPoints = 100;
    [SerializeField] enemyType myEnemyType;
    [SerializeField] int scoreValue = 10;

    bool isDead = false;

    public void TakeDamage(int damageToTake) {
        Debug.Log("Enemy takes " + damageToTake + " damage");
        hitPoints -= damageToTake;
        if(hitPoints <= 0) {
            Die();
        }
    }

    private void Die() {
        if(isDead) { return; }
        Debug.Log("Enemy died");

        GameState gameState = GameState.instanceOfGameState;
        if(myEnemyType == enemyType.small) {
            gameState.AddRailgunDamage();
        }
        gameState.AddScore(scoreValue);

        isDead = true;
        GetComponentInChildren<Animator>().SetTrigger("Die");
        GetComponent<EnemyAI>().EnemyIsDead();
    }

    public enemyType GetEnemyType() { return myEnemyType; }
}
