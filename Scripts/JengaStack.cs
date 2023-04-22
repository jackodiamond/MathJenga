using UnityEngine;

public class JengaStack : MonoBehaviour
{
    public GameObject blockPrefab; // The prefab for the Jenga block
    public int rows = 10; // The number of rows in the Jenga stack

    Vector3[] position = { Vector3.zero, Vector3.zero, Vector3.zero };
    Quaternion[] rotation= {Quaternion.identity, Quaternion.identity, Quaternion.identity };

    public Transform[] blockParent;

    float blockWidth, blockHeight, blockLength;
    int[] currentRow = { 0, 0, 0 };
    int[] blockCount= { 0, 0, 0 };
    
    void Start()
    {
        // Get the width and height of the Jenga block from the prefab
        blockWidth = blockPrefab.GetComponent<Renderer>().bounds.size.x;
        blockHeight = blockPrefab.GetComponent<Renderer>().bounds.size.y;
        blockLength = blockPrefab.GetComponent<Renderer>().bounds.size.z;

        int stackIndex = 0;
        for (int i = 0; i < rows; i++)
        {
            addBlock(stackIndex);
            blockCount[stackIndex]++;
            currentRow[stackIndex] = blockCount[stackIndex] / 3;
        }

        stackIndex = 1;
        for (int i = 0; i < rows; i++)
        {
            addBlock(stackIndex);
            blockCount[stackIndex]++;
            currentRow[stackIndex] = blockCount[stackIndex] / 3;
        }

        stackIndex = 2;
        for (int i = 0; i < 25; i++)
        {
            addBlock(stackIndex);
            blockCount[stackIndex]++;
            currentRow[stackIndex] = blockCount[stackIndex] / 3;
        }
    }


    void addBlock(int stackIndex)
    {
        if (blockCount[stackIndex] % 3 == 0)
        {
            position[stackIndex] = new Vector3(-blockWidth * 1.5f +stackIndex*10, blockHeight * currentRow[stackIndex], 0);
            rotation[stackIndex] = Quaternion.identity;
            if (currentRow[stackIndex] % 2 == 1)
            {
                position[stackIndex] = new Vector3(blockWidth * 1.5f + stackIndex * 10 - blockWidth / 2f, blockHeight * currentRow[stackIndex], -blockLength / 3 - blockWidth / 2f);
                rotation[stackIndex] = Quaternion.Euler(0, 90, 0);
            }
        }

        GameObject block = Instantiate(blockPrefab, position[stackIndex], rotation[stackIndex]);

        block.transform.parent = blockParent[stackIndex];

        if (currentRow[stackIndex] % 2 == 0)
        {
            position[stackIndex].x += blockWidth + blockLength / 4;
        }
        else
        {
            position[stackIndex].z += blockWidth + blockLength / 4;
        }
    }
}