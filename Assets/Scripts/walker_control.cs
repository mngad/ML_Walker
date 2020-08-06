using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class walker_control : Agent
{

  public Quaternion initRot;
  public Vector3 initPos;
  public Vector3 initPosllu ;
  public Vector3 initPoslll ;
  public Vector3 initPosrlu;
  public Vector3 initPosrll;
public Quaternion initRotllu;
public Quaternion initRotlll;
public Quaternion initRotrlu;
public Quaternion initRotrll;

  public GameObject thewalker;
  public GameObject body;
  public GameObject rightlegupper;
  public GameObject rightleglower;
  public GameObject leftlegupper;
  public GameObject leftleglower;
  public float time;

  public float lluRotMult;
  public float lllRotMult;
  public float rluRotMult;
  public float rllRotMult;

  Rigidbody2D bodyRB;

  Rigidbody2D lluRB;
  Rigidbody2D lllRB;
  Rigidbody2D rllRB;
  Rigidbody2D rluRB;
  HingeJoint2D hinge;
   HingeJoint2D hinge1;
HingeJoint2D hinge2;
HingeJoint2D hinge3;


bodycollide bc;
bodycollide lcl;
bodycollide lcr;

void start(){
  Physics.IgnoreLayerCollision(9, 9);
}

  public int checkpointsHit;
  private void Awake()
  {
     hinge = rightleglower.GetComponent<HingeJoint2D>();
      hinge1 = rightlegupper.GetComponent<HingeJoint2D>();
   hinge2 = leftleglower.GetComponent<HingeJoint2D>();
  hinge3 = leftlegupper.GetComponent<HingeJoint2D>();
    bodyRB = body.GetComponent<Rigidbody2D>();
    lluRB = body.GetComponent<Rigidbody2D>();
    lllRB = body.GetComponent<Rigidbody2D>();
    rluRB = body.GetComponent<Rigidbody2D>();
    rllRB = body.GetComponent<Rigidbody2D>();
    initRot = body.transform.rotation;
    initPos = body.transform.position;
    initPosllu = leftlegupper.transform.position;
    initPoslll = leftleglower.transform.position;
    initPosrlu = rightlegupper.transform.position;
    initPosrll = rightleglower.transform.position;

    initRotllu = leftlegupper.transform.rotation;
    initRotlll = leftleglower.transform.rotation;
    initRotrlu = rightlegupper.transform.rotation;
    initRotrll = rightleglower.transform.rotation;
     Application.targetFrameRate = 300;



       bc = body.GetComponent<bodycollide>();
       lcl = leftlegupper.GetComponent<bodycollide>();
       lcr = rightlegupper.GetComponent<bodycollide>();
  }
  public void Update()
  {
      //count += 1;


      time += Time.deltaTime;
      SetReward(-0.05f);
      SetReward(body.transform.position.x *0.01f);
      //SetReward(bodyRB.velocity.x *0.1f);
      // if(body.transform.position.y >= -0.5f){
      //   SetReward(0.05f);
      // }



      if(bc.hitFloor == true){

        SetReward(-1f);
        EndEpisode();
      }
      if(bc.backstop == true){

        SetReward(-2f);
        EndEpisode();
        bc.backstop = false;
      }
      if(lcl.hitFloor == true){

        SetReward(-0.1f);
        lcl.hitFloor = false;
      }
      if(lcr.hitFloor == true){

        SetReward(-0.1f);
        lcr.hitFloor = false;
      }

      if(bc.checkpoint == true){
        SetReward(1f);
        bc.checkpoint = false;
      }

      if(bodyRB.velocity.magnitude < 0.01){
        SetReward(-1f);
        EndEpisode();
      }



      //add rewards here

  }


  public override void Initialize()
  {

    thewalker.transform.position = initPos;
    thewalker.transform.rotation = initRot;
      //initRot = thewalker.transform.rotation;
      //initPos = thewalker.transform.position;
      //Debug.Log("InitPos = " + initPos);

      //m_ResetParams = Academy.Instance.FloatProperties;
      //SetResetParameters();



  }

  public override void OnActionReceived(float[] vectorAction)
  {

    float multMult = 200f;
    //
    // lluRotMult =   multMult * Mathf.Clamp(vectorAction[0], -1f, 1f);
    // lllRotMult =   multMult * Mathf.Clamp(vectorAction[1], -1f, 1f);
    // rluRotMult =   multMult * Mathf.Clamp(vectorAction[2], -1f, 1f);
    // rllRotMult =   multMult * Mathf.Clamp(vectorAction[3], -1f, 1f);
    // rllRB.rotation +=rllRotMult;
    // rluRB.rotation += rluRotMult;
    // lllRB.rotation += lllRotMult;
    // lluRB.rotation +=lluRotMult;



     JointMotor2D motor = hinge.motor;
     motor.motorSpeed = multMult * Mathf.Clamp(vectorAction[0], -1f, 1f);
     hinge.motor = motor;

      JointMotor2D motor1 = hinge1.motor;
      motor1.motorSpeed = multMult * Mathf.Clamp(vectorAction[1], -1f, 1f);
      hinge1.motor = motor1;

       JointMotor2D motor2 = hinge2.motor;
       motor2.motorSpeed = multMult * Mathf.Clamp(vectorAction[2], -1f, 1f);
       hinge2.motor = motor2;

        JointMotor2D motor3 = hinge3.motor;
        motor3.motorSpeed = multMult * Mathf.Clamp(vectorAction[3], -1f, 1f);
        hinge3.motor = motor3;


  }


public override void CollectObservations(VectorSensor sensor)
  {


      //sensor.AddObservation(bodyRB.velocity.x);
      sensor.AddObservation(body.transform.position.x);
      //sensor.AddObservation(bodyRB.velocity.y);
      sensor.AddObservation(body.transform.position.y);


     sensor.AddObservation(leftlegupper.transform.rotation.z);
     sensor.AddObservation(leftleglower.transform.rotation.z );
     sensor.AddObservation(rightlegupper.transform.rotation.z );
     sensor.AddObservation(rightleglower.transform.rotation.z);

  }






  public override void OnEpisodeBegin()
  {


      time = 0f;
      bc.hitFloor = false;


     body.transform.position = initPos;
     body.transform.rotation = initRot;
     bodyRB.velocity = new Vector3(0f, 0f, 0f);
     bodyRB.transform.rotation = initRot;

     rllRB.rotation =0f;
     rluRB.rotation =0f;
     lllRB.rotation =0f;
     lluRB.rotation =0f;
     rllRB.velocity =new Vector3(0f, 0f, 0f);
     rluRB.velocity =new Vector3(0f, 0f, 0f);
     lllRB.velocity =new Vector3(0f, 0f, 0f);
     lluRB.velocity =new Vector3(0f, 0f, 0f);
     rightleglower.transform.position = initPosrll;
     rightlegupper.transform.position = initPosrlu;
     leftleglower.transform.position = initPoslll;
     leftlegupper.transform.position = initPosllu;
    leftlegupper.transform.rotation= initRotllu;
    leftleglower.transform.rotation = initRotlll;
    rightlegupper.transform.rotation = initRotrlu;
    rightleglower.transform.rotation = initRotrll;
      //thewalker.GetComponent<RigidBody2D>().transform.rotation = initRot;




      //Reset the parameters when the Agent is reset.
      //SetResetParameters();
  }
}
