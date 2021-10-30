using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField]
    GameObject UI_Unit;
    
    [SerializeField]
    TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f))
            {
                if (hit.transform != null)
                {
                    //switch (hit.transform.gameObject.name)
                    //{
                    //    case "Unit": UI_Unit.SetActive(true); break;
                    //    default: print("Other than Unit"); break;
                    //}

                    UI_Unit.SetActive(true);
                    text.SetText(hit.transform.gameObject.name);
                    //print(hit.transform.gameObject.name);
                }
            }
        }
    }

    public void CloseUI()
    {
        UI_Unit.SetActive(false);
    }
}