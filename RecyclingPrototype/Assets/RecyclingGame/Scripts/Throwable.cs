using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    public ThrowablesScriptableObject throwableData;

    public Bin.binTypes properBin;
    public Bin.binTypes secondaryBinType;
    public string disposeInstructions;
    public bool secondaryBin = false;
    public int pointsWorth;
    public int secondaryPointsWorth;
    public GameObject childCollider;
    public Mesh mesh;
    public Material material;

    public PointsAndTime pointsAndTimeTracker;

    public bool gettingDestroyed;



    void Start()
    {
       
        LoadData();
    }


    void Update()
    {

    }

    public void LoadData()
    {
        //collider
        childCollider = throwableData.childCollider;
        if (GetComponentInChildren<Collider>())
        {
            Transform oldCollider = GetComponentInChildren<Collider>().gameObject.transform;
            oldCollider.parent = null;
            Destroy(oldCollider.gameObject);
        }
        GameObject newCollider = Instantiate(childCollider, this.transform);
        GetComponentInParent<Rigidbody>().ResetCenterOfMass();

        //trash type
        properBin = throwableData.properBin;
        disposeInstructions = throwableData.disposeInstructions;
        pointsWorth = throwableData.pointsWorth;
        secondaryBin = throwableData.secondaryBin;
        if (secondaryBin)
        {
            secondaryBinType = throwableData.secondaryBinType;
            secondaryPointsWorth = throwableData.secondaryPointsWorth;
        }

        //model

        mesh = throwableData.mesh;
        GetComponentInChildren<MeshFilter>().mesh = mesh;

        //material
        material = throwableData.material;
        GetComponentInChildren<MeshRenderer>().material = material;
    }

  
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponentInParent<Bin>())
        {
            if (!gettingDestroyed)
            {
                gettingDestroyed = true;

                
                transform.parent = null;
                if (gameObject.GetComponentInParent<PlayerMovement>())
                {
                    gameObject.GetComponentInParent<PlayerMovement>().holdingItem = false;
                }

                Bin target = other.gameObject.GetComponentInParent<Bin>();
                if (target.binType == properBin)
                {
                    //point up
                    //particle
                    Debug.Log("yay");
                    //temporary delete animation
                    pointsAndTimeTracker.AddPoints(pointsWorth);

                    //show the icon
                    target.DisplayIcon(true);

                }
                else if (secondaryBin && secondaryBinType == target.binType)
                {
                    //temporary delete animation
                    pointsAndTimeTracker.AddPoints(secondaryPointsWorth);

                    target.DisplayIcon(true);
                }
                else
                {
                   
                    Debug.Log(disposeInstructions);
                    Debug.Log(properBin);
                    Debug.Log(target.binType);
                    //bad recycle
                    target.DisplayIcon(false);
                }
               

                iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero, "time", 1.5f, "easetype", "easeoutcubic", "oncomplete", "DeleteObject", "oncompleteparams", gameObject));

            }
        }


    }
}
