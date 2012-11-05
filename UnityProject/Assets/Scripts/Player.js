var score:int = 0;

function powerUp(){
	
	// Increases speed
	var motor:CharacterMotor = gameObject.GetComponent("CharacterMotor");
	motor.movement.maxForwardSpeed += 3.0;
	motor.movement.maxBackwardsSpeed += 3.0;
	
	// Increases jump
	motor.jumping.baseHeight += 2.0;
	
}