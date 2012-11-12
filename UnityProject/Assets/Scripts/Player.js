var score:int = 0;
var forward:Vector3 = Vector3(0,0,0);
var scoreBar:GUIText;
var inkBar:Transform;
var inkBarMaxWidth:float;
var maxInk:float;
var currentInk:float;
var inkPerSecond:float;
var shootCost:float;
var drawCost:float;

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
	
}

function powerUp(){
	
	// Increases speed
	var motor:CharacterMotor = gameObject.GetComponent("CharacterMotor");
	motor.movement.maxForwardSpeed += 3.0;
	motor.movement.maxBackwardsSpeed += 3.0;
	
	// Increases jump
	motor.jumping.baseHeight += 2.0;
	
}

function updateInkBar(){

	inkBar.transform.localScale.y = inkBarMaxWidth / ( maxInk / currentInk );

}

function updateScoreBar(){
	
	scoreBar.text = "" + score;
	
}