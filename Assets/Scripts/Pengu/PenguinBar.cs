using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PenguinBar : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float verticalOffset = 2;

    private Penguin pengu = null;
    private static Camera cam = null;
    private bool userClicked = false;

    public void SetPengu(Penguin p)
    {
        pengu = p;
        pengu.Bar = this;
        if (!cam) cam = Camera.main;
    }

    private void OnEnable()
    {
        Invoke(nameof(MakePenguHappy), 2);
    }

    private void OnDisable()
    {
        userClicked = false;
    }

    private void LateUpdate()
    {
        transform.position = cam.WorldToScreenPoint(pengu.transform.position + Vector3.up * verticalOffset);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        userClicked = true;
        gameObject.SetActive(false);
        pengu.GoNext();
    }

    private void MakePenguHappy()
    {
        if (!userClicked)
        {
            gameObject.SetActive(false);
            pengu.GoNext();
        }
    }
}
