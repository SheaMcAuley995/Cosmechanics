using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public static Grid instance;

    public LayerMask flamableMask;
    public Vector3 gridWorldSize;
    public GameObject fireEffect;
    public float nodeRadius;
    public Node[,] grid;
    public GameObject[,] fireGrid;
    List<Node> fires = new List<Node>();

    float nodeDiameter;
    public int gridSizeX, gridSizeY;

    [Header("Fire Statistics")]
    public float fireStartPercentage;
    public float fireTimer;
    public AlertUI alertUI;

    [Header("Debug tools")]
    [SerializeField] bool GenerateGrid;
    [SerializeField] bool LightFire;
    [SerializeField] bool showGrid;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

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
                grid[x, y] = new Node(flameable, worldPoint, x, y, fireTimer, Instantiate(fireEffect, worldPoint, Quaternion.Euler(0f, 0f, 0f)));
                grid[x, y].fireEffect.SetActive(false);
                if(grid[x, y].isFlamable)
                {
                    alertUI.problemMax += 1;
                    alertUI.problemCurrent += 1;
                }
            }
        }


        
        //Debug.Log("Length of grid is " + grid.Length);
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


    public void GenerateLaserFire(Node firePos)
    {
        int chanceToStartFire = Random.Range(1, 11);

        if (chanceToStartFire > 5)
        {
            //Debug.Log("In this house we stan the fire gods");
            Collider[] fireLocation = Physics.OverlapSphere(firePos.worldPosition, 1f, flamableMask);
            foreach (var firePosition in fireLocation)
            {
                if (firePos.isFlamable)
                {
                    if(grid[checkX, checkY].isFlamable)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
        }
        else
        {
            if (firePos.isFlamable)
            {
                alertUI.problemCurrent -= 1;
                firePos.fireTimer = fireTimer;
                firePos.isFlamable = false;
                firePos.fireEffect.SetActive(true);
                fires.Add(firePos);
            }
        }
    }

    public void GenerateEngineFire()
    {
        if(firePos.isFlamable && firePos != null)
        {
            alertUI.problemCurrent -= 1;
            firePos.fireTimer = fireTimer;
            firePos.isFlamable = false;
            firePos.fireEffect.SetActive(true);
            fires.Add(firePos);
        }

        //int safetyNet = 0;
        //while (fireStartPosition.isFlamable != true)
        //{
        //    fireStartPosition = grid[Random.Range(0, gridSizeX), Random.Range(0, gridSizeY)];
        //    safetyNet++;

        //    //Debug.Log("Fiyah");
        //    GameObject fire = Instantiate(fireEffect, fireStartPosition.worldPosition, Quaternion.Euler(-90f, 0f, 0f));
        //    fires.Add(fire);

    public void onFire(Node firePos)
    {
        Collider[] castedObjects = Physics.OverlapSphere(firePos.worldPosition, 1);

        for (int i = 0; i < castedObjects.Length; i++)
        {
            if(castedObjects[i].CompareTag("Extinguisher"))
            {
                alertUI.problemCurrent += 1;
                fires.Remove(firePos);
                firePos.isFlamable = true;
                firePos.fireEffect.SetActive(false);
                return;
            }
            if(castedObjects[i].GetComponent<PlayerController>() != null)
            {
                castedObjects[i].GetComponent<PlayerController>().onFireTimerCur -= Time.time * 2;
            }
        }

        firePos.fireTimer -= Time.deltaTime;
        if(firePos.fireTimer < 0)
        {
            List<Node> flameableNeighbors = GetFlamableNeighbors(firePos);

            if (flameableNeighbors.Count > 0)
            {
                int index = Random.Range(0, flameableNeighbors.Count);
                GenerateFire(flameableNeighbors[index]);
            }
            firePos.fireTimer = fireTimer;
        }

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
            //GenerateFire();
            LightFire = false;
        }


        if (grid != null && showGrid)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isFlamable) ? Color.white : Color.red;
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }

    }
}