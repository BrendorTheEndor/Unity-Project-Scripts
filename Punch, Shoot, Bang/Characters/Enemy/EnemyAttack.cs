using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] GameObject attackEffect;
    [SerializeField] CapsuleCollider attackHitbox;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] float attackVolume = 1f;

    EnemyAI enemyAI;

    bool canAttack = true;

    private void Start() {
        attackHitbox.enabled = false;
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TriggerAttack() {
        if(!canAttack) { return; }
        attackEffect.GetComponent<ParticleSystem>().Play();
        AudioSource.PlayClipAtPoint(attackSFX, transform.position, attackVolume);
        attackHitbox.enabled = true;
    }

    public void EndAttack() {
        Debug.Log("attack has finished");
        attackHitbox.enabled = false;
        enemyAI.AttackFinished();
    }

    //public void DisableAttack() {
    //    canAttack = false;
    //    attackHitbox.enabled = false;
    //}

    //public void EnableAttack() {
    //    canAttack = true;
    //}
}
