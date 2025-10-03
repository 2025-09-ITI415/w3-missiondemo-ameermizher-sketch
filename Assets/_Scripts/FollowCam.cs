using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S;           // Singleton
    static public GameObject POI;        // Point of Interest

    [Header("Inscribed")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    public GameObject viewBothGO;

    [Header("Dynamic")]
    public float camZ;

    public eView nextView = eView.slingshot;

    public enum eView
    {
        slingshot,
        castle,
        both,
        none
    }

    private eView view = eView.slingshot;

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        if (POI == null) return;

        Vector3 destination = POI.transform.position;

        Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
        if ((poiRigid != null) && poiRigid.IsSleeping())
        {
            POI = null;
            return;
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;

        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }

    //  Add this method so MissionDemolition can call SWITCH_VIEW
    public void SwitchView(eView newView)
    {
         if ( newView == eView.none ) {
             newView = nextView;
         }

        switch (view)
        {
            case eView.slingshot:
                POI = null; // default to slingshot (camera stays on starting area)
                nextView = eView.castle; 
                break;

            case eView.castle:
                POI = MissionDemolition.GET_CASTLE(); // follow castle
                nextView = eView.both;
                break;

            case eView.both:
                POI = GameObject.Find("ViewBoth"); // optional: create an empty GameObject in scene
                nextView = eView.slingshot;
                break;
        }
    }
    public void SwitchView() {                                                  // i
         SwitchView( eView.none );
     }
 
     static public void SWITCH_VIEW( eView newView ) {
        Debug.Log("S is" + S);                      // j
        S.SwitchView( newView );
     }
}
