using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public static Grid instance;

    public GameObject fireEffect;
    public LayerMask flamableMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    public Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    [SerializeField] bool GenerateGrid;
    [SerializeField] bool LightFire;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool flameable = (Physics.CheckSphere(worldPoint, nodeRadius, flamableMask));
                grid[x, y] = new Node(flameable, worldPoint, x, y);
            }
        }
        
        Debug.Log("Length of grid is " + grid.Length);
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }


    public void GenerateFire()
    {
        Node fireStartPosition = grid[Random.Range(0, gridSizeX), Random.Range(0, gridSizeY)];
        //Node fireStartPosition = grid[(int)ShipHealth.instance.attackLocation.x, (int)ShipHealth.instance.attackLocation.z];

        int safetyNet = 0;
        while(fireStartPosition.isFlamable != true)
        {
            fireStartPosition = grid[Random.Range(0, gridSizeX), Random.Range(0, gridSizeY)];
            //fireStartPosition = grid[(int)ShipHealth.instance.attackLocation.x, (int)ShipHealth.instance.attackLocation.z];
            safetyNet++;

            GameObject fire = Instantiate(fireEffect, fireStartPosition.worldPosition, Quaternion.Euler(-90f, 0f, 0f));
            Debug.Log("Fiyah");

            if (safetyNet > 50)
            {
                Debug.Log("#Nope");
                break;
            }
        }



        fireStartPosition.isFlamable = false;
    }


    private void OnDrawGizmos()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        if(GenerateGrid)
            CreateGrid();

        if(LightFire)
        {
            GenerateFire();
            LightFire = false;
        }


        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isFlamable) ? Color.white : Color.red;
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }

    }

}