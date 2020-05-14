using UnityEngine;

[RequireComponent(typeof(MachineBehaviour))]
public class AlternatePlacementRotation : MonoBehaviour
{
    private void Start()
    {
        UpdateMachineRotation();

        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));

        if (GameController.gameController.transforms[pos.x + 1, pos.y] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x + 1, pos.y].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch { }
        }

        if (GameController.gameController.transforms[pos.x - 1, pos.y] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x - 1, pos.y].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch { }
        }

        if (GameController.gameController.transforms[pos.x, pos.y + 1] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x, pos.y + 1].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch { }
        }

        if (GameController.gameController.transforms[pos.x, pos.y - 1] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x, pos.y - 1].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch { }
        }
    }

    void UpdateMachineRotation()
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));

        try
        {
            if (GameController.gameController.transforms[pos.x + 1, pos.y] != null && GameController.gameController.transforms[pos.x + 1, pos.y].gameObject.tag == gameObject.tag)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            if (GameController.gameController.transforms[pos.x - 1, pos.y] != null && GameController.gameController.transforms[pos.x - 1, pos.y].gameObject.tag == gameObject.tag)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if (GameController.gameController.transforms[pos.x, pos.y + 1] != null && GameController.gameController.transforms[pos.x, pos.y + 1].gameObject.tag == gameObject.tag)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (GameController.gameController.transforms[pos.x, pos.y - 1] != null && GameController.gameController.transforms[pos.x, pos.y - 1].gameObject.tag == gameObject.tag)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        catch
        {
            Debug.Log("Trying to check if road is placed outside of map. Nothing to worry about");
        }
    }
}