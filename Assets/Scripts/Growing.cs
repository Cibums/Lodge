using UnityEngine;

public class Growing : MonoBehaviour
{
    public int currentTreeHeight;
    public int minTime;
    public int maxTime;

    private int time;
    private int ticks;

    private void Start()
    {
        time = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        if (ticks >= time)
        {
            Grow();
        }

        ticks++;
    }

    private void Grow()
    {
        GameController.gameController.PlaceTree(new Vector2(transform.position.x, transform.position.z), currentTreeHeight + 1);
        gameObject.GetComponent<MachineBehaviour>().ignoreDestruction = true;
        Destroy(gameObject);
    }
}