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
        StartCoroutine(StartCardFlip());
    }


    //public Animation anim;

    //private void LateUpdate()
    //{
    //    anim = GetComponent<Animation>();
    //    foreach (AnimationState state in anim)
    //    {
    //        state.speed = 4f;
    //        print(state.speed);
    //    }

    //}



    public IEnumerator StartCardFlip()
    {
        AnimationClip animationClip;
        animationClip = cardView.GetComponent<Animation>().clip; // refer to pt 10 8:52 if this doesn't work...
        print(animationClip.name);
        if (state == CardState.Flipped)
        {
            print("~1~");
            print( cardView.GetComponent<Animation>().GetClip("CardFlip"));
        }
        else
        {
            print("~2~");
            cardView.GetComponent<AnimationState>().speed = -1f;
        }

        cardView.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(animationClip.length + delay);
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
