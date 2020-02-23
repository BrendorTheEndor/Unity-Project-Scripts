using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class SetMaterial : MonoBehaviour {

    [SerializeField] List<Material> materialsList;
    ProBuilderMesh thisMesh;

    private void Start() {
        thisMesh = GetComponent<ProBuilderMesh>();
        foreach(Face certainFace in thisMesh.faces) {
            //Debug.Log("This face: " + certainFace.ToString() + " before");
            List<Face> tempList = new List<Face>();
            tempList.Add(certainFace);
            thisMesh.SetMaterial(tempList, materialsList[UnityEngine.Random.Range(0, materialsList.Count - 1)]);
            //Debug.Log("This face: " + certainFace.ToString() + " after");
        }
        thisMesh.ToMesh();
        thisMesh.Refresh();
    }

}

