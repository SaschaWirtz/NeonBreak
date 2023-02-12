using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private readonly int COL = 5;

    /// <summary> 
    /// Number of rows in brick cluster
    /// </summary>
    private readonly int ROW = 5;

    /// <summary> 
    /// Start position of columns in brick cluster
    /// </summary>
    private readonly float COLSTART = -2f;

    /// <summary> 
    /// Start position of rows in brick cluster
    /// </summary>
    private readonly float ROWSTART = -0.5f;

    /// <summary> 
    /// Steps inbetween bricks in column
    /// </summary>
    private readonly float COLSTEPS = 1f;

    /// <summary> 
    /// Steps inbetween bricks in row
    /// </summary>
    private readonly float ROWSTEPS = 1f;

    /// <summary> 
    /// Object brick pool.
    /// </summary>
    private List<GameObject> brickPool;

    /// <summary> 
    /// List of available brick pattern.
    /// </summary>
    private List<List<bool>> spawnPattern = new List<List<bool>>{
        new List<bool>{true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
        new List<bool>{false, false, true, false, false, false, true, true, true, false, true, true, true, true, true, false, true, true, true, false, false, false, true, false, false},
        new List<bool>{false, false, true, false, false, false, false, true, false, false, true, true, true, true, true, false, false, true, false, false, false, false, true, false, false},
        new List<bool>{true, false, false, false, false, true, true, false, false, false, true, true, true, false, false, true, true, true, true, false, true, true, true, true, true},
        new List<bool>{false, false, false, false, true, false, false, false, true, true, false, false, true, true, true, false, true, true, true, true, true, true, true, true, true}
    };

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
    /// Initialize factory by making static.
    /// </summary>
    private void Awake() {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;

            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }
    }

    /// <summary> 
    /// Generating bricks.
    /// </summary>
    private void Start() {
        this.brickPool = new List<GameObject>();
        this.generateBricks();
    }

    /// <summary> 
    /// Reset brick pool.
    /// </summary>
    /// <param name="scene"> 
    /// Scene that got loaded.
    /// </param>
    /// <param name="mode"> 
    /// LoadSceneMode for this loading process.
    /// </param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.name == "Main") {
            this.brickPool = new List<GameObject>();
        }
    }

    /// <summary> 
    /// Generating and placing bricks in gameview.
    /// </summary>
    public void generateBricks() {
        Debug.Log("In generate method");
        int brickPattern = UnityEngine.Random.Range(0, this.spawnPattern.Count);
        int brickPosition = 0;
        for(int column = 0; column < this.COL; column++) {
            float yPos = this.ROWSTART + (column * this.ROWSTEPS);
            for(int row = 0; row < this.ROW; row++) {
                if(this.spawnPattern[brickPattern][brickPosition]) {
                    float xPos = this.COLSTART + (row * this.COLSTEPS);
                    GameObject brickObject;
                    int powerupRandomInt = new System.Random().Next(1, 5);
                    if(powerupRandomInt < 2) {
                        brickObject = this.GetActivePooledPowerup(BrickType.EnlargePadel);
                    }else {
                        brickObject = this.GetActivePooledPowerup(BrickType.Default);
                    }
                    if(brickObject != null) {
                        brickObject.transform.position = new Vector3(xPos, yPos);
                    }
                }
                brickPosition++;
            }
        }
    }

    /// <summary> 
    /// Returns a unused brick gameobject from objectpool.
    /// </summary>
    /// <param name="type"> 
    /// Brick type.
    /// </param>
    /// <returns> 
    /// Unused brick gameobject from objectpool.
    /// </returns>
    private GameObject GetActivePooledPowerup(BrickType type) {

        // Iterate through objectpool and search for unused brick gameobject.
        for(int pooledBrick = 0; pooledBrick < this.brickPool.Count; pooledBrick++) {
            if(!this.brickPool[pooledBrick].activeInHierarchy && this.brickPool[pooledBrick].GetComponent<BrickController>().type == type) {
                Debug.Log("Got old.");
                this.brickPool[pooledBrick].SetActive(true);
                return this.brickPool[pooledBrick];
            }
        }

        // Get correct prefab through bricktype
        GameObject toInstantiate;
        switch (type) {
            case BrickType.Default:
                toInstantiate = this.Brick;
                break;
            case BrickType.EnlargePadel:
                toInstantiate = this.EnlargedPadelBrick;
                break;
            default:
                toInstantiate = null;
                break;
        }

        if(toInstantiate != null) {
            // Create new brick gameobject if no unused is available.
            GameObject newBrick = Instantiate(toInstantiate);
            this.brickPool.Add(newBrick);
            return newBrick;
        }

        return null;
    }
}