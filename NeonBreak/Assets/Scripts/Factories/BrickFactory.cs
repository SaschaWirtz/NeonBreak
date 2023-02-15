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
    /// List of brick prefabs
    /// </summary>
    [SerializeField]
    private List<GameObject> brickPrefabs;

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
    private int maxPoolSize = 20;

    /// <summary> 
    /// Maximal amount of each powerup bricks that can be pooled
    /// </summary>
    [SerializeField]
    private int maxPowerupPoolSize = 10;

    /// <summary> 
    /// Dictionary of brick pools
    /// </summary>
    private Dictionary<int, Stack<GameObject>> brickPools;

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
        this.InitializePools();
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
            this.InitializePools();
        }
    }

    /// <summary> 
    /// Initializing brick pools.
    /// </summary>
    private void InitializePools() {
        this.brickPools = new Dictionary<int, Stack<GameObject>>();
        foreach(int types in Enum.GetValues(typeof(BrickType))) {
            this.brickPools.Add(types, new Stack<GameObject>());
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
                        brickObject = this.GetBrick(BrickType.EnlargePadel);
                    }else {
                        brickObject = this.GetBrick(BrickType.Default);
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
    /// Getting active brick from pool.
    /// </summary>
    /// <returns> 
    /// Brick gameobject.
    /// </returns>
    private GameObject GetBrick(BrickType type) {

        if(this.brickPools[(int) type].Count > 0) {
            GameObject brick = this.brickPools[(int) type].Pop();
            brick.gameObject.SetActive(true);
            return  brick;
        }else {
            foreach(GameObject prefab in this.brickPrefabs) {
                if(prefab.GetComponent<BrickController>().type == type) {
                    return Instantiate(prefab);
                }
            }
            return null;
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

        BrickType brickType = brick.GetComponent<BrickController>().type;
        int size = brickType == BrickType.Default ? this.maxPoolSize : this.maxPowerupPoolSize;

        if(this.brickPools[(int) brickType].Count < size) {
            this.brickPools[(int) brickType].Push(brick);
        }else {
            Destroy(brick);
        }
    }
}