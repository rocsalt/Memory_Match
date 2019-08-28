using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playmat : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    public Layout layout;
    public GameObject board;

    // Start is called before the first frame update
    void Start()
    {
        CreateLayout();
        CreateCardsFromLayout();
    }

    void CreateLayout()
    {
        board = layout.GetRandomLayout();
    }

    void CreateCardsFromLayout()
    {
        foreach(Slot slot in layout.slots)
        {
            GameObject go = GameObject.Instantiate(cardPrefab, slot.transform.position, Quaternion.Euler(0, 270, 270)) as GameObject;
            go.transform.parent = slot.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
