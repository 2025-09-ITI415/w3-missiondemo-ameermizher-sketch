 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class Slingshot : MonoBehaviour {
     
      void OnMouseEnter() {
         print( "Slingshot:OnMouseEnter()" );
     }
 
     void OnMouseExit() {
         print( "Slingshot:OnMouseExit()" );
     }
 void OnMouseEnter() {
    //print("Slingshot:OnMouseEnter()");
    launchPoint.SetActive( true );
}
 
void OnMouseExit() {
    //print("Slingshot:OnMouseExit()");
    launchPoint.SetActive( false );
}
     // void Start() {…}  // Please delete the unused Start() and Update() methods
     // void Update() {…}
     
      
         public GameObject launchPoint;

     void Awake() {
         Transform launchPointTrans = transform.Find("LaunchPoint");     // a
         launchPoint = launchPointTrans.gameObject;
         launchPoint.SetActive( false );                                 // b
     }

     void OnMouseEnter() {
         //print("Slingshot:OnMouseEnter()");
         launchPoint.SetActive( true );                                  // b
     }

     void OnMouseExit() {
        //print("Slingshot:OnMouseExit()");
         launchPoint.SetActive( false );                                // b
     }
 }
      // fields set in the Unity Inspector pane 
     [Header("Inscribed")]                                                       // a
     public GameObject        projectilePrefab;
 
     // fields set dynamically
     [Header("Dynamic")]                                                         // a
     public GameObject        launchPoint;
     public Vector3           launchPos;                                         // b
     public GameObject        projectile;                                        // b
     public bool              aimingMode;                                        // b
 
     void Awake() {
         Transform launchPointTrans = transform.Find("LaunchPoint");
         launchPoint = launchPointTrans.gameObject;
         launchPoint.SetActive( false );
         launchPos = launchPointTrans.position;                                 // c
     }
         void OnMouseEnter() { … }  // Do not change OnMouseEnter()
 
     void OnMouseExit() { … }   // Do not change OnMouseExit()
     
     void OnMouseDown() {                                                       // d
         // The player has pressed the mouse button while over Slingshot
         aimingMode = true;
         // Instantiate a Projectile
         projectile = Instantiate( projectilePrefab ) as GameObject;
         // Start it at the launchPoint
         projectile.transform.position = launchPos;
         // Set it to isKinematic for now
         projectile.GetComponent<Rigidbody>().isKinematic = true;               // e
     }
 
}