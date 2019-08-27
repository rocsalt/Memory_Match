using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    public Vector3 startingPosition;
    public enum GridType { XbyY }
    public GridType gridType;
    public enum Orientation { TopLeft }
    public Orientation orientation;
    public int xCoords;
    public int yCoords;
    public int xSpacing;
    public int ySpacing;
    public bool createGrid = false;
    public bool clearGrid = false;
    public GameObject slotPrefab;
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
    }

    private void CreateGrid()
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

    private void ClearGrid()
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
                    go.transform.parent = gameObject.transform;
                    index++;
                    slots.Add(go);
                }
            }
        }
    }
}
