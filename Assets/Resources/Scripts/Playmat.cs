using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playmat : VersionedView
{
    public enum BoardState { Flipping, Comparing }
    public BoardState state = BoardState.Flipping;

    [SerializeField] GameObject cardPrefab;
    private static Playmat playmat;
    public Layout layout;
    public GameObject board;

    public CardGG card1;
    public CardGG card2;

    public List<CardGG> cards = new List<CardGG>();

    // Start is called before the first frame update
    void Start()
    {
        playmat = this;
        GameSettings.Instance().SetDifficulty(GameSettings.GameDifficulty.Medium);
        CreateLayout();
        CreateCardsFromLayout();
        CreateCardTypes();
    }

    public static Playmat GetPlaymat()
    {
        return playmat;
    }

    void CreateLayout()
    {
        board = layout.GetRandomLayout();
    }

    void CreateCardsFromLayout()
    {
        foreach(Slot slot in layout.slots)
        {
            GameObject go = Instantiate(cardPrefab, slot.transform.position, Quaternion.Euler(0, 270, 270)) as GameObject;
            go.transform.parent = slot.transform;
            Destroy(slot.GetComponent<BoxCollider>());
            cards.Add(go.GetComponent<CardGG>());
        }
    }

    void CreateCardTypes()
    {
        for (int i = 0; i <= cards.Count / 2; i++)
        {
            CardGG c1 = cards[i];
            CardGG c2 = cards[cards.Count - 1 - i]; // todo Put in true randomization, this only makes the last one the same as the first one and moves inward
            string type = GameSettings.Instance().GetRandomType();
            c1.GenerateCard(type);
            c2.GenerateCard(type);
        }
    }
    public override void DirtyUpdate()
    {
        switch (state)
        {
            case BoardState.Comparing:
                Compare();
                break;
        }
    }

    private void Compare()
    {
        if (card1.cardType == card2.cardType)
        {
            print("We got a match");
        }
        else
        {
            card1.Unflip();
            card2.Unflip();
        }

        card1 = null;
        card2 = null;
        state = BoardState.Flipping;
    }

    public void SetCardsForMatch(CardGG card)
    {
        if (card1 == null)
        {
            card1 = card;
            state = BoardState.Flipping;
        }
        else
        {
            card2 = card;
            state = BoardState.Comparing;
        }
        MarkDirty();
    }
}
