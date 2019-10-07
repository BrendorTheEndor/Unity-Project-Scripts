using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTerrrainGenerator : MonoBehaviour {

    [SerializeField] List<GameObject> tilesToPaint;
    [SerializeField] int heightOfGrid;
    [SerializeField] int widthOfGrid;



    private void Awake() {
        for(int i = 1; i <= heightOfGrid; i++) {
            for(int j = 1; j <= widthOfGrid; j++) {
                GameObject tileCreated = Instantiate(tilesToPaint[UnityEngine.Random.RandomRange(0, tilesToPaint.Count)], transform);
                tileCreated.transform.position = new Vector3(transform.position.x + (2 * j - 1), 0, transform.position.z + (2 * i - 1));
                tileCreated.isStatic = true;
            }
        }

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int k = 0;
        while(k < meshFilters.Length) {
            meshFilters[k].gameObject.GetComponent<MeshRenderer>().enabled = false;
            combine[k].mesh = meshFilters[k].sharedMesh;
            combine[k].transform = meshFilters[k].transform.localToWorldMatrix;
            meshFilters[k].gameObject.GetComponent<MeshRenderer>().enabled = false;

            k++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, false);
        transform.gameObject.SetActive(true);
    }
}
