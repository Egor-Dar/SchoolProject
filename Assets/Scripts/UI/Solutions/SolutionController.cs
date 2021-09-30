using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MathfClass;

public class SolutionController : MonoBehaviour
{
    [SerializeField] private InputField InputFieldValue1;
    [SerializeField] private InputField InputFieldValue2;
    [SerializeField] private InputField InputFieldResult;
    private int value1;
    private int value2;
    private int result;

    public void Examination()
    {
        value1 = Convert.ToInt32(InputFieldValue1.text);
        value2 = Convert.ToInt32(InputFieldValue2.text);
        result = Convert.ToInt32(InputFieldResult.text);
        if (MathClass.DifferenceNumber(value1, value2) == result)
        {
            Debug.Log("Урон засчитан");
        }
        else
        {
            Debug.Log("Урон не засчитан");
        }
    }
}
