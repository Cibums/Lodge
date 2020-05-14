using System;
using UnityEngine;

[RequireComponent(typeof(MachineBehaviour))]
public class AlternatePlacementRotation : MonoBehaviour
{
    private void Start()
    {
        //Rotates
        UpdateMachineRotation();

        //Gets position
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));

        if (GameController.gameController.transforms[pos.x + 1, pos.y] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x + 1, pos.y].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        if (GameController.gameController.transforms[pos.x - 1, pos.y] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x - 1, pos.y].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        if (GameController.gameController.transforms[pos.x, pos.y + 1] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x, pos.y + 1].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        if (GameController.gameController.transforms[pos.x, pos.y - 1] != null)
        {
            try
            {
                GameController.gameController.transforms[pos.x, pos.y - 1].gameObject.GetComponent<AlternatePlacementRotation>().UpdateMachineRotation();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }

    /// <summary>
    /// Rotates object based on adjecent object with the same tag
    /// </summary>
    private void UpdateMachineRotation()
    {
        //Gets the position of gameObject
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));

        try
        {
            //Checking on the X-axis positive
            if (GameController.gameController.transforms[pos.x + 1, pos.y] != null && GameController.gameController.transforms[pos.x + 1, pos.y].gameObject.tag == gameObject.tag)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            //Checking on the X-axis negative
            if (GameController.gameController.transforms[pos.x - 1, pos.y] != null && GameController.gameController.transforms[pos.x - 1, pos.y].gameObject.tag == gameObject.tag)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            //Checking on the Z-axis positive
            if (GameController.gameController.transforms[pos.x, pos.y + 1] != null && GameController.gameController.transforms[pos.x, pos.y + 1].gameObject.tag == gameObject.tag)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            //Checking on the Z-axis negative
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