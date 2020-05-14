using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growing : MonoBehaviour
{
    public int currentTreeHeight;
    public int minTime;
    public int maxTime;

    private int time;
    private int ticks; 

    void Start()
    {
        time = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        if (ticks >= time)
        {
            Grow();
        }

        ticks++;
    }

    void Grow()
    {
        GameController.gameController.PlaceTree(new Vector2(transform.position.x, transform.position.z), currentTreeHeight + 1);
        gameObject.GetComponent<MachineBehaviour>().ignoreDestruction = true;
        Destroy(gameObject);
    }
}
