using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pengu")]
public class PenguVariant : ScriptableObject
{
    [SerializeField] private string penguName = string.Empty;
    [SerializeField] private Sprite penguIcon = null;
    [SerializeField] private Penguin penguPrefab = null;

    public string Name => penguName;
    public Sprite Icon => penguIcon;
    public Penguin Prefab => penguPrefab;
}
