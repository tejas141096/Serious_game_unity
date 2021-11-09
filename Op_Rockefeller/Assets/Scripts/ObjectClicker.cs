using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectClicker : MonoBehaviour
{
    // Reference to UI Canvas
    [SerializeField]
    GameObject UI;

    [SerializeField]
    GameObject Inventory;

    [SerializeField]
    GameObject InventoryButton;

    // TMP Text to update on canvas
    [SerializeField]
    TextMeshProUGUI Headertext;

    [SerializeField]
    TextMeshProUGUI Detailstext;

    GameObject lastGameObject;

    Dictionary<GameObject, (int, bool)> item = new Dictionary<GameObject, (int, bool)>();
    RaycastHit hit;

    void Start()
    {
        UI.SetActive(false);
        Inventory.SetActive(false);
        item.Add(GameObject.Find("Pyramid"), (0, false));
        item.Add(GameObject.Find("Pyramid (2)"), (1, false));
        item.Add(GameObject.Find("space_panel"), (2, false));
        item.Add(GameObject.Find("Box (1)"), (3, false));
        item.Add(GameObject.Find("Box (2)"), (4, false));
        item.Add(GameObject.Find("space_sphere"), (5, false));
        item.Add(GameObject.Find("space_sphere (1)"), (6, false));
        item.Add(GameObject.Find("space_pipe"), (7, false));
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
                        if (data != null) {
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

    public void CollectObject ()
    {
        AddObjectToInventory(lastGameObject);
    }

    void AddObjectToInventory (GameObject lastGameObject)
    {
        Inventory.SetActive(true);
        item[lastGameObject] = (item[lastGameObject].Item1, true);
        string UIPaneltoUpdate = "ItemPanel (" + item[lastGameObject].Item1 + ")";
        string UIImagetoUpdate = "ItemLogo (" + item[lastGameObject].Item1 + ")";
        GameObject InventoryUIPanelToUpdate = GameObject.Find(UIPaneltoUpdate);
        GameObject InventoryUIImageToUpdate = GameObject.Find(UIImagetoUpdate);

        InventoryUIPanelToUpdate.GetComponent<Image>().color = new Color(InventoryUIPanelToUpdate.GetComponent<Image>().color.r, InventoryUIPanelToUpdate.GetComponent<Image>().color.g, InventoryUIPanelToUpdate.GetComponent<Image>().color.b, 1f);
        InventoryUIImageToUpdate.GetComponent<Image>().color = new Color(InventoryUIImageToUpdate.GetComponent<Image>().color.r, InventoryUIImageToUpdate.GetComponent<Image>().color.g, InventoryUIImageToUpdate.GetComponent<Image>().color.b, 1f);
        lastGameObject.SetActive(false);

        Inventory.SetActive(false);
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