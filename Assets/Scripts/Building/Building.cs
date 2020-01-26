using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private bool isAuto = false;
    [SerializeField] private float duration = 2;
    private bool isFull = false;

    public float Percentage { get; private set; } = 0;
    public BuildingBar Bar { get; set; } = null;

    private void Start()
    {
        Manager.Instance.AddBuildingBar(this);
    }

    private void FixedUpdate()
    {
        if (!isFull) Percentage += 1 / duration * Time.deltaTime;

        if (Percentage >= 1)
        {
            isFull = true;

            if (isAuto)
            {
                GoNext();
            }
            else
            {
                Bar.Stop();
            }
        }
    }

    public void GoNext()
    {
        Percentage = 0;
        isFull = false;
    }
}
