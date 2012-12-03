var gameArea:Transform;
var score:int = 0;
var forward:Vector3 = Vector3(0,0,0);
var scoreBar:GUIText;
var deadText:GUIText;
var inkBar:Transform;
var inkBarMaxWidth:float;
var maxInk:float;
var currentInk:float;
var inkPerSecond:float;
var shootCost:float;
var drawCost:float;
var respawnTime:float;
var respawnLocation:Vector3;
public var animationObj : Transform;
public var otherPlayerA:Transform;
public var otherPlayerB:Transform;
public var otherPlayerC:Transform;
private var timeSinceInvisibility:int = 0;
private var invisibilityTime:int;
public var dead:boolean;
public var playerNumber:int;
private var gameEnded:boolean;
var color:Color;

function Start(){
	
	gameEnded = false;
	deadText.material.color = color;
	deadText.text = "";

}

function Update(){

	// Set z-position to 0!!
	gameObject.transform.position.z = 0;
	gameObject.transform.eulerAngles.x = forward.x;
	gameObject.transform.eulerAngles.y = forward.y;
	gameObject.transform.eulerAngles.z = forward.z;
	
	// Update ink
	currentInk += Time.deltaTime * inkPerSecond;
	
	if (currentInk >= maxInk){
		currentInk = maxInk;
	}
	
	updateInkBar();
	updateScoreBar();
	
	// Check for invisibility
	
	if (!animationObj.renderer.enabled 
		&& Time.time > timeSinceInvisibility + invisibilityTime){
		
		visible();
		
	}
	
	
}

function invisible(time){
	animationObj.renderer.enabled = false; 
	timeSinceInvisibility = Time.time;
	invisibilityTime = time;

}

function invisiblePU(time){
	var playerA:Player = otherPlayerA.gameObject.GetComponent("Player");
	var playerB:Player = otherPlayerB.gameObject.GetComponent("Player");
	var playerC:Player = otherPlayerC.gameObject.GetComponent("Player");
	
	playerA.invisible(time);
	playerB.invisible(time);
	playerC.invisible(time);
}

function visible(){

	animationObj.renderer.enabled = true; 

}

function teleportPU(){

	var drawer:Drawer = gameObject.GetComponent("Drawer");
	
	drawer.enableTeleport();

}

function eraserPU(){

	var shooter:Shooter = gameObject.GetComponent("Shooter");
	
	shooter.enableErasing();

}

function updateInkBar(){

	inkBar.transform.localScale.y = inkBarMaxWidth / ( maxInk / currentInk );
	
}

function updateScoreBar(){
	
	if (!gameEnded){
		scoreBar.text = "" + score;
	}
	
}

function die(){
	//score--;
	dead = true;
	deadText.text = "DEAD";
	//inkBar.renderer.enable = false;
	transform.position = Vector3(-1000,10,0);
	yield WaitForSeconds(respawnTime);
	if (!gameEnded){
		visible();
		respawn();
	}
}

function respawn(){
	dead = false;
	deadText.text = "";
	transform.position = respawnLocation + gameArea.position;
}

function endGame(){

	gameEnded = true;
	die();

}

function removeFromGame(){

	endGame();
	inkBar.renderer.enabled = false;
	scoreBar.text = " ";
	deadText.text = " ";
	Destroy(gameObject);

}