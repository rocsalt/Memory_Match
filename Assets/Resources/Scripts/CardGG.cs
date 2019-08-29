using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGG : VersionedView
{
    public enum CardState { Flipped, Hidden }
    public CardState state = CardState.Hidden;

    public GameObject cardView;
    public string cardType;
    public float delay = 0.5f;

    public override void DirtyUpdate()
    {
        base.DirtyUpdate();
        StartCoroutine(StartCardFlip());
    }

    public IEnumerator StartCardFlip()
    {
        cardView.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(delay);
    }

    public void GenerateCard(string type)
    {
        cardType = type;
        Renderer renderer = cardView.GetComponent<Renderer>();
        renderer.material.mainTexture = Resources.Load("Graphics/" + cardType) as Texture2D;
    }

    private void OnMouseDown()
    {
        if (state == CardState.Hidden)
        {
            state = CardState.Flipped;
            MarkDirty();
        }
    }
}
