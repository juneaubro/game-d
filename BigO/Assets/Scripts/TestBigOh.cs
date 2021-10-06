using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBigOh : MonoBehaviour
{
    public int numberOfElements = 1000;
    ArrayList arrayList;

    void Start()
    {
        print("Populating the Array List with " + numberOfElements);
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        arrayList = new ArrayList();
        for (int i = 0; i < numberOfElements; i++) {
            arrayList.Add(UnityEngine.Random.value);
        }
        long t21 = stopwatch.ElapsedMilliseconds;

        print("Sorting the Array List with " + numberOfElements);
        arrayList.Sort();
        long t32 = stopwatch.ElapsedMilliseconds;

        print("Populating time t21 = " + t21.ToString());
        print("Sorting time t31 = " + t32.ToString());

        object[] arr = arrayList.ToArray();
        MySort(arr);
        long t43 = stopwatch.ElapsedMilliseconds;
        print("Sorting time t43 = " + t43.ToString());
    }

    void MySort(object[] arr) {
        for (int i = 0; i < arr.Length - 1; i++) {
            float a = (float)arr[i];
            for(int j = i + 1; j < arr.Length; j++) {
                float b = (float)arr[j];
                if(b < a) {
                    object temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        }
    }
}
