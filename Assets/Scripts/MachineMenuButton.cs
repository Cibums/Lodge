using UnityEngine;

public class MachineMenuButton : MonoBehaviour
{
    public int id;

    /// <summary>
    /// Sets selected item ID to button's item ID
    /// </summary>
    public void SetID()
    {
        GameController.gameController.selectedIndex = id;
    }

    /// <summary>
    /// Closes the HUD
    /// </summary>
    public void CloseHUD()
    {
        UIController.ui.ToggleHud();
    }
}