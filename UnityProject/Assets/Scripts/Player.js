var score:int = 0;
var forward:Vector3 = Vector3(0,0,0);

function Update(){

	// Set z-position to 0!!
	gameObject.transform.position.z = 0;
	gameObject.transform.eulerAngles.x = forward.x;
	gameObject.transform.eulerAngles.y = forward.y;
	gameObject.transform.eulerAngles.z = forward.z;

}

function powerUp(){
	
	// Increases speed
	var motor:CharacterMotor = gameObject.GetComponent("CharacterMotor");
	motor.movement.maxForwardSpeed += 3.0;
	motor.movement.maxBackwardsSpeed += 3.0;
	
	// Increases jump
	motor.jumping.baseHeight += 2.0;
	
}