using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playmat : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    public Layout layout;
    public GameObject board;

    public List<CardGG> cards = new List<CardGG>();

    // Start is called before the first frame update
    void Start()
    {
        GameSettings.Instance().SetDifficulty(GameSettings.GameDifficulty.Medium);
        CreateLayout();
        CreateCardsFromLayout();
        CreateCardTypes();
    }

    void CreateLayout()
    {
        board = layout.GetRandomLayout();
    }

    void CreateCardsFromLayout()
    {
        foreach(Slot slot in layout.slots)
        {
            GameObject go = Instantiate(cardPrefab, slot.transform.position, Quaternion.Euler(180, 270, 270)) as GameObject;
            go.transform.parent = slot.transform;
            Destroy(slot.GetComponent<BoxCollider>());
            cards.Add(go.GetComponent<CardGG>());
        }
    }

    void CreateCardTypes()
    {
        for (int i = 0; i < cards.Count / 2; i++)
        {
            CardGG c1 = cards[i];
            CardGG c2 = cards[cards.Count / 2 - 1 - i];
            string type = GameSettings.Instance().GetRandomType();
            c1.GenerateCard(type);
            c2.GenerateCard(type);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
