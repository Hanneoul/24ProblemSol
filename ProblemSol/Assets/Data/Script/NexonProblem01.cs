using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexonProblem01 : MonoBehaviour
{
    int generator(int num)
    {
        int cal = num;
        int result = num;

        while(true)
        {
            if (cal % 10 == 0 && cal / 10 == 0)
                break;
            result += cal % 10;
            cal = cal / 10;
        }

        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        int[] a = new int[5000];
        for(int i = 0; i<5000;i++ )
        {
            a[i] = i + 1; //(1~5000)
        }

        for (int j = 1; j < 5001; j++)
        {
            int bigyo = generator(j);
            for (int i = 0; i < 5000; i++)
            {
                if (bigyo == a[i])
                {
                    a[i] = 0;
                }
            }
        }

        int FinalResult = 0;
        for (int i = 0; i < 5000; i++)
        {
            FinalResult += a[i];
        }

        Debug.Log(FinalResult);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
