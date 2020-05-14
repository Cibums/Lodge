using UnityEngine;

public class ItemBehaivour : MonoBehaviour
{
    [HideInInspector] public int id = 0;
    [HideInInspector] public Transform lastConveyor;
    [SerializeField] private float despawnTime = 10;

    private float t = 0;

    private void Update()
    {
        //Counts Seconds
        if (GetComponent<Rigidbody>().velocity.x < 1 && GetComponent<Rigidbody>().velocity.z < 1)
        {
            t += Time.deltaTime;
        }
        else
        {
            //Sets variable to 0 when it hits despawnTime
            t = 0;
        }

        //If despawnTime seconds is hit, despawn item
        if (t >= despawnTime)
        {
            Destroy(gameObject);
        }
    }
}