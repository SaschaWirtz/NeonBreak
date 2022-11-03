using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFactory : MonoBehaviour {
    public static BrickFactory Instance;

    public GameObject Brick;
    public GameObject EnlargedPadelBrick;

    private readonly int COL = 4;
    private readonly int ROW = 4;
    private readonly float COLSTART = -1.5f;
    private readonly float ROWSTART = 0f;
    private readonly float COLSTEPS = 1f;
    private readonly float ROWSTEPS = 1f;

    public static BrickFactory GetInstance() {
        return Instance;
    }

    private void Awake() {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }

        this.generateBricks();
    }

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