using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguElement : MonoBehaviour
{
    [SerializeField] private PenguVariant pengu = null;
    [SerializeField] private Button buyButton = null;
    [SerializeField] private Text nameText = null;
    [SerializeField] private Image iconImg = null;

    private void Awake()
    {
        buyButton.onClick.AddListener(BuyPengu);
        nameText.text = pengu.Name;
        iconImg.sprite = pengu.Icon;
    }

    private void BuyPengu()
    {
        Instantiate(pengu.Prefab, Vector3.zero, Quaternion.identity);
    }
}
