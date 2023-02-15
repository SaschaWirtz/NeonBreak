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
    /// Maximal amount of normal bricks that can be pooled
    /// </summary>
    [SerializeField]
    private int maxNormalPoolSize = 20;

    /// <summary> 
    /// Maximal amount of enlarged bricks that can be pooled
    /// </summary>
    [SerializeField]
    private int maxEnlargedPoolSize = 10;

    /// <summary> 
    /// Object normal brick pool.
    /// </summary>
    private Stack<GameObject> normalBrickPool;

    /// <summary> 
    /// Object enlarged brick pool.
    /// </summary>
    private Stack<GameObject> enlargedBrickPool;

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
        this.normalBrickPool = new Stack<GameObject>();
        this.enlargedBrickPool = new Stack<GameObject>();
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
            this.normalBrickPool = new Stack<GameObject>();
            this.enlargedBrickPool = new Stack<GameObject>();
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
                        brickObject = this.GetEnlargedBrick();
                    }else {
                        brickObject = this.GetNormalBrick();
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
    /// Getting active normal brick from pool.
    /// </summary>
    /// <returns> 
    /// Normal brick gameobject.
    /// </returns>
    private GameObject GetNormalBrick() {
        if(this.normalBrickPool.Count > 0) {
            GameObject normalBrick = this.normalBrickPool.Pop();
            normalBrick.gameObject.SetActive(true);
            return  normalBrick;
        }else {
            return Instantiate(this.Brick);
        }
    }

    /// <summary> 
    /// Getting active enlarged brick from pool.
    /// </summary>
    /// <returns> 
    /// Enlarged brick gameobject.
    /// </returns>
    private GameObject GetEnlargedBrick() {
        if(this.enlargedBrickPool.Count > 0) {
            GameObject enlargedBrick = this.enlargedBrickPool.Pop();
            enlargedBrick.gameObject.SetActive(true);
            return enlargedBrick;
        }else {
            return Instantiate(this.EnlargedPadelBrick);
        }
    }

    /// <summary> 
    /// Returns brick back to pool if pool isn't full.
    /// </summary>
    /// <param name="brick"> 
    /// Brick that is been returned to the pool.
    /// </param>
    public void ReturnBrick(GameObject brick) {
        brick.gameObject.SetActive(false);
        switch(brick.GetComponent<BrickController>().type) {
            case BrickType.Default:
                if(this.normalBrickPool.Count < this.maxNormalPoolSize) {
                    this.normalBrickPool.Push(brick);
                }
                break;
            case BrickType.EnlargePadel:
            if(this.enlargedBrickPool.Count < this.maxEnlargedPoolSize) {
                this.enlargedBrickPool.Push(brick);
            }
                break;
            default:
                break;
        }
    }
}