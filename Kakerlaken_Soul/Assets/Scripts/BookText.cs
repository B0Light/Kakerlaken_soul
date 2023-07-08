using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BookComment", menuName = "Scriptable Object/Book Data"
    ,order = int.MaxValue)]
public class BookText : ScriptableObject
{
    [SerializeField] private string text1;
    [SerializeField] private string text2;
    [SerializeField] private string text3;
    [SerializeField] private string text4;
    public string Text1
    {
        get { return text1; }
    }

    public string Text2
    {
        get { return text2; }
    }

    public string Text3
    {
        get { return text3; }
    }

    public string Text4
    {
        get { return text4; }
    }
}
