using UnityEngine;

public class Output : MachineBehaviour
{
    private float t = 0;

    private void Update()
    {
        t += Time.deltaTime;
        Debug.Log(t);

        if (t >= 1.0f)
        {
            OutputItem();
            t = 0.0f;
        }
    }

    /// <summary>
    /// Spawns item from near storage to scene
    /// </summary>
    private void OutputItem()
    {
        Transform storage = null;

        //Gets neasrest storage
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Storage"))
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 1.5f)
            {
                storage = go.transform;
            }
        }

        Transform conveyor = null;

        //Gets nearest conveyor
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Conveyor"))
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 1.2f)
            {
                conveyor = go.transform;
            }
        }

        //Return if storage has no items
        if (GameController.gameController.inventory.Count <= 0)
        {
            return;
        }

        //If both conveyor and storage is near
        if (storage != null && conveyor != null)
        {
            //Take item from storage
            int id = GameController.gameController.DestroyRandomItem();

            //Spawn item to scene
            Instantiate(GameController.gameController.allItems[id].prefab, conveyor.position + new Vector3(0, 0.3f, 0), Quaternion.identity).GetComponent<ItemBehaivour>().id = id;
        }
    }
}