using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lodge : MachineBehaviour
{
    public Transform lodgeOptionsPanel;
    public TMPro.TMP_Text levelText;

    private void OnMouseDown()
    {
        GameController.gameController.menuOpen = true;
        lodgeOptionsPanel.gameObject.SetActive(true);
        levelText.SetText("Level " + GameController.gameController.level.ToString());
    }

    public void Close()
    {
        GameController.gameController.menuOpen = false;
        lodgeOptionsPanel.gameObject.SetActive(false);
    }
}
