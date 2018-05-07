using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{

    public void ViewSO()
    {
        var sos = Array.ConvertAll(Resources.FindObjectsOfTypeAll<ScriptableObject>(), s => s.GetType().FullName);
        Array.Sort(sos);
        var sb = new StringBuilder();
        foreach (var so in sos) sb.AppendLine(so);
        Debug.Log(sb.ToString());
    }

}
