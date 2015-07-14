
//横移動速度
var speed : float = 1;

//ゲーム停止フラグ
var FreezeFlag : boolean;

//アニメーションするオブジェクトとアニメーター  
var animateObject : GameObject;
var animator : Animator;

var Ground : GameObject;

//左右の障害物
var RailA : GameObject;
var RailB : GameObject;

//敵のタグ
var enemyTagName : String = "Takenoko";

function Awake(){
  //アニメーターを自動設定
  animator = animateObject.GetComponent(Animator);
}

function Start () {

}

function Update () {

}

function FixedUpdate(){
  if( FreezeFlag == false ){
    var horizontalMove = Input.GetAxis( "Horizontal" );
    animator.SetFloat( "HorizontalMove", horizontalMove );
    //Debug.Log( horizontalMove );
   if(RailB.transform.position.x < transform.position.x && transform.position.x < RailA.transform.position.x)
 	   GetComponent.<Rigidbody>().MovePosition( GetComponent.<Rigidbody>().position +  Vector3( horizontalMove, 0, 0 ) * speed * Time.deltaTime );
  }
}

function OnCollisionEnter(other:Collision){
  Debug.Log("HitCollision: " + other.gameObject.name );
  
  if(other.gameObject.tag == "DeleteLine"){
  	Debug.Log("Atari");
  	Application.LoadLevel("ClearScene");
  }
  
  if( other.gameObject.tag == enemyTagName ){
    Debug.Log( "Hit Takenoko!" );
    //たけのこにぶつかった時の処理
    Ground.SendMessage("GameEnd",SendMessageOptions.DontRequireReceiver);
    SetKinematicMode(false);
  	GetComponent.<Rigidbody>().velocity = Vector3.zero;
  	GetComponent.<Rigidbody>().AddForce(Vector3.up * 500);
  }
  
}

function OnTriggerEnter(other:Collider){
  //Dev 開発用
  Debug.Log("HitTrigger ObjectName : " + other.gameObject.name + "\nTag : " + other.tag );
  //SetKinematicMode(false);
  //rigidbody.velocity = Vector3.zero;
  //rigidbody.AddForce(Vector3.up * 500); //ACT

}

function SetKinematicMode(setState:boolean){
  GetComponent.<Rigidbody>().isKinematic = setState;
}

function Freeze(){
  FreezeFlag = true;
}

function Reset(){
  FreezeFlag = false;
}