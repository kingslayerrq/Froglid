using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GameObject uPrefab_Mal;
    [SerializeField] private GameObject uPrefab_Bene;

    #region Singleton
    public static UnitManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public void spawnUnit()
    {
        var bUnit = Instantiate(uPrefab_Bene,);
        bUnit.transform.SetParent(GridManager.Instance.getNodeByPos(0, 0).GetComponentInChildren<HorizontalLayoutGroup>().transform);
        var mUnit = Instantiate(uPrefab_Mal);
        mUnit.transform.SetParent(GridManager.Instance.getNodeByPos(8, 0).GetComponentInChildren<HorizontalLayoutGroup>().transform);
    }
}
