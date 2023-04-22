using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackGenerator : MonoBehaviour
{
    private static StackGenerator instance;

    public static StackGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StackGenerator>();

                if (instance == null)
                {
                    instance = new GameObject("StackGanerator").AddComponent<StackGenerator>();
                }
            }

            return instance;
        }
    }

    public GameObject blockPrefab;

    Vector3[] position = { Vector3.zero, Vector3.zero, Vector3.zero };
    Quaternion[] rotation = { Quaternion.identity, Quaternion.identity, Quaternion.identity };

    public Transform[] blockParent;

    float blockWidth, blockHeight, blockLength;
    int[] currentRow = { 0, 0, 0 };
    int[] blockCount = { 0, 0, 0 };

    int stackIndex;

    private void Start()
    {
        Renderer blockRenderer = blockPrefab.GetComponent<Renderer>();
        blockWidth = blockRenderer.bounds.size.x;
        blockHeight = blockRenderer.bounds.size.y;
        blockLength = blockRenderer.bounds.size.z;
    }

    public void generateStack(List<DataModel> blockData)
    {
        foreach (DataModel obj in blockData)
        {
            if(obj.grade =="6th Grade")
            {
                stackIndex = 0;
                addJengaBlock(obj, stackIndex);
            }
            else if(obj.grade == "7th Grade")
            {
                stackIndex = 1;
                addJengaBlock(obj, stackIndex);
            }
            else if (obj.grade == "8th Grade")
            {
                stackIndex = 2;
                addJengaBlock(obj, stackIndex);
            }
            blockCount[stackIndex]++;
            currentRow[stackIndex] = blockCount[stackIndex] / 3;
            Debug.Log(obj.grade);

        }
    }

    void addJengaBlock(DataModel blockData,int stackIndex)
    {
        if (blockCount[stackIndex] % 3 == 0)
        {
            position[stackIndex] = new Vector3(-blockWidth * 1.5f + stackIndex * 10, blockHeight * currentRow[stackIndex], 0);
            rotation[stackIndex] = Quaternion.identity;
            if (currentRow[stackIndex] % 2 == 1)
            {
                position[stackIndex] = new Vector3(blockWidth * 1.5f + stackIndex * 10 - blockWidth / 2f, blockHeight * currentRow[stackIndex], -blockLength / 3 - blockWidth / 2f);
                rotation[stackIndex] = Quaternion.Euler(0, 90, 0);
            }
        }

        GameObject block = Instantiate(blockPrefab, position[stackIndex], rotation[stackIndex]);

        block.transform.parent = blockParent[stackIndex];

        block.GetComponent<JengaBlockPrefab>().myData = blockData;

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
