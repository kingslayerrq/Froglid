using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour

{

    [SerializeField]//private int width, height;
    private int mapSize;
    
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private int xOffset,yOffset;
    [SerializeField]
    private Transform cam;
    [Tooltip("Dictionary of tiles, key is (x, y) coordinates")]
    [SerializeField]
    private Dictionary<Vector2, Tile> tileDict;

    void generateGrid(int size)
    {
        // Instantiate the dictionary
        //tileDict = new Dictionary<Vector2, Tile>();

        //first half of the diamond
        double xPos = 0;
        double yPos = 0;
        int space = size - 1;
        for (int i = 0; i < size ; i++)
        {
            xPos += 0.8 * space; //adjust x
            for (int j = 0; j <= i; j++)
            {

                var tile = Instantiate(tilePrefab, new Vector2((float)xPos, (float)yPos), Quaternion.identity);
                tile.name = $"Tile{xPos} {yPos}";
                xPos += 0.8*2; //next tile in the same row
            }
            xPos = 0;
            yPos -= 1; // tile height 
            space--;
            
        }
        //bottom half
        space = 1;
        for (int i = size - 1; i > 0; i--)
        {
            xPos += 0.8 * space;
            for (int j = 0; j < i; j++)
            {
                var tile = Instantiate(tilePrefab, new Vector2((float)xPos, (float)yPos), Quaternion.identity);
                tile.name = $"Tile{xPos} {yPos}";
                xPos += 0.8 * 2;
            }
            xPos = 0;
            yPos -= 1;
            space++;
        }
        


        // set camera to middle of the grids
        cam.transform.position = new Vector3((float)(size-1)*0.8f, (float)-(size-1), -10f);
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
        generateGrid(5);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
