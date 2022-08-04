using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="new dialogue",menuName ="Dialogue/dialogueData")]
public class DialogData_so : ScriptableObject
{
    public List<string> dialogueList = new List<string> ();
}
