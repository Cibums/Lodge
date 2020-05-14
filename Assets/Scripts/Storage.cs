using UnityEngine;

public class Storage : MachineBehaviour
{
    public int size = 20;

    private void Awake()
    {
        gameObject.tag = "Storage";
    }

    private void Start()
    {
        GameController.gameController.maxItems += size;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Picks up item in scene to inventory
        if (other.gameObject.tag == "Item" && GameController.gameController.inventory.Count != GameController.gameController.maxItems)
        {
            GameController.gameController.inventory.Add(other.gameObject.GetComponent<ItemBehaivour>().id);
            Destroy(other.gameObject);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        if (isQuitting || ignoreDestruction)
        {
            return;
        }

        //Destroys random items that doesn't fit in inventory
        if (GameController.gameController.inventory.Count > GameController.gameController.maxItems - size)
        {
            //Gets how many items that should be destroyed
            int itemsToDestroyCount = GameController.gameController.inventory.Count - (GameController.gameController.maxItems - size);

            Debug.Log("Destroyed " + itemsToDestroyCount.ToString() + " random items from storages");

            for (int i = 0; i < itemsToDestroyCount; i++)
            {
                //Destroys
                GameController.gameController.DestroyRandomItem();
            }
        }

        //Updates max items
        GameController.gameController.maxItems -= size;
    }
}