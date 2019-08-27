using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    [Header("General Positioning")]
    [SerializeField] Vector3 startingPosition;
    public enum GridType { XbyY, ClickToRemove }
    [SerializeField] GridType gridType;
    public enum Orientation { TopLeft }
    [SerializeField] Orientation orientation;
    [SerializeField] int xCoords;
    [SerializeField] int yCoords;
    [SerializeField] int xSpacing;
    [SerializeField] int ySpacing;
    [Header("Actions")]
    [SerializeField] bool createGrid = false;
    [SerializeField] bool clearGrid = false;
    [SerializeField] bool register = false;
    [SerializeField] bool save = false;
    [Header("Objects")]
    [SerializeField] GameObject slotPrefab;
    [SerializeField] GameObject gridRoot;

    [SerializeField] string layoutName = "";
    public List<GameObject> slots = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateGrid();
        ClearGrid();
        Register();
        Save();
    }

    private void Save()
    {
        if (save)
        {
            save = false;
            if (layoutName.Length == 0) { return; }
            //PrefabUtility.CreatePrefab("Assets/Resources/Prefabs/" + layoutName + ".prefab", gridRoot);
            PrefabUtility.SaveAsPrefabAsset(gridRoot, "Assets/Resources/Prefabs/" + layoutName + ".prefab");
            layoutName = "";
        }
    }

    private void ClearGrid()
    {
        if (clearGrid)
        {
            clearGrid = false;
            for (int i = 0; i < slots.Count; i++)
            {
                DestroyImmediate(slots[i]);
            }
            slots.Clear();
        }
    }

    private void CreateGrid()
    {
        if (createGrid)
        {
            createGrid = false;
            int index = 0;
            for (int y = 0; y < yCoords; y++)
            {
                for (int x = 0; x < xCoords; x++)
                {
                    GameObject go = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    go.name = "Slot" + index.ToString();

                    switch (orientation)
                    {
                        case Orientation.TopLeft:
                            go.transform.position = new Vector3(
                                startingPosition.x + x * xSpacing,
                                startingPosition.y,
                                startingPosition.z - y * ySpacing
                                );
                            break;
                    }
                    go.transform.parent = gridRoot.transform;
                    index++;
                    slots.Add(go);
                }
            }
        }
    }

    private void Register()
    {
        if (register)
        {
            register = false;
            SceneView.duringSceneGui -= OnClickToRemove;
            SceneView.duringSceneGui += OnClickToRemove;
        }
    }

    private void OnClickToRemove(SceneView sceneView)
    {
        Event e = Event.current;
        if (e.isMouse && e.button == 0 && e.type == EventType.MouseDown) // todo Update this for updated mouseDown event...?
        {
            Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x,
                                              -e.mousePosition.y + Camera.current.pixelHeight));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                slots.Remove(hit.collider.gameObject);
                DestroyImmediate(hit.collider.gameObject);
            }
            e.Use();
        }
    }
}
