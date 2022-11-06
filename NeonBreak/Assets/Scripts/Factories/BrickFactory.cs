using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFactory : MonoBehaviour {

    /// <summary> 
    /// Static gameobject instance of brickfactory
    /// </summary>
    public static BrickFactory Instance;

    /// <summary> 
    /// Prefab reference of normal brick
    /// </summary>
    public GameObject Brick;

    /// <summary> 
    /// Prefab reference of enlarged padel brick
    /// </summary>
    public GameObject EnlargedPadelBrick;

    /// <summary> 
    /// Number of columns in brick cluster
    /// </summary>
    private readonly int COL = 4;

    /// <summary> 
    /// Number of rows in brick cluster
    /// </summary>
    private readonly int ROW = 4;

    /// <summary> 
    /// Start position of columns in brick cluster
    /// </summary>
    private readonly float COLSTART = -1.5f;

    /// <summary> 
    /// Start position of rows in brick cluster
    /// </summary>
    private readonly float ROWSTART = 0f;

    /// <summary> 
    /// Steps inbetween bricks in column
    /// </summary>
    private readonly float COLSTEPS = 1f;

    /// <summary> 
    /// Steps inbetween bricks in row
    /// </summary>
    private readonly float ROWSTEPS = 1f;

    /// <summary> 
    /// Getter for static gameobject instance.
    /// </summary>
    /// <returns> 
    /// Static gameobject instance.
    /// </returns>
    public static BrickFactory GetInstance() {
        return Instance;
    }

    /// <summary> 
    /// Initialize factory by making static and generating bricks.
    /// </summary>
    private void Awake() {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }

        this.generateBricks();
    }

    /// <summary> 
    /// Generating and placing bricks in gameview.
    /// </summary>
    public void generateBricks() {
        for(int column = 0; column < this.COL; column++) {
            float yPos = this.ROWSTART + (column * this.ROWSTEPS);
            for(int row = 0; row < this.ROW; row++) {
                float xPos = this.COLSTART + (row * this.COLSTEPS);
                GameObject brickObject;
                int powerupRandomInt = new System.Random().Next(1, 5);
                if(powerupRandomInt < 2) {
                    brickObject = Instantiate(this.EnlargedPadelBrick);
                }else {
                    brickObject = Instantiate(this.Brick);
                }
                brickObject.transform.position = new Vector3(xPos, yPos);
            }
        }
    }
}