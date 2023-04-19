using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour

{

    [SerializeField]
    private int width, height;
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private Transform cam;
    [Tooltip("Dictionary of tiles, key is (x, y) coordinates")]
    [SerializeField]
    private Dictionary<Vector2, Tile> tileDict;

    void generateGrid()
    {
        // Instantiate the dictionary
        tileDict = new Dictionary<Vector2, Tile>();
        // create the tiles, naming them (x, y) coordinates
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                Tile spawnTile = Instantiate(tilePrefab, new Vector3(i, j), Quaternion.identity);
                spawnTile.name = $"Tile{i} {j}";

                // Check for offset which renders a different color
                bool isOffset = (i % 2 == 0 & j % 2 == 1) || (i % 2 == 1 & j % 2 == 0);
                spawnTile.Init(isOffset);

                // Store tiles into the dictionary
                tileDict[new Vector2(i, j)] = spawnTile;

            }
        }


        // set camera to middle of the grids
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10f);
    }

    // get tile by pos vector2
    public Tile getTileAtPos(Vector2 pos)
    {
        if (tileDict.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }
        return null;

    }
    // Start is called before the first frame update
    void Start()
    {
        generateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
