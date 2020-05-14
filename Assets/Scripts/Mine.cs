using UnityEngine;

public class Mine : MachineBehaviour
{
    public int minSeconds;
    public int maxSeconds;

    private int seconds = 1;
    private float t = 0;

    private void Update()
    {
        //Counts seconds
        t += Time.deltaTime;

        if (t >= seconds)
        {
            //Spawns Item Each Second
            SpawnItem();
            t = 0.0f;
            seconds = Random.Range(minSeconds, maxSeconds);
        }
    }

    /// <summary>
    /// Spawn item in scene
    /// </summary>
    private void SpawnItem()
    {
        Transform conveyor = null;

        //Gets nearest conveyor
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Conveyor"))
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 1.2f)
            {
                conveyor = go.transform;
            }
        }

        //If near conveyor exists
        if (conveyor != null)
        {
            //Spawn iron
            int id = 2;

            Instantiate(GameController.gameController.allItems[id].prefab, conveyor.position + new Vector3(0, 0.3f, 0), Quaternion.identity).GetComponent<ItemBehaivour>().id = id;
        }
    }
}