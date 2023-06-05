using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Color baseColor, offsetColor;
    [SerializeField]
    private SpriteRenderer sRenderer;
    [SerializeField]
    private GameObject highlight;

    // change color for offset tiles
    //public void Init(bool isOffset)
    //{
        //sRenderer.color = isOffset ? offsetColor : baseColor;
    //}

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
