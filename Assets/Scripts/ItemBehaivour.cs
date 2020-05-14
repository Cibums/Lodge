using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaivour : MonoBehaviour
{
    [HideInInspector] public int id = 0;
    [HideInInspector] public Transform lastConveyor;
    [SerializeField] private float despawnTime = 10;

    float t = 0;

    private void Update()
    {
        if (GetComponent<Rigidbody>().velocity.x < 1 && GetComponent<Rigidbody>().velocity.z < 1)
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
        }

        if (t >= despawnTime)
        {
            Destroy(gameObject);
        }
    }
}
