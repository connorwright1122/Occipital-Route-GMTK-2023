using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    private MazeCell[,] _mazeGrid;

    [SerializeField]
    private GameObject _mazeHolder;

    //[SerializeField]
    public NavMeshSurface surface;

    public Material[] materials;


    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                _mazeGrid[x, z].transform.parent = _mazeHolder.transform;

                int xC = x.CompareTo(_mazeWidth / 2);
                int zC = z.CompareTo(_mazeDepth / 2);

                Color targetColor;
                Material mat;

                if (xC > 0 && zC > 0)
                {
                    targetColor = Color.red;
                    mat = materials[0];
                }
                else if (xC < 0 && zC > 0)
                {
                    targetColor = Color.yellow;
                    mat = materials[1];
                }
                else if (xC < 0 && zC < 0)
                {
                    //targetColor = Color.red;
                    targetColor = Color.green;
                    mat = materials[2];
                }
                else if (xC > 0 && zC < 0)
                {
                    //targetColor = Color.yellow;
                    targetColor = Color.blue;
                    mat = materials[3];
                }
                else
                {
                    targetColor = Color.gray;
                    mat = materials[0];
                }

                Renderer[] rs = _mazeGrid[x, z].GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in rs)
                {
                    renderer.material = mat;
                    //Material m = renderer.material;
                    //m.color = targetColor;
                    renderer.material.color = targetColor;
                }
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);

        surface.BuildNavMesh();

        _mazeHolder.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
    }

    private void RandomColor()
    {

    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.isVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.isVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.isVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.isVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

}
