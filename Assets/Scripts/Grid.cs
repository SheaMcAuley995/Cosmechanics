using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField] Vector3 gizmosCubeSize;
    [SerializeField] Vector3 gridSize;

    List<Node> nodes;


    private void OnDrawGizmosSelected()
    {
        for(int i = 0; i < gridSize.z; i++)
        {
            for(int j = 0; j < gridSize.x; j++)
            {
                //Gizmos.DrawWireCube(new Vector3(i,0,j) + transform.position, (gizmosCubeSize));
                Node newNode = new Node();
                newNode.transform.position = new Vector3(i, 0, j) + transform.position;
                nodes.Add(newNode);
            }
        }
    }
}
