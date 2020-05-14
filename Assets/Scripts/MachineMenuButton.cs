using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMenuButton : MonoBehaviour
{
    public int id;

    public void SetID()
    {
        GameController.gameController.selectedIndex = id;
    }

    public void CloseHUD()
    {
        UIController.ui.ToggleHud();
    }
}
