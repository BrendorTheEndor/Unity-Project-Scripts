using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    // The distance from the player the enemy needs to be to activate AI
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float stunDuration = 1f;
    [SerializeField] float launchDuration = .2f;
    [SerializeField] bool enemyTriggered = false; // Serialized for fun mostly
    [SerializeField] CapsuleCollider feetCollider;

    float distanceToTarget = Mathf.Infinity;
    bool canMove = true;
    bool hasLaunched = false;
    bool isAttacking = false;

    Transform target;
    NavMeshAgent myNavMeshAgent;
    Animator myAnimator;
    Coroutine waitCoroutine = null;
    //EnemyAttack attackScript;

    const string ATTACK_TRIGGER = "Attack";
    const string TRIGGERED_BOOL = "AITriggered";

    // Start is called before the first frame update
    void Start() {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerMelee>().transform;
        //attackScript = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update() {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget <= chaseRange) {
            enemyTriggered = true;
        }
        if(enemyTriggered) {
            PursuePlayer();
        }
    }

    public void OnDamageTaken() {
        TriggerEnemy();
    }

    public void TriggerEnemy() {
        enemyTriggered = true;
        myAnimator.SetBool(TRIGGERED_BOOL, true);
    }

    private void PursuePlayer() {

        FaceTaget();

        if(distanceToTarget >= myNavMeshAgent.stoppingDistance) {
            ChaseTarget();
        }
        if(distanceToTarget <= myNavMeshAgent.stoppingDistance && !isAttacking) {
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
        //myAnimator.SetTrigger(CHASE_TRIGGER);
        if(!canMove) { return; }
        myNavMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget() {
        isAttacking = true;
        myAnimator.SetTrigger(ATTACK_TRIGGER);
    }

    // Used to show the chase range in the editor
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    //Used to re-enable AI when landing, there's trigger collider for "feet" on the enemy
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Enemy is touching " + other.transform.name);
        if(other.transform.tag == "Ground" && hasLaunched) {
            Debug.Log("Enemy is touching ground");

            GetComponent<Rigidbody>().isKinematic = true;

            waitCoroutine = StartCoroutine(WaitAndMove());
            hasLaunched = false;
            feetCollider.enabled = false;
            //attackScript.EnableAttack();
        }
    }

    public void DisableMovement() {
        canMove = false;
        feetCollider.enabled = true;
        //attackScript.DisableAttack();
        GetComponent<Health>().SetCanExplode(true);

        GetComponent<Rigidbody>().isKinematic = false;

        StartCoroutine(LaunchTimer());
        if(waitCoroutine != null) {
            StopCoroutine(waitCoroutine);
        }
    }

    public void AttackFinished() {
        isAttacking = false;
    }

    IEnumerator LaunchTimer() {
        yield return new WaitForSeconds(launchDuration);
        hasLaunched = true;
    }

    IEnumerator WaitAndMove() {
        yield return new WaitForSeconds(stunDuration);
        myNavMeshAgent.enabled = true;
        canMove = true;
        GetComponent<Health>().SetCanExplode(false);
    }

}
