using UnityEngine;

public class DestroyWhenEmpty : MonoBehaviour
{
    private void Update()
    {
        //If transform has no children, destroy
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}