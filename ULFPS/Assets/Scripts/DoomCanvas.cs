using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DoomCanvas : MonoBehaviour
{
   public Image healthIndicator;

   public Sprite health1;
   public Sprite health2;
   public Sprite health3;
   public Sprite health4;
 
 /*  private static CanvasManager  _instance;
   public static CanvasManager  Instance
   { get {return _instance;}
   
   }*/

   private static DoomCanvas _instance;
    //public static DoomCanvas Instance { get { return _instance; } }

       public static DoomCanvas Instance { get { return _instance; } set { _instance = value; } }

/*
 private void Awake(){

   if 
 }*/
   public void UpdateHealth(float healthValue){

         UpdateHealthIndicator(healthValue);

   }

   public void  UpdateHealthIndicator(float healthValue)
   {
         if (healthValue>=66f) 
         {
               healthIndicator.sprite = health1;
         }

         if (healthValue<66f && healthValue>= 33f  ) 
         {
               healthIndicator.sprite = health2;
         }

         if (healthValue<33f && healthValue> 0f ) 
         {
               healthIndicator.sprite = health3;
         }

         if (healthValue<= 0f )
         {
               healthIndicator.sprite = health4;
         }

   }

   





}
