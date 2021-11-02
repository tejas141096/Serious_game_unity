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

    // TMP Text to update on canvas
    [SerializeField]
    TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        // Hit a raycast on object
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f))
            {
                // hit True if hits an object with *Mesh Collider*
                if (hit.transform != null)
                {
                    // Display UI Canvas on hit
                    UI.SetActive(true);
                    text.SetText(hit.transform.gameObject.name);
                    //print(hit.transform.gameObject.name);
                }
            }
        }
    }

    // Function to close the UI on close button click
    public void CloseUI()
    {
        UI.SetActive(false);
    }
}