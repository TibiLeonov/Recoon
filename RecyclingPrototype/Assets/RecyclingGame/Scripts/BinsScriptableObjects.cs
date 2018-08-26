using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBin", menuName = "Bin")]
public class BinsScriptableObjects : ScriptableObject {

    public new string name;
    public Bin.binTypes binType;

    /*
    public GameObject childCollider;
    public Mesh mesh;
    public Material material;
    */
   
}
