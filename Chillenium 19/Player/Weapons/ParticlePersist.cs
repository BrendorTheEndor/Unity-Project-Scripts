using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePersist : MonoBehaviour {

    public void ParentAboutToBeDestroyed() {
        Destroy(gameObject, 2f);
        transform.parent = null;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
