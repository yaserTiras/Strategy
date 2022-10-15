using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Item
{

    public Cell currentCell;
    private IEnumerator travelNumerator;
    private Collider2D myCollider;
    protected override void SetUp()
    {
        Dimensions = new int[1, 1];

        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
    }

    public override void CheckHittedCell()
    {

    }

    public override void OnClick()
    {
        if (IsPlaced)
        {
            // Set To Move
        }
        else
        {
            ItemsMessenger.instance.ProduceItem(this);
        }
    }

    public override void OnProduce(Cell spawnedCell,Cell targetCell)
    {
        IsPlaced = true;      
        transform.position = spawnedCell.transform.position;
        myCollider.enabled = true;
        gameObject.SetActive(true);
        currentCell = spawnedCell;
        Travel(targetCell);
    }

    public override void Travel(Cell target)
    {
        if (travelNumerator != null)
            StopCoroutine(travelNumerator);
        travelNumerator = Traveler(Pathfinder.FindPath(currentCell, target));
        StartCoroutine(travelNumerator);
    }

    private IEnumerator Traveler(List<Cell> path)
    {
        if (path == null)
            yield break;
        int currentNode = path.Count-1;
        while (currentNode >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentNode].transform.position, 20f * Time.deltaTime);

            if (Vector3.Distance(transform.position, path[currentNode].transform.position) <= 2f)
            {
                currentNode--;
            }
            yield return null;
        }
        currentCell = path[0];
    }

    private void OccupyCell(Cell cell)
    {
        cell.Walkable = false;
    }

    public override void OnEnable()
    {

    }

    public override void OnDisable()
    {

    }
}
