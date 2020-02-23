using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMelee : MonoBehaviour {

    [SerializeField] float forceStrength = 1f;
    [SerializeField] float upwardsFactor = 2f;

    Animator myAnimator;
    Camera firstPersonCamera;

    const string MELEE_TRIGGER = "Swing";

    // Start is called before the first frame update
    void Start() {
        myAnimator = GetComponent<Animator>();
        firstPersonCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(firstPersonCamera.transform.forward);
        if(Input.GetButtonDown("Fire2")) {
            //Debug.Log("Button pressed");
            myAnimator.SetTrigger(MELEE_TRIGGER);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Hit " + other.transform.name);
        Health enemyHealth = other.GetComponent<Health>();

        // TODO Might effect player?
        if(enemyHealth != null && enemyHealth.GetHealthType() == Health.HealthType.Enemy) {
            var enemyRigidbody = other.GetComponent<Rigidbody>();
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<EnemyAI>().DisableMovement();
            var forceToApply = (new Vector3(firstPersonCamera.transform.forward.x, upwardsFactor, firstPersonCamera.transform.forward.z)) * forceStrength;
            //enemyRigidbody.velocity = new Vector3(0, 0, 0);
            //enemyRigidbody.AddForce(forceToApply, ForceMode.Impulse);
            enemyRigidbody.velocity = forceToApply; // Changed to velocity from force as this is more consistent
        }
    }
}
