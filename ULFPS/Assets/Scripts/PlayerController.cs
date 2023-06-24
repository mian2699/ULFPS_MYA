using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
     private float speed;

    [SerializeField]
     private float turnSpeed;

    [SerializeField]
     private float shootDistance = 4f;

    [SerializeField]
    private ParticleSystem shootPS;
     [SerializeField]
    private ParticleSystem shootPS2;


     private Rigidbody mRb;
   
    //hacia donde me quiero mover segun las teclas que estoy presionando
    private Vector2 mDirection;
    private Vector2 mDeltaLook;

    private Transform cameraMain;

    private GameObject debugImpactSphere;

    private GameObject bloodObjectParticles;

    private GameObject otherObjectParticles;


     private GameObject debugImpactSphere2;
    //yaranga
    private GameObject bloodObjectParticles2;

    private GameObject otherObjectParticles2;


    public float maxHealth = 100f;

     [SerializeField]
    private float health = 100f;

    //yaranga
   

   

    

    private void Start()
    {
        mRb = GetComponent<Rigidbody>();

        cameraMain = transform.Find("Main Camera");

        debugImpactSphere = Resources.Load<GameObject>("DebugImpactSphere");
        bloodObjectParticles = Resources.Load<GameObject>("BloodSplat_FX Variant");
        otherObjectParticles = Resources.Load<GameObject>("GunShot_Smoke_FX Variant");       
        
    //mya
    debugImpactSphere2 = Resources.Load<GameObject>("DebugImpactSphere2");
        bloodObjectParticles2 = Resources.Load<GameObject>("RocketExplosion");
        otherObjectParticles2 = Resources.Load<GameObject>("RocketMuzzle");

    //mya

        Cursor.lockState = CursorLockMode.Locked;

        //---mya
        
                    //--- mya

                     ChangeWeapon2(1);

               //  health = maxHealth;

             //  DoomCanvas.Instance.UpdateHealth(health);

               DoomCanvas.Instance = GameObject.FindObjectOfType<DoomCanvas>();
    }

    private void Update()
    {

        //movimiento hacia adelante
         mRb.velocity = mDirection.y  * speed * transform.forward 
                        + mDirection.x * speed * transform.right;
         // para revisar la direccion donde mira mi player

       /*  Debug.DrawRay(
            transform.position,
            transform.forward,
            Color.red
         );*/

        transform.Rotate(
                Vector3.up,
                turnSpeed * Time.deltaTime * mDeltaLook.x

        );

        cameraMain.GetComponent<CameraMovement>().RotateUpDown(
                    -turnSpeed * Time.deltaTime * mDeltaLook.y
        );

   //   Debug.Log(mDeltaLook);


   //-yaranga

      //------ yaranga

      if (Input.GetKeyDown (KeyCode.Tab)){

              

            if(weaponSelected != 1){
                        //Debug.Log("Aca1");
                     

                         ChangeWeapon2(1);
                        weaponSelected = 1;
            }

            else{
               // Debug.Log("Aca2");
                       ChangeWeapon2(2);
                        weaponSelected = 2;

            }


        }

    }
        
    private void OnMove(InputValue value)
    {
        mDirection = value.Get<Vector2>();
    }


    private void OnLook(InputValue value)
    {
        mDeltaLook = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed)  
        {
                Shoot();
        }
    }

    private void Shoot()  
    {
        shootPS.Play();
        shootPS2.Play();

        RaycastHit hit;
        
        
        
        
        if (   /* Physics.RayCast(
                    cameraMain.position,//origen del rayo la posicion de la camara
                    cameraMain.forward, // hacia donde apunta la camara 
                    out hit,
                    shootDistance
                )     */ 

                Physics.Raycast(
                            cameraMain.position,
                            cameraMain.forward,
                            out hit,
                            shootDistance

                 )
            
       )

        //Siempre  y cuando haya un impacto 
        {
            // istancia la esfera a la hora de impacto
           // var debugSphere = Instantiate(debugImpactSphere,hit.point,Quaternion.identity);
          //  Destroy(debugSphere,3f);
            //Debug.Log("Pego en algo" + hit.point);

            //---------------------
        
          /*  if (hit.collider.CompareTag("Enemigos"))
            {// si dispara al enemigo sale  la particula de sangre 
                    
                   
                    var bloodPS = Instantiate(bloodObjectParticles, hit.point,Quaternion.identity);
                    Destroy(bloodPS , 3f);
                    // para la vida del enemigo cuando se idspasra  
                    var enemyController = hit.collider.GetComponent<EnemyController>();
                    enemyController.TakeDamage(1f);
            }else
            {// si dispara a otro lado que no sea el enemigo sale el humo  
                
                      var otherPS = Instantiate(otherObjectParticles, hit.point,Quaternion.identity);
                      otherPS.GetComponent<ParticleSystem>().Play();
                    Destroy(otherPS , 3f);
            }*/


             if(weaponSelected != 1){

             //   Debug.Log("Acadisparaescopeta");
                      
                     

                        // ChangeWeapon2(1);
                       // weaponSelected = 1;

                        if (hit.collider.CompareTag("Enemigos"))
                        {// si dispara al enemigo sale  la particula de sangre 
                                
                            
                                var bloodPS = Instantiate(bloodObjectParticles, hit.point,Quaternion.identity);
                                Destroy(bloodPS , 3f);
                                // para la vida del enemigo cuando se idspasra  
                                var enemyController = hit.collider.GetComponent<EnemyController>();
                                enemyController.TakeDamage(1f);
                        }else
                        {// si dispara a otro lado que no sea el enemigo sale el humo  
                            
                                var otherPS = Instantiate(otherObjectParticles, hit.point,Quaternion.identity);
                                otherPS.GetComponent<ParticleSystem>().Play();
                                Destroy(otherPS , 3f);
                        }

            }

             else{
                       // Debug.Log("disparapistola");

                        shootDistance = 15f;            
                        if (hit.collider.CompareTag("Enemigos"))
                        {// si dispara al enemigo sale  la particula de sangre 
                                
                            
                                var bloodPS2 = Instantiate(bloodObjectParticles2, hit.point,Quaternion.identity);
                                Destroy(bloodPS2 , 5f);
                                // para la vida del enemigo cuando se idspasra  
                                var enemyController = hit.collider.GetComponent<EnemyController>();
                                enemyController.TakeDamage(5f);
                        }else
                        {// si dispara a otro lado que no sea el enemigo sale el humo  
                                //  Debug.Log("disparapistolaNotEnemy");
                                var otherPS2 = Instantiate(otherObjectParticles2, hit.point,Quaternion.identity);
                                otherPS2.GetComponent<ParticleSystem>().Play();
                                Destroy(otherPS2 , 5f);
                        }



            }

        }



    }

    public void TakeDamage(float damage)
    {
      
     // vida del player
        health -= damage;
        Debug.Log("vida:" + health);
        if (health <= 0f){

            //fin del juego
           // Debug.Log("FIN DEL JUEGO"+health+ " * "+damage);
        }

        DoomCanvas.Instance.UpdateHealth(health);
    }


    private void OnTriggerEnter(Collider col){

        if (col.CompareTag("Enemigo-Attack"))
        {       
          //  Debug.Log("Player recibe danho");
                 TakeDamage(1f);

        }
           

    }




    // ---- yaranga cambio de arma 

    
   int weaponSelected = 1 ;

   [SerializeField]
   public GameObject primary , secondary;

  




   void ChangeWeapon2(int weaponType)
    {
        if (weaponType == 1 ){

            primary.SetActive(true);
            secondary.SetActive(false);
        }

        if (weaponType ==2){
            primary.SetActive(false);
            secondary.SetActive(true);
        }
        
    }
}
