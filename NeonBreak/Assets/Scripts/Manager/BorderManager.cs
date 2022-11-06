using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{

    /// <summary> 
    /// Prefab of lower edge.
    /// </summary>
    public GameObject lowerEdgePrefab;

    /// <summary> 
    /// Root canvas to be used for ui determination.
    /// </summary>
    public Canvas rootCanvas;

    /// <summary> 
    /// Create border colliders. Script execution order ensured by unity project settings.
    /// </summary>
    void Awake() {
        this.createBorderCollider();
    }

    /// <summary> 
    /// Removing border manager from sceentree.
    /// </summary>
    void Start()
    {
        Destroy(gameObject);
    }

    /// <summary> 
    /// Create the screen borders once for this level.
    /// </summary>
    private void createBorderCollider()
    {
        Vector2 rightUpperCorner = ViewportHelper.RightUpperCorner;
        Vector2 leftDownCorner = ViewportHelper.LeftDownCorner;
        Vector2[] collisionPoints;
 
        // Calculating offset of score border because ui
        RectTransform canvas = this.rootCanvas.GetComponent<RectTransform>();
        RectTransform scoreContainer = ScoreManager.GetInstance().GetComponent<RectTransform>();
        float scoreHeight = scoreContainer.rect.height / canvas.rect.height;
        scoreHeight = ViewportHelper.GetViewportHeight() * scoreHeight;

        EdgeCollider2D upperEdge = new GameObject("upperEdge").AddComponent<EdgeCollider2D>();
        upperEdge.tag = "Borders";
        collisionPoints = upperEdge.points;
        collisionPoints[0] = new Vector2(leftDownCorner.x, rightUpperCorner.y - scoreHeight);
        collisionPoints[1] = new Vector2(rightUpperCorner.x, rightUpperCorner.y - scoreHeight);
        upperEdge.points = collisionPoints;
 
        EdgeCollider2D lowerEdge = Instantiate(this.lowerEdgePrefab).GetComponent<EdgeCollider2D>();
        lowerEdge.name = "lowerEdge";
        collisionPoints = lowerEdge.points;
        collisionPoints[0] = new Vector2(leftDownCorner.x, leftDownCorner.y);
        collisionPoints[1] = new Vector2(rightUpperCorner.x, leftDownCorner.y);
        lowerEdge.points = collisionPoints;

        EdgeCollider2D leftEdge = new GameObject("leftEdge").AddComponent<EdgeCollider2D>();
        leftEdge.tag = "Borders";
        collisionPoints = leftEdge.points;
        collisionPoints[0] = new Vector2(leftDownCorner.x, leftDownCorner.y);
        collisionPoints[1] = new Vector2(leftDownCorner.x, rightUpperCorner.y);
        leftEdge.points = collisionPoints;
 
        EdgeCollider2D rightEdge = new GameObject("rightEdge").AddComponent<EdgeCollider2D>();
        rightEdge.tag = "Borders";
        collisionPoints = rightEdge.points;
        collisionPoints[0] = new Vector2(rightUpperCorner.x, rightUpperCorner.y);
        collisionPoints[1] = new Vector2(rightUpperCorner.x, leftDownCorner.y);
        rightEdge.points = collisionPoints;
    }
}