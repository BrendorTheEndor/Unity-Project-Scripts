using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    const string PROJECTILE_PARENT_NAME = "Projectiles";

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject gun;
    AttackerSpawner myLaneSpawner;
    Animator animator;
    GameObject projectileParent;

    private void Start() {
        animator = GetComponent<Animator>();
        SetLaneSpawner();
        CreateProjectileParent();
    }

    private void Update() {
        if(IsAttackerInLane()) {
            animator.SetBool("isAttacking", true);
        }
        else {
            animator.SetBool("isAttacking", false);
        }
    }

    private void CreateProjectileParent() {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if(!projectileParent) {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    // Sets the spawner we're looking at for this particular lane
    private void SetLaneSpawner() {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach(AttackerSpawner spawner in spawners) {
            // Basically, if the difference in y position is less than the smallest number we got, then it's in the lane
            bool IsCloseEnough = (Mathf.Abs((spawner.transform.position.y + 1) - transform.position.y) <= Mathf.Epsilon);
            // The + 1 is there because otherwise the defenders were off by 1 on what their lane was

            if(IsCloseEnough) {
                myLaneSpawner = spawner;
                //Debug.Log(myLaneSpawner.name);
            }
        }
    }

    private bool IsAttackerInLane() {
        if(myLaneSpawner.transform.childCount <= 0) {
            return false;
        }
        return true;
    }

    public void Fire() {
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, transform.rotation);
        newProjectile.transform.parent = projectileParent.transform;
    }

}
