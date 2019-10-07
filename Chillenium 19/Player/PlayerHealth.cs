using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] int hitPoints = 100;
    [SerializeField] Slider playerHealthBar;

    int maxHealth;

    private void Start() {
        maxHealth = hitPoints;
    }

    public void TakeDamage(int damageToTake) {
        hitPoints -= damageToTake;

        playerHealthBar.value = (float)hitPoints / maxHealth;

        if(hitPoints <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Player died");
        GetComponent<PlayerController>().PlayerHasDied();
        GetComponentInChildren<Animator>().SetTrigger("Die");
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad() {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }
}
