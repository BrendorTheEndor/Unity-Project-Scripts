using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    //enum enemyType { big, small };

    //[SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    //[SerializeField] int damageToDeal = 20;

    float distanceToTarget = Mathf.Infinity;
    bool enemyTriggered = false;

    Transform target;
    NavMeshAgent myNavMeshAgent;
    Animator myAnimator;
    //EnemyHealth enemyHealth;
    bool isDead = false;

    // string refs for animator
    const string CHASE_TRIGGER = "beginChase";
    const string IDLE_TRIGGER = "deaggro";
    const string ATTACK_BOOL = "isAttacking";

    // Start is called before the first frame update
    void Start() {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponentInChildren<Animator>();
        //enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update() {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        if(isDead) { return; }
        PursuePlayer();
    }

    private void PursuePlayer() {

        FaceTaget();

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(distanceToTarget >= myNavMeshAgent.stoppingDistance) {
            ChaseTarget();
        }
        if(distanceToTarget <= myNavMeshAgent.stoppingDistance) {
            AttackTarget();
        }
    }

    // Changes the rotation of the enemy to face the target
    private void FaceTaget() {
        Vector3 direction = (target.position - transform.position).normalized; // Get the direction to face
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Find the rotation to look in
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed); // Gradually turn to that rotation
    }

    private void ChaseTarget() {
        myAnimator.SetBool(ATTACK_BOOL, false);
        //myAnimator.SetTrigger(CHASE_TRIGGER);
        myNavMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget() { // TODO use animation event to attack the player
        Debug.Log("Player attacked");
        myAnimator.SetBool(ATTACK_BOOL, true);
        //target.GetComponent<PlayerHealth>().TakeDamage(damageToDeal);
    }

    public void EnemyIsDead() {
        isDead = true;
        if(GetComponent<EnemyHealth>().GetEnemyType() == EnemyHealth.enemyType.big) {
            Destroy(gameObject, 3f);
        }
        else {
            Destroy(gameObject);
        }
    }
}
