using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   public float Speed=2f;
   public float AwakeRadio = 2f;

   public float AttackRadio = 0.5f;

   public float Health = 5f;

  
   public GameObject hitboxRight;
   public GameObject hitboxLeft;

   private Animator mAnimator;
  
   private Rigidbody mRb;
   private Vector2 mDirection; // plano x z 

   private bool mIsAttacking = false;

   private UnityEngine.AI.NavMeshAgent navMeshAgent;




   private void Start()
   {

        mRb = GetComponent<Rigidbody>();
        mAnimator = transform.GetComponentInChildren<Animator>(false);
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
     
        /*            .Find("Parasite L Starkie")
                    .GetComponent<Animator>();*/


   }

   
   private void Update()
   {
  
  
  
        var collider1 = IsPlayerInAttackArea();
        if (collider1 != null && !mIsAttacking)
        {


           //  Debug.Log("ACA");
               mRb.velocity = new Vector3(
                // eje x * velocidad del enemigo * 
                    0f,
                    0f,
                   0f
                    
            );   
           navMeshAgent.isStopped = true;
              mAnimator.SetBool("IsWalking", false);
              
              mAnimator.SetTrigger("Attack");
              return;
        }

         var collider2 = IsPlayerNearby(); // var 

        // si encuentra  al jugador en su radar 

        // si vamos avanzar cuando el personaje esta cerca y no esta atacando 
        if (collider2 != null && !mIsAttacking)
        {

            //Debug.Log("ACA2");
           mAnimator.SetBool("IsWalking", true);
             navMeshAgent.isStopped = false;
          navMeshAgent.SetDestination(collider2.transform.position);
            //Walk(collider2);

                
       

        }
        else
        {
                //si no esta en su radar 
                // para de caminar
          // velocidad
            ///   Debug.Log("ACA3  ");
            mRb.velocity =  Vector3.zero;

                //parar
                mAnimator.SetBool("IsWalking",false);
                navMeshAgent.isStopped = true;
                navMeshAgent.ResetPath();
               // navMeshAgent.SetDestination(null);

                 
        }

      //  Debug.Log(colliders.Length);

   

   }

    private void NewMethod(Collider collider2)
    {
        // agregarle direccion de movimiento al caminar
        var playerPosition = collider2.transform.position;//la posicion del jugador
        var direction = playerPosition - transform.position;// la posicion del jugador - nuestra posicion // plano x z 
        mDirection = new Vector2(direction.x, direction.z);

        // velocidad
        direction.y = 0f;

        transform.rotation = Quaternion.Lerp(transform.rotation,
                                                Quaternion.LookRotation(direction, Vector3.up),
                                                0.1f);
        mRb.velocity = new Vector3(
                // eje x * velocidad del enemigo * 
                mDirection.x * Speed,
                0f,
                mDirection.y * Speed

        );

        // caminar
        mAnimator.SetBool("IsWalking", true);
        mAnimator.SetFloat("Horizontal", mDirection.x); // direccion
        mAnimator.SetFloat("Vertical", mDirection.y);// direccion
    }

    private Collider IsPlayerNearby(){

 // nuestro enemeigo haga un cast a su alreadedor si nuestro persona entra persigue al jugador
    //  se inicia con 0 , pero cuando me acerco al jugador la variable colliders toma el valor de 1
            var colliders = Physics.OverlapSphere(
                                 transform.position,
                                AwakeRadio,
                                LayerMask.GetMask("Player") // se agrego layers Player
                            );

          //  Debug.Log(colliders.Length);

            // quiero saber con quien he chocado para saber su posicion y luego perseguirlo

            // si encuentra al player retorna valor 
            if (colliders.Length ==1) return colliders[0];

            // caso contrario
            else  return null ;

            //return colliders.Length ==1;


            
    }


    private Collider IsPlayerInAttackArea(){

 // nuestro enemeigo haga un cast a su alreadedor si nuestro persona entra persigue al jugador
    //  se inicia con 0 , pero cuando me acerco al jugador la variable colliders toma el valor de 1
            var colliders = Physics.OverlapSphere(
                                 transform.position,
                                AttackRadio,
                                LayerMask.GetMask("Player") // se agrego layers Player
                            );

          //  Debug.Log(colliders.Length);

            // quiero saber con quien he chocado para saber su posicion y luego perseguirlo

            // si encuentra al player retorna valor 
            if (colliders.Length ==1) return colliders[0];

            // caso contrario
            else  return null ;

            //return colliders.Length ==1;

 
            
    }


    public void StartAttack(){

          mIsAttacking = true;
    }

    public void EnableHitbox(){

        //activa el hit box para que haga el daño a partir del minuto 1 
        hitboxLeft.SetActive(true);
        hitboxRight.SetActive(true);
    }



    public void StopAttack(){

        mIsAttacking = false;
      // detiene el ataque 
         hitboxLeft.SetActive(false);
        hitboxRight.SetActive(false);
    }

 // cuantto de daño se le hara al player
  public void TakeDamage(float damage)
  {
    Health -= damage;
    if (Health <= 0f){

          Destroy(gameObject);
    }
  
  }



}
