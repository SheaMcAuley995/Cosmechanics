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
    public LayerMask playerLayer;
    public AlertUI alertUI;
    public float fireHealth;

    [Header("Debug tools")]
    [SerializeField] bool GenerateGrid;
    [SerializeField] bool LightFire;
    [SerializeField] bool showGrid;
    [SerializeField] bool startOnFire = false;
    [SerializeField] bool spawnTheFires = true;

    ParticleSystem par;
   

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
        spawnTheFires = true;
        CreateGrid();
    }

    public void Update()
    {
        // If the game isn't paused
        for (int i = 0; i < fires.Count; ++i)
        {
            onFire(fires[i]);
        }
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
                if(spawnTheFires)
                {
                    grid[x, y] = new Node(flameable, worldPoint, x, y, fireTimer, Instantiate(fireEffect, worldPoint, Quaternion.Euler(0f, 0f, 0f)), new Collider[4], fireHealth);
                    grid[x, y].fireEffect.SetActive(startOnFire);
                }
                else
                {
                    grid[x, y] = new Node(flameable, worldPoint, x, y, fireTimer, null,new Collider[4], fireHealth);
                }

                if (grid[x, y].isFlamable && nullCheck<AlertUI>(alertUI))
                {
                    alertUI.problemMax += 1;
                    alertUI.problemCurrent += 1;
                }
            }
        }

        if(startOnFire)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    fires.Add(grid[x, y]);
                }
            }
        }
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


    public List<Node> GetFlamableNeighbors(Node node)
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
                    if (grid[checkX, checkY].isFlamable)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
        }
        return neighbours;
    }

    public void GenerateLaserFire(Node firePos)
    {
        int chanceToStartFire = Random.Range(0, 100);

        if (chanceToStartFire < fireStartPercentage)
        {
            if (firePos.isFlamable)
            {
                if (nullCheck<AlertUI>(alertUI))
                {
                    alertUI.problemCurrent -= 1;
                }
                firePos.fireTimer = fireTimer;
                firePos.isFlamable = false;
                firePos.fireEffect.SetActive(true);
                fires.Add(firePos);
            }
        }
    }

    public void GenerateFire(Node firePos)
    {
        if (firePos.isFlamable && nullCheck<Node>(firePos))
        {
            if(nullCheck<AlertUI>(alertUI))
            {
                alertUI.problemCurrent -= 1;
            }
            
            firePos.fireTimer = fireTimer;
            firePos.fireEffect.SetActive(true);
            fires.Add(firePos);
        }
        
    }

    private bool nullCheck<T>(T thing)
    {
        if(thing != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void onFire(Node firePos)
    {
        int count = Physics.OverlapSphereNonAlloc(firePos.worldPosition, 1, firePos.playerArray, playerLayer);
        //Collider[] castedObjects = Physics.OverlapSphere(firePos.worldPosition, 1);
        firePos.isFlamable = false;
        for (int i = 0; i < count; i++)
        {
            if (firePos.playerArray[i].CompareTag("Extinguisher"))
            {
                firePos.fireHealth -= Time.deltaTime;
                var main = firePos.fireEffect.GetComponent<ParticleSystem>();
                var em = main.emission;
                em.rateOverTime = 120 * (firePos.fireHealth / fireHealth);

                if (firePos.fireHealth <= 0)
                {
                    fires.Remove(firePos);
                    firePos.isFlamable = true;
                    firePos.fireEffect.SetActive(false);
                    return;
                }
            }
            var playerCon = firePos.playerArray[i].GetComponent<PlayerController>();
            if (playerCon != null)
            {
                playerCon.onFireTimerCur -= Time.time * 2;
            }
        }
        Debug.Log(":");

        firePos.fireTimer -= Time.deltaTime;
        if (firePos.fireTimer < 0)
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
   
        if (GenerateGrid)
            CreateGrid();
   
        if (LightFire)
        {
            //GenerateFire();
            LightFire = false;
        }
   
   
        if (grid != null && showGrid)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isFlamable) ? Color.white : Color.red;
                //Gizmos.color = ((woooo % 2) != 1) ? Color.white : Color.red;
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                //woooo = (int)Mathf.Sin(woooo++ * Time.time * 10);
            }
            foreach (Node fire in fires)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(fire.worldPosition, 1);
            }
        }
   
    }
}