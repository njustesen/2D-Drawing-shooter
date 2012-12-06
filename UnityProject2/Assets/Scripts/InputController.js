var jump:String;
var horizontal:String;
var vertical:String;
public var playerNumber:int;
public var animationObj : Transform;
public var animationSpeed : float;
public var standing : Material;
public var color:Color;
public var rightAnimation : Material[] = new Material[18];
public var leftAnimation : Material[] = new Material[18];
private var motor : CharacterMotor;
private var animationTimer : float;
private var lastDirection : float;
private var animNum : int;


// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
	animationTimer = Time.time;
	lastDirection = 0;
	animNum = 0;
}

// Update is called once per frame
function Update () {

	// Get the input vector from kayboard or analog stick
	var serverScript = GameObject.Find("GameArea").GetComponent("Server");
	//var directionVector = new Vector3(Input.GetAxis(vertical), 0, Input.GetAxis(horizontal));
	//var directionVector = new Vector3(Input.GetAxis(vertical), 0, serverScript.getXSpeed(playerNumber));
	var directionVector = new Vector3(Input.GetAxis(vertical), 0, Input.GetAxis(horizontal));
	
	if (directionVector != Vector3.zero) {
	
		// Animation/*
		if (directionVector.z > 0){
			
			if (lastDirection <= 0){
				
				lastDirection = 1;
				
				// Change direction material
				animNum = 0;
				animationObj.renderer.material = rightAnimation[animNum];
				animationTimer = Time.time;
			
			} else if (Time.time - animationTimer >= animationSpeed ){
					
					animNum++;
					if (animNum == rightAnimation.Length){
						animNum = 0;
					}
					
					animationObj.renderer.material = rightAnimation[animNum];
					animationTimer = Time.time;
			
			}
			
		} else if (directionVector.z < 0){
			
			if (lastDirection >= 0){
				
				lastDirection = -1;
				
				// Change direction material
				animNum = 0;
				animationObj.renderer.material = leftAnimation[animNum];
				animationTimer = Time.time;
			
			} else if (Time.time - animationTimer >= animationSpeed ){
					
					animNum++;
					if (animNum == leftAnimation.Length){
						animNum = 0;
					}
					
					animationObj.renderer.material = leftAnimation[animNum];
					animationTimer = Time.time;
			
			}
			
		}
		
	
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	} else {
	
		animationObj.renderer.material = standing;
	
	}
	
	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVector;
	motor.inputJump = Input.GetButton(jump);
	//motor.inputJump = serverScript.getJump(playerNumber);
	
	animationObj.renderer.material.color = color;

}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")
