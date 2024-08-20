using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [ContextMenu("Teeest")]
    private void Test()
    {
        int[,] defaultData = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        int[,] testData = new int[3, 3];
        Array.Copy(defaultData,0, testData,0, 8);
        //Array.;



        for (int x = 0; x < 3; x++)
        {
            string text = "";
            for (int z = 0; z < 3; z++)
            {
                text += testData[x, z];
            }
            print(text);
        }
    }
}
