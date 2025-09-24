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
 