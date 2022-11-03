using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{

    public GameObject lowerEdgePrefab;

    void Awake() {
        this.createBorderCollider();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Create the screen borders once for this level
    private void createBorderCollider()
    {
        Vector2 rightUpperCorner = ViewportHelper.RightUpperCorner;
        Vector2 leftDownCorner = ViewportHelper.LeftDownCorner;
        Vector2[] collisionPoints;
 
        EdgeCollider2D upperEdge = new GameObject("upperEdge").AddComponent<EdgeCollider2D>();
        upperEdge.tag = "Borders";
        collisionPoints = upperEdge.points;
        collisionPoints[0] = new Vector2(leftDownCorner.x, rightUpperCorner.y);
        collisionPoints[1] = new Vector2(rightUpperCorner.x, rightUpperCorner.y);
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