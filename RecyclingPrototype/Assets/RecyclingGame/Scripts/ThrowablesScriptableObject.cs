using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewThrowable", menuName ="Throwable")]
public class ThrowablesScriptableObject : ScriptableObject {

    public new string name;
    public Bin.binTypes properBin;
    public bool secondaryBin=false;
    public Bin.binTypes secondaryBinType;
    public string disposeInstructions;
    public int pointsWorth;
    public int secondaryPointsWorth;
    public GameObject childCollider;
    public Mesh mesh;
    public Material material;

}
