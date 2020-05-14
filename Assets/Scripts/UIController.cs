using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Transform HUDPanel;
    public Transform ShowButton;
    public Transform HideButton;

    public GameObject machineMenuButtonPrefab;

    public Transform machineMenuContentPanel;

    public static UIController ui;

    private void Awake()
    {
        if (ui == null)
        {
            ui = this;
        }
    }

    public void UpdateMachinesUI()
    {
        foreach (Transform t in machineMenuContentPanel)
        {
            Destroy(t.gameObject);
        }

        int index = 0;

        foreach (Machine machine in GameController.gameController.machines)
        {
            if (machine.placeable)
            {
                GameObject go = Instantiate(machineMenuButtonPrefab, machineMenuContentPanel);
                go.GetComponent<MachineMenuButton>().id = index;
                go.GetComponent<Image>().sprite = machine.icon;
            }

            index++;
        }
    }

    public void ToggleHud()
    {
        if (HUDPanel.gameObject.activeSelf)
        {
            HUDPanel.gameObject.SetActive(false);
            ShowButton.gameObject.SetActive(true);
            HideButton.gameObject.SetActive(false);
        }
        else if (!HUDPanel.gameObject.activeSelf)
        {
            HUDPanel.gameObject.SetActive(true);
            ShowButton.gameObject.SetActive(false);
            HideButton.gameObject.SetActive(true);

            UpdateMachinesUI();
        }
    }
}