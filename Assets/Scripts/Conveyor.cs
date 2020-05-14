using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MachineBehaviour
{
    public float Speed;
    public Direction direction;

    private void FixedUpdate()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        List<Transform> itemsInRange = new List<Transform>();

        foreach (GameObject item in items)
        {
            if (Vector3.Distance(item.transform.position, transform.position) <= 1.2f)
            {
                itemsInRange.Add(item.transform);
            }
        }

        foreach (Transform go in itemsInRange)
        {
            if (go.gameObject.GetComponent<Rigidbody>())
            {
                if (go.gameObject.GetComponent<ItemBehaivour>())
                {
                    go.gameObject.GetComponent<ItemBehaivour>().lastConveyor = transform;
                }

                switch (direction)
                {
                    case Direction.Left:
                        go.gameObject.GetComponent<Rigidbody>().velocity = Speed * (transform.forward - transform.right);
                        break;
                    case Direction.Right:
                        go.gameObject.GetComponent<Rigidbody>().velocity = Speed * (transform.forward + transform.right);
                        break;
                    default:
                        go.gameObject.GetComponent<Rigidbody>().velocity = Speed * (transform.forward);
                        break;
                }
            }
        }
    }
}

public enum Direction
{
    None,
    Left,
    Right
}