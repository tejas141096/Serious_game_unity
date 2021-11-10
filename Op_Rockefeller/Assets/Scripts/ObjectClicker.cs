using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ObjectClicker : MonoBehaviour
{
    // Reference to UI Canvas
    [SerializeField]
    GameObject UI;

    [SerializeField]
    GameObject Inventory;

    [SerializeField]
    GameObject InventoryButton;

    [SerializeField]
    GameObject CombineShipUI;

    // TMP Text to update on canvas
    [SerializeField]
    TextMeshProUGUI Headertext;

    [SerializeField]
    TextMeshProUGUI Detailstext;

    [SerializeField]
    TextMeshProUGUI CSTextHeader;

    [SerializeField]
    TextMeshProUGUI CSTextDetails;

    [SerializeField]
    Image CSImage;

    GameObject lastGameObject;

    Dictionary<string, (int, bool)> item = new Dictionary<string, (int, bool)>();
    RaycastHit hit;

    void Start()
    {
        UI.SetActive(false);
        Inventory.SetActive(false);
        CombineShipUI.SetActive(false);
        item.Add("Pyramid", (0, false));
        item.Add("Pyramid (2)", (1, false));
        item.Add("space_panel", (2, false));
        item.Add("space_sphere", (3, false));
        item.Add("space_sphere (1)", (4, false));
        item.Add("space_pipe", (5, false));
    }

    // Update is called once per frame
    void Update()
    {
        if (!UI.activeSelf)
        {
            // Hit a raycast on object
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                    // hit True if hits an object with *Mesh Collider*
                    if (hit.transform != null)
                    {
                        var data = hit.transform.gameObject.GetComponent<TextData>();
                        if (data != null)
                        {
                            // Display UI Canvas on hit
                            UI.SetActive(true);
                            Headertext.SetText(data.Title);
                            Detailstext.SetText(data.Description);

                            lastGameObject = hit.transform.gameObject;
                            //print(lastGameObject.GetType());
                            //print(hit.transform.gameObject.name);
                        }
                    }
                }
            }
        }
    }

    public void CollectObject()
    {
        AddObjectToInventory(lastGameObject);
    }

    void AddObjectToInventory(GameObject lastGameObject)
    {
        Inventory.SetActive(true);
        item[lastGameObject.name] = (item[lastGameObject.name].Item1, true);
        string UIPaneltoUpdate = "ItemPanel (" + item[lastGameObject.name].Item1 + ")";
        string UIImagetoUpdate = "ItemLogo (" + item[lastGameObject.name].Item1 + ")";
        GameObject InventoryUIPanelToUpdate = GameObject.Find(UIPaneltoUpdate);
        GameObject InventoryUIImageToUpdate = GameObject.Find(UIImagetoUpdate);

        InventoryUIPanelToUpdate.GetComponent<Image>().color = new Color(InventoryUIPanelToUpdate.GetComponent<Image>().color.r, InventoryUIPanelToUpdate.GetComponent<Image>().color.g, InventoryUIPanelToUpdate.GetComponent<Image>().color.b, 1f);
        InventoryUIImageToUpdate.GetComponent<Image>().color = new Color(InventoryUIImageToUpdate.GetComponent<Image>().color.r, InventoryUIImageToUpdate.GetComponent<Image>().color.g, InventoryUIImageToUpdate.GetComponent<Image>().color.b, 1f);
        lastGameObject.SetActive(false);

        Inventory.SetActive(false);
    }

    public void OpenCombineShip()
    {
        var button = EventSystem.current.currentSelectedGameObject.name;
        print(button);
        if (button == "CombineShipButton2")
        {
            if (item["Pyramid"].Item2 && item["space_sphere"].Item2 && item["space_panel"].Item2)
            {
                CombineShipUI.SetActive(true);
                UI.SetActive(false);
                Inventory.SetActive(false);
                InventoryButton.SetActive(false);

                var data = EventSystem.current.currentSelectedGameObject.GetComponent<CSTextData>();

                print(data.Title);
                CSTextHeader.SetText(data.Title);
                CSTextDetails.SetText(data.Description);
                CSImage.sprite = data.image;
            }
        }

        if (button == "CombineShipButton1")
        {
            if (item["Pyramid (2)"].Item2 && item["space_sphere (1)"].Item2 && item["space_pipe"].Item2)
            {
                CombineShipUI.SetActive(true);
                UI.SetActive(false);
                Inventory.SetActive(false);
                InventoryButton.SetActive(false);

                var data = EventSystem.current.currentSelectedGameObject.GetComponent<CSTextData>();
                CSTextHeader.SetText(data.Title);
                CSTextDetails.SetText(data.Description);
                CSImage.sprite = data.image;
            }
        }
    }

    public void CloseCombineShip()
    {
        UI.SetActive(false);
        Inventory.SetActive(false);
        InventoryButton.SetActive(true);
        CombineShipUI.SetActive(false);
    }

    public void OpenInventory()
    {
        Inventory.SetActive(true);
        InventoryButton.SetActive(false);
    }

    public void CloseInventory()
    {
        Inventory.SetActive(false);
        InventoryButton.SetActive(true);
    }

    // Function to close the UI on close button click
    public void CloseUI()
    {
        UI.SetActive(false);
        InventoryButton.SetActive(true);
    }
}