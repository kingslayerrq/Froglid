using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour

{
    [SerializeField] private int mapSize;
    [SerializeField] private Node nodePrefab;
    [SerializeField] private int xOffset, yOffset;
    [SerializeField] private Transform cam;

    
    [SerializeField] private List<Node> nodeList;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject nodeGrp;
    [SerializeField] private GameObject edgeGrp;
    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private GameObject arrPrefabBotLeft;
    [SerializeField] private GameObject arrPrefabBotRight;
    [SerializeField] private GameObject arrPrefabTopLeft;
    [SerializeField] private GameObject arrPrefabTopRight;
    [Tooltip("Adjacency matrix for the nodes: 1=>an edge is present")]
    public int[,] edgeMatrix;

    #region Singleton
    public static GridManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public void generateGrid(int size)
    {
        int k = 0, rowInd = 0, colInd = 0;
        nodeList = new List<Node>();                                                                // Instantiate the dictionary                                                              
        double xPos = 0;
        double yPos = 0;
        int space = size - 1;
        for (int i = 0; i < size ; i++)                                                                         //first half of the diamond
        {
            xPos += 0.8 * space; //adjust x
            for (int j = 0; j <= i; j++)
            {
                var node = Instantiate(nodePrefab, new Vector2((float)xPos, (float)yPos), Quaternion.identity);
                node.id = k;
                node.rowIndex = rowInd;
                node.columnIndex = colInd;
                node.transform.SetParent(nodeGrp.transform);
                nodeList.Add(node);
                node.name = $"Node{rowInd}:{colInd}:{k}";
                xPos += 0.8*2;                                                                                 //next Node in the same row
                k++;
                colInd++;
            }
            colInd = 0;                                                                                        // reset colIndex
            rowInd++;
            xPos = 0;
            yPos -= 1;                                                                                         // Node height 
            space--;  
        }
        //bottom half
        space = 1;
        for (int i = size - 1; i > 0; i--)
        {
            xPos += 0.8 * space;
            for (int j = 0; j < i; j++)
            {
                var node = Instantiate(nodePrefab, new Vector2((float)xPos, (float)yPos), Quaternion.identity);
                node.id = k;
                node.rowIndex = rowInd;
                node.columnIndex = colInd;
                node.transform.SetParent(nodeGrp.transform);
                nodeList.Add(node);
                node.name = $"Node{rowInd}:{colInd}:{k}";
                xPos += 0.8 * 2;
                k++;
                colInd++;
            }
            colInd = 0;                                                                                         
            rowInd++;
            xPos = 0;
            yPos -= 1;
            space++;
        }
        // set camera to middle of the grids
        cam.transform.position = new Vector3((float)(size-1)*0.8f, (float)-(size-1), -10f);
    }

    // get tile by index
    public Node getNodeByPos(int rowInd, int colInd)
    {
        return nodeList.Where(n=> n.rowIndex == rowInd && n.columnIndex == colInd).FirstOrDefault();
    }

    public List<Node> getNodeByRow(int rowInd)
    {
        return nodeList.Where(n => n.rowIndex == rowInd).OrderBy(n=>n.columnIndex).ToList<Node>();
    }

    [Tooltip("int parameter indicates arrow direction {0=>botLeft, 1=>botRight, 2=>topLeft, 3=>topRight")] 
    public void createEdge(Node start, Node end, int i)
    {
        //LineRenderer lineRenderer = Instantiate(lineRendererPrefab, canvas.transform);
        //lineRenderer.transform.SetParent(canvas.transform);
        //lineRenderer.positionCount = 2;
        //lineRenderer.SetPosition(0, start.transform.position);
        //lineRenderer.SetPosition(1, end.transform.position);


        Vector3 dir = end.transform.position - start.transform.position;
        float distance = dir.magnitude;
        

        
        Vector3 pos = start.transform.position + dir/2;
        Debug.Log(pos);
        GameObject arr;
        switch (i)
        {
            case 0:
                arr = Instantiate(arrPrefabBotLeft, pos, Quaternion.LookRotation(end.transform.position), edgeGrp.transform);
                break;
            case 1:
                arr = Instantiate(arrPrefabBotRight, pos, Quaternion.LookRotation(end.transform.position), edgeGrp.transform);
                break;
            case 2:
                arr = Instantiate(arrPrefabTopLeft, pos, Quaternion.LookRotation(end.transform.position), edgeGrp.transform);
                break;
            case 3:
                arr = Instantiate(arrPrefabTopRight, pos, Quaternion.LookRotation(end.transform.position), edgeGrp.transform);
                break;
            default:
                arr = null;
                break;
        }

        arr.transform.rotation = Quaternion.Euler(arr.transform.rotation.x, 0, arr.transform.rotation.y);

 
    }

    public void generateEdges()
    {
        List<Node> start = new List<Node>();
        List<Node> end = new List<Node>();
        edgeMatrix = new int[mapSize * mapSize,  mapSize* mapSize];

        for (int i = 0; i < mapSize - 1; i++)
        {
            start = getNodeByRow(i);
            foreach(Node node in start)
            {
                var endNode = getNodeByPos(i + 1, node.columnIndex);
                createEdge(node, endNode, 0);
                edgeMatrix[node.id,endNode.id] = 1;
                endNode = getNodeByPos(i + 1, node.columnIndex + 1);
                createEdge(node, endNode, 1);
                edgeMatrix[node.id, endNode.id] = 1;
            }
        }

        for (int i = 2*(mapSize - 1); i > mapSize - 1; i--)
        {
            start = getNodeByRow(i);
            foreach(Node node in start)
            {
                var endNode = getNodeByPos(i - 1, node.columnIndex);
                createEdge(node, endNode, 2);
                edgeMatrix[node.id, endNode.id] = 1;
                endNode = getNodeByPos(i - 1, node.columnIndex + 1);
                createEdge(node, endNode, 3);
                edgeMatrix[node.id, endNode.id] = 1;
            }
        }


    }
}
