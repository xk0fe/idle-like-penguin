using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    [SerializeField] private BoolVariable isPhotoMode;
    [SerializeField] private LayerMask penguMask = 0;
    [SerializeField] private Button changeModeButton = null;
    
    [Header("Bars")]
    [SerializeField] private Transform barsParent = null;
    [SerializeField] private PenguinBar penguBarPrefab = null;
    [SerializeField] private BuildingBar bBarPrefab = null;

    private CameraController cameraController = null;
    private List<Penguin> pengus = new List<Penguin>();

    public static bool InPhotoMode => Instance.isPhotoMode.Value;

    private void Awake() => Instance = this;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        changeModeButton.onClick.AddListener(() => 
        {
            if (pengus.Count > 0)
            {
                isPhotoMode.SetOpposite();
            }
        });

        isPhotoMode.OnValueChanged += IsPhotoMode_OnValueChanged;
    }

    private void IsPhotoMode_OnValueChanged(bool value)
    {
        if (value)
        {
            var pengu = pengus[Random.Range(0, pengus.Count)];
            cameraController.SetTarget(pengu.transform);
        }
        else
        {
            cameraController.SetDefault();
        }
    }

    private void Update()
    {
        HandlePhotoMode();
    }

    private void HandlePhotoMode()
    {
        if (InPhotoMode) cameraController.HandleRotation();
        else cameraController.HandlePosition();

        if (Input.GetKeyDown(KeyCode.Mouse0) && InPhotoMode)
        {
            Ray ray = cameraController.Cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, penguMask))
            {
                var pengu = hit.transform.GetComponent<Penguin>();

                if (pengu != null) cameraController.SetTarget(pengu.transform);
            }
        }
    }

    public void AddPenguBar(Penguin pengu)
    {
        if (!pengus.Contains(pengu))
        {
            var bar = Instantiate(penguBarPrefab, barsParent);
            bar.SetPengu(pengu);
            pengu.GoNext();

            pengus.Add(pengu);
        }
    }

    public void AddBuildingBar(Building b)
    {
        var bar = Instantiate(bBarPrefab, barsParent);
        bar.SetBuilding(b);
    }
}
