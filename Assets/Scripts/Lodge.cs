using UnityEngine;

public class Lodge : MachineBehaviour
{
    public Transform lodgeOptionsPanel;
    public TMPro.TMP_Text levelText;

    private void OnMouseDown()
    {
        //When clicked open upgrade menu
        GameController.gameController.menuOpen = true;
        lodgeOptionsPanel.gameObject.SetActive(true);
        //Updates text to level
        levelText.SetText("Level " + GameController.gameController.level.ToString());
    }

    /// <summary>
    /// Closes lodge upgrade menu
    /// </summary>
    public void Close()
    {
        GameController.gameController.menuOpen = false;
        lodgeOptionsPanel.gameObject.SetActive(false);
    }
}