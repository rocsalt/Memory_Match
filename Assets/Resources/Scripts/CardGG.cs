using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGG : MonoBehaviour
{
    public string cardType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCard(string type)
    {
        cardType = type;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = Resources.Load("Graphics/" + cardType) as Texture2D;
    }
}
