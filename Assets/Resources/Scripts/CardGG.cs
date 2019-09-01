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

    Animation anim;
    AnimationState clip;

    private void Start()
    {
        anim = cardView.GetComponent<Animation>();
        clip = anim["CardFlip"];
    }

    public override void DirtyUpdate()
    {
        StartCoroutine(StartCardFlip());
    }

    public IEnumerator StartCardFlip()
    {
        if (state == CardState.Flipped)
        {
            clip.speed = 1f;
        }
        else
        {
            clip.speed = -1f;
            clip.time = clip.length;
        }

        anim.Play();
        yield return new WaitForSeconds(clip.length + delay);
        if (state == CardState.Flipped)
        {
            Playmat.GetPlaymat().SetCardsForMatch(this);
        }
    }

    public void GenerateCard(string type)
    {
        cardType = type;
        Renderer renderer = cardView.GetComponent<Renderer>();
        renderer.material.mainTexture = Resources.Load("Graphics/" + cardType) as Texture2D;
    }

    public void Unflip()
    {
        state = CardState.Hidden;
        MarkDirty();
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
