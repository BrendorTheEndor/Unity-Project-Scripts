using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    float currentSpeed = 1f;
    GameObject currentTarget;

    private void Awake() {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    private void OnDestroy() {

        LevelController levelController = FindObjectOfType<LevelController>();

        if(levelController != null) {
            levelController.AttackerKilled();
        }
    }

    void Update() {
        Move();
        UpdateAnimationState();
    }

    private void UpdateAnimationState() {
        if(!currentTarget) {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }

    private void Move() {
        transform.Translate(Vector2.left * Time.deltaTime * currentSpeed);
    }

    public void SetMovementSpeed(float speed) { currentSpeed = speed; }

    public void Attack(GameObject target) {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void StrikeCurrentTarget(float damage) {
        if(!currentTarget) { return; }
        Health health = currentTarget.GetComponent<Health>();

        if(health) {
            health.DealDamage(damage);
        }
    }
}
