using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class BypassingObject : MonoBehaviour
{
    internal TilemapRenderer tilemapRenderer;

    void Awake()
    {
        tilemapRenderer = GetComponentInChildren<TilemapRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        tilemapRenderer.sortingOrder = 1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        tilemapRenderer.sortingOrder = -1;
    }
}
