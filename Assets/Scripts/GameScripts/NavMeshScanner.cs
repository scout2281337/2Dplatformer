using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; 

public class NavMeshScanner : MonoBehaviour
{
    void Start()
    {

        StartCoroutine(ScanNavMesh());
    }
    IEnumerator ScanNavMesh() 
    {
        yield return new WaitForSeconds(3f);

        if (AstarPath.active != null)
        {

            AstarPath.active.Scan();
            Debug.Log("NavMesh was scanned at the start of the scene.");
        }
        else
        {
            Debug.LogError("AstarPath is not active in the scene!");
        }


    }


}



