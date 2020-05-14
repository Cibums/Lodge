using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MachineBehaviour
{
    public int[] sawableItems;
    private Transform lastConveyor;

    private void OnTriggerEnter(Collider other)
    {
        bool has = false;
        int id = -1;

        foreach (int i in sawableItems)
        {
            if (other.gameObject.GetComponent<ItemBehaivour>().id == i)
            {
                id = other.gameObject.GetComponent<ItemBehaivour>().id;
                has = true;
            }
        }

        if (has)
        {
            if (other.gameObject.GetComponent<ItemBehaivour>())
            {
                lastConveyor = other.gameObject.GetComponent<ItemBehaivour>().lastConveyor;
            }

            SpawnItem();

            Destroy(other.gameObject);
        }

        int GetSawedID(int itemID)
        {
            switch (itemID)
            {
                case 0:
                    return 3;
                default:
                    return 0;
            }
        }

        void SpawnItem()
        {
            Transform conveyor = null;

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Conveyor"))
            {
                if (Vector3.Distance(go.transform.position, transform.position) <= 1.2f && go.transform != lastConveyor)
                {
                    conveyor = go.transform;
                }
            }

            if (conveyor != null)
            {
                Instantiate(GameController.gameController.allItems[GetSawedID(id)].prefab, conveyor.position + new Vector3(0, 0.3f, 0), Quaternion.identity).GetComponent<ItemBehaivour>().id = GetSawedID(id);
            }
        }
    }
}
