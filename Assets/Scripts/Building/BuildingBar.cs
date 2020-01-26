using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBar : MonoBehaviour
{
    [SerializeField] private float verticalOffset = 2;

    private GameObject bar = null;
    private Transform fill = null;
    private Button clickButton = null;

    private Building building = null;
    private static Camera cam = null;

    public void SetBuilding(Building b)
    {
        building = b;
        building.Bar = this;
        if (!cam) cam = Camera.main;

        bar = transform.GetChild(0).gameObject;
        fill = bar.transform.GetChild(0);
        clickButton = transform.GetChild(1).GetComponent<Button>();
        clickButton.onClick.AddListener(() => Continue());

        clickButton.gameObject.SetActive(false);

        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        var scale = fill.localScale;
        scale.x = building.Percentage;
        fill.localScale = scale;

        transform.position = cam.WorldToScreenPoint(building.transform.position + Vector3.up * verticalOffset);
    }

    public void Stop()
    {
        bar.SetActive(false);
        clickButton.gameObject.SetActive(true);
    }

    public void Continue()
    {
        bar.SetActive(true);
        clickButton.gameObject.SetActive(false);
        building.GoNext();
    }
}
