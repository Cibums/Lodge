using UnityEngine;

public class Saw : MachineBehaviour
{
    public int[] sawableItems;
    private Transform lastConveyor;

    private void OnTriggerEnter(Collider other)
    {
        bool has = false;
        int id = -1;

        //For all items that are sawable
        foreach (int i in sawableItems)
        {
            //If collided item is sawable
            if (other.gameObject.GetComponent<ItemBehaivour>().id == i)
            {
                //Set id to item id
                id = other.gameObject.GetComponent<ItemBehaivour>().id;
                has = true;
            }
        }

        if (has)
        {
            //Sets last conveyor
            if (other.gameObject.GetComponent<ItemBehaivour>())
            {
                lastConveyor = other.gameObject.GetComponent<ItemBehaivour>().lastConveyor;
            }

            //Saws
            SpawnItem(id);

            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Gets ID that the item should have after sawed
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    private int GetSawedID(int itemID)
    {
        switch (itemID)
        {
            case 0:
                return 3;

            default:
                return 0;
        }
    }

    /// <summary>
    /// Spawns item in scene
    /// </summary>
    private void SpawnItem(int id)
    {
        Transform conveyor = null;

        //Gets nearest conveyor
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Conveyor"))
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 1.2f && go.transform != lastConveyor)
            {
                conveyor = go.transform;
            }
        }

        if (conveyor != null)
        {
            //Spawns
            Instantiate(GameController.gameController.allItems[GetSawedID(id)].prefab, conveyor.position + new Vector3(0, 0.3f, 0), Quaternion.identity).GetComponent<ItemBehaivour>().id = GetSawedID(id);
        }
    }
}