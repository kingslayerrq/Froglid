using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        GridManager.Instance.generateGrid(5);
        GridManager.Instance.generateEdges();
        UnitManager.Instance.spawnUnit();
    }

}
