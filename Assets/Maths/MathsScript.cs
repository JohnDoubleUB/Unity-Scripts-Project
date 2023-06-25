using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetPower();

        //Square root of 10000
        print("Square root: " + Mathf.Pow(10000f, 1f/2f)); //Power is multiplied by it self x amount of times
        print("Square root: " + Mathf.Sqrt(10000f));
        print("Cube root: " + Mathf.Pow(10000f, 1f / 2f));

        //Challenges: Make a square a number inferer using a grid
        //I.e: 3 squared would be a 3x3 grid
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetPower() 
    {
        print("Power: " + Mathf.Pow(2, 24));
    }
}
