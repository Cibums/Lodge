using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level = 0;
    public int maxHeight = 4;
    public int worldSize = 150;

    [Range(0, 100)]
    public int treeAmount = 25;

    [Range(0, 100)]
    public int rockAmount = 25;

    public int maxItems = 10;
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
    public int selectedIndex = 0;
    public bool menuOpen = false;

    private void Awake()
    {
        transforms = new Transform[worldSize, worldSize];

        if (gameController == null)
        {
            gameController = this;
        }

        transforms = new Transform[worldSize, worldSize];
    }

    private void Start()
    {
        GenerateForest(treeAmount, rockAmount);
    }

    private void Update()
    {
        if (menuOpen)
        {
            Time.timeScale = 0.0f;
            return;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
    }

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

    public void PlaceTree(Vector2 position, int height)
    {
        Transform tf = Instantiate(trees[height], new Vector3(position.x, 0, position.y), Quaternion.identity).transform;
        transforms[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = tf;
        tf.gameObject.GetComponentInChildren<Renderer>().material.color = tf.gameObject.GetComponentInChildren<Renderer>().material.color + new Color(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 1);
    }

    public void ClearSlot(int x, int y)
    {
        try
        {
            if (transforms[x, y] != null)
            {
                if (transforms[x, y].gameObject.GetComponent<MachineBehaviour>().dropAmount + inventory.Count <= maxItems && transforms[x, y].gameObject.GetComponent<MachineBehaviour>().indestructable == false)
                {
                    Destroy(transforms[x, y].gameObject);
                    transforms[x, y] = null;
                }
            }
        }
        catch
        {
            return;
        }
    }

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
            return;
        }
    }

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

    public int DestroyRandomItem()
    {
        int rnd = Random.Range(0, inventory.Count);

        int id = inventory[rnd];

        inventory.RemoveAt(rnd);

        return id;
    }

    public void UpgradeLodge()
    {
        level++;
    }
}