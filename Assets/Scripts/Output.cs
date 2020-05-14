using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output : MachineBehaviour
{
    float t = 0;

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

    void OutputItem()
    {
        Transform storage = null;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Storage"))
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 1.5f)
            {
                storage = go.transform;
            }
        }

        Transform conveyor = null;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Conveyor"))
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 1.2f)
            {
                conveyor = go.transform;
            }
        }

        if (GameController.gameController.inventory.Count <= 0)
        {
            return;
        }

        if (storage != null && conveyor != null)
        {
            int id = GameController.gameController.DestroyRandomItem();

            Instantiate(GameController.gameController.allItems[id].prefab, conveyor.position + new Vector3(0,0.3f,0), Quaternion.identity).GetComponent<ItemBehaivour>().id = id;
        }
    }
}
