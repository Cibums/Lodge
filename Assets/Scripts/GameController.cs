using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Settings")]
    public int level = 0;
    public int maxHeight = 4;
    public int worldSize = 150;
    public int maxItems = 10;

    [Range(0, 100)]
    public int treeAmount = 25;

    [Range(0, 100)]
    public int rockAmount = 25;

    [Header("Objects")]
    public List<int> inventory = new List<int>();
    public Machine[] machines;
    public GameObject[] trees;
    public Item[] allItems;
    public GameObject destroyParticles;
    public LayerMask onlyPlane;
    public static GameController gameController;
    public Transform shadow;
    public Transform floor;
    public Transform lodge;
    public Transform[,] transforms = new Transform[10, 10];
    
    [Header("Debugging")]
    public int selectedIndex = 0;
    public bool menuOpen = false;

    private void Awake()
    {
        transforms = new Transform[worldSize, worldSize];

        //Creates an instance of the class if there isn't one already.
        if (gameController == null)
        {
            gameController = this;
        }

        transforms = new Transform[worldSize, worldSize];
    }

    private void Start()
    {
        //Generate a random forest on start.
        GenerateForest(treeAmount, rockAmount);
    }

    private void Update()
    {
        //If the menu is open, pause the game.
        if (menuOpen)
        {
            Time.timeScale = 0.0f;
            return;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        #region GroundControll
        
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Checks if the player is hovering their mouse over the ground, and if they are, they are able to place, destroy and rotate stuff.
        if (Physics.Raycast(ray, out hit, onlyPlane))
        {
            Vector3Int pos = new Vector3Int(Mathf.RoundToInt(hit.point.x), 0, Mathf.RoundToInt(hit.point.z));

            shadow.position = pos + new Vector3(0, 0.01f, 0);

            if (SlotOccupied(pos.x, pos.z))
            {
                shadow.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                shadow.gameObject.GetComponent<Renderer>().material.color = Color.white;
            }

            if (Input.GetMouseButton(0))
            {
                PlaceItem(selectedIndex, pos);
            }

            if (Input.GetMouseButton(1))
            {
                ClearSlot(pos.x, pos.z);
            }

            if (Input.GetMouseButtonDown(2))
            {
                if (transforms[pos.x, pos.z] != null)
                {
                    if (transforms[pos.x, pos.z].gameObject.GetComponent<MachineBehaviour>().rotatable)
                    {
                        transforms[pos.x, pos.z].gameObject.GetComponent<MachineBehaviour>().Rotate();
                    }
                }
            }
        }
        else
        {
            shadow.position = new Vector3(100, 0, 100);
        }
        #endregion
    }

    /// <summary>
    /// Generates a random forest at the start of the game.
    /// </summary>
    /// <param name="probabilityToSpawnTree"></param>
    /// <param name="probabilityToSpawnRock"></param>
    private void GenerateForest(int probabilityToSpawnTree, int probabilityToSpawnRock)
    {
        floor.position = new Vector3((worldSize / 2) - 0.5f, 0, (worldSize / 2) - 0.5f);
        floor.localScale = new Vector3((worldSize / 10) + 0.2f, 1, (worldSize / 10) + 0.2f);

        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                int prob = UnityEngine.Random.Range(0, 100);

                if (prob < probabilityToSpawnTree)
                {
                    PlaceTree(new Vector2(x, y), UnityEngine.Random.Range(0, maxHeight));
                }
                else
                {
                    prob = UnityEngine.Random.Range(0, 100);

                    if (prob < probabilityToSpawnRock)
                    {
                        PlaceItem(4, new Vector3Int(x, 0, y));
                    }
                }
            }
        }

        lodge.position = new Vector3((worldSize) / 2 - 0.5f, lodge.position.y, (worldSize) / 2 - 0.5f);

        ClearSlot(Mathf.RoundToInt(lodge.position.x - 0.5f), Mathf.RoundToInt(lodge.position.z - 0.5f));
        transforms[Mathf.RoundToInt(lodge.position.x - 0.5f), Mathf.RoundToInt(lodge.position.z - 0.5f)] = lodge;

        ClearSlot(Mathf.RoundToInt(lodge.position.x + 0.5f), Mathf.RoundToInt(lodge.position.z - 0.5f));
        transforms[Mathf.RoundToInt(lodge.position.x + 0.5f), Mathf.RoundToInt(lodge.position.z - 0.5f)] = lodge;

        ClearSlot(Mathf.RoundToInt(lodge.position.x - 0.5f), Mathf.RoundToInt(lodge.position.z + 0.5f));
        transforms[Mathf.RoundToInt(lodge.position.x - 0.5f), Mathf.RoundToInt(lodge.position.z + 0.5f)] = lodge;

        ClearSlot(Mathf.RoundToInt(lodge.position.x + 0.5f), Mathf.RoundToInt(lodge.position.z + 0.5f));
        transforms[Mathf.RoundToInt(lodge.position.x + 0.5f), Mathf.RoundToInt(lodge.position.z + 0.5f)] = lodge;
    }

    /// <summary>
    /// Places a tree in the world with random color.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="height"></param>
    public void PlaceTree(Vector2 position, int height)
    {
        Transform tf = Instantiate(trees[height], new Vector3(position.x, 0, position.y), Quaternion.identity).transform;
        transforms[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = tf;
        tf.gameObject.GetComponentInChildren<Renderer>().material.color = 
            tf.gameObject.GetComponentInChildren<Renderer>().material.color + 
            new Color(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 1);
    }

    /// <summary>
    /// Destroys something at spicific coordiantes.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void ClearSlot(int x, int y)
    {

        try
        {
            if (transforms[x, y] != null)
            {
                //If there is space in the inventory and the object is not indestructable, destroy the obeject.
                if (transforms[x, y].gameObject.GetComponent<MachineBehaviour>().dropAmount +
                    inventory.Count <= maxItems && transforms[x, y].gameObject.GetComponent<MachineBehaviour>().indestructable == false)
                {
                    Destroy(transforms[x, y].gameObject);
                    transforms[x, y] = null;
                }
            }
        }
        catch
        {
            Debug.LogWarning("Ignore this.");
            return;
        }
    }

    /// <summary>
    /// Places an item at a given location.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="position"></param>
    public void PlaceItem(int id, Vector3Int position)
    {
        try
        {
            if (transforms[position.x, position.z] == null)
            {
                transforms[position.x, position.z] = Instantiate(machines[id].prefab, position, Quaternion.identity).transform;
            }
        }
        catch
        {
            Debug.LogWarning("Ignore.");
            return;
        }
    }

    /// <summary>
    /// Checks if a slot is occupied or not.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool SlotOccupied(int x, int y)
    {
        try
        {
            if (transforms[x, y] == null)
            {
                return false;
            }
        }
        catch
        {
            return true;
        }

        return true;
    }

    /// <summary>
    /// Removes a random item.
    /// </summary>
    /// <returns></returns>
    public int DestroyRandomItem()
    {
        int rnd = Random.Range(0, inventory.Count);

        int id = inventory[rnd];

        inventory.RemoveAt(rnd);

        return id;
    }

    /// <summary>
    /// Increases level.
    /// </summary>
    public void UpgradeLodge()
    {
        level++;
    }
}