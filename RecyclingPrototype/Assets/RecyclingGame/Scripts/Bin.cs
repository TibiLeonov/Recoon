using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {

    public enum binTypes {Compost, Recycling, Hazard, Donation, Garbage};

    public BinsScriptableObjects binData;

   
    /*public GameObject childCollider;
    public Mesh mesh;
    public Material material;
    */
    public binTypes binType;

    public SpriteRenderer correctSprite;
    public SpriteRenderer inCorrectSprite;

    void Start () {
        
        LoadData();
	}
	
    //this is for loading scriptable object data
    public void LoadData()
    {
        //still needs to load colliders and meshes  and materials------------------
        binType = binData.binType;

        //collider
       /* childCollider = binData.childCollider;
        if (GetComponentInChildren<Collider>())
        {
            Transform oldCollider = GetComponentInChildren<Collider>().gameObject.transform;
            oldCollider.parent = null;
            Destroy(oldCollider.gameObject);
        }
        GameObject newCollider = Instantiate(childCollider, this.transform);
        GetComponentInParent<Rigidbody>().ResetCenterOfMass();


        //model

        mesh = binData.mesh;
        GetComponentInChildren<MeshFilter>().mesh = mesh;

        //material
        material = binData.material;
        GetComponentInChildren<MeshRenderer>().material = material;
        */
    }
	
	void Update () {
		
	}
     /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (other.gameObject.GetComponentInChildren<Throwable>())
            {
                Throwable thrown = other.gameObject.GetComponentInChildren<Throwable>();
                if (thrown.properBin == binType)
                {
                    //point up
                    //particle
                    Debug.Log("yay");
                    iTween.ScaleTo(thrown.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1.5f, "easetype", "easeoutcubic", "oncomplete", "DeleteObject", "oncompleteparams", thrown.gameObject));
                    //temporary delete animation
                    pointsAndTimeTracker.AddPoints(thrown.pointsWorth);


                }
                else if (thrown.secondaryBin && thrown.secondaryBinType == binType)
                {
                    iTween.ScaleTo(thrown.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1.5f, "easetype", "easeoutcubic", "oncomplete", "DeleteObject", "oncompleteparams", thrown.gameObject));
                    //temporary delete animation
                    pointsAndTimeTracker.AddPoints(thrown.secondaryPointsWorth);
                }
                else
                {
                    Debug.Log(thrown.disposeInstructions);
                    Debug.Log(thrown.properBin);
                    Debug.Log(binType);
                    //bad recycle
                }
            }

        }
    }*/

    public void DisplayIcon(bool correct)
    {
        if (correct)
        {
            correctSprite.GetComponent<UI_PopUp>().PopUpImage(2);
        }
        else
        {
            inCorrectSprite.GetComponent<UI_PopUp>().PopUpImage(2);
        }
    }

    void DeleteObject(GameObject target)
    {
        Destroy(target);
    }

}
