var jump:String;
var horizontal:String;
var vertical:String;
public var playerNumber:int;
public var animationObj : Transform;
public var animationSpeed : float;
public var standing : Material;
public var leftA : Material;
public var leftB : Material;
public var leftC : Material;
public var leftD : Material;
public var leftE : Material;
public var rightA : Material;
public var rightB : Material;
public var rightC : Material;
public var rightD : Material;
public var rightE : Material;
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
	var directionVector = new Vector3(Input.GetAxis(vertical), 0, serverScript.getXSpeed(playerNumber));
	//var directionVector = new Vector3(Input.GetAxis(vertical), 0, Input.GetAxis(horizontal));
	
	if (directionVector != Vector3.zero) {
	
		// Animation/*
		Debug.Log(directionVector.z);
		if (directionVector.z > 0){
			
			if (lastDirection <= 0){
				
				lastDirection = 1;
				
				// Change direction material
				animationObj.renderer.material = rightA;
				animNum = 1;
				animationTimer = Time.time;
				Debug.Log("rightA");
			
			} else if (Time.time - animationTimer >= animationSpeed ){
			
				Debug.Log(animationObj.renderer.material.Equals(rightA));
			
				// Change material
				if (animNum == 1){
					
					animationObj.renderer.material = rightB;
					animNum = 2;
					animationTimer = Time.time;
					Debug.Log("rightB");
					
				} else if (animNum == 2){
				
					animationObj.renderer.material = rightC;
					animNum = 3;
					animationTimer = Time.time;
					Debug.Log("rightC");
				
				} else if (animNum == 3){
				
					animationObj.renderer.material = rightD;
					animNum = 4;
					animationTimer = Time.time;
					Debug.Log("rightD");
				
				} else if (animNum == 4){
				
					animationObj.renderer.material = rightE;
					animNum = 5;
					animationTimer = Time.time;
					Debug.Log("rightE");
				
				} else if (animNum == 5){
				
					animationObj.renderer.material = rightA;
					animNum = 1;
					animationTimer = Time.time;
					Debug.Log("rightA");
				
				}
			
			}
			
		} else if (directionVector.z < 0){
			
			if (lastDirection >= 0){
			
				lastDirection = -1;
			
				// Change direction material
				animationObj.renderer.material = leftA;
				animNum = -1;
				animationTimer = Time.time;
				Debug.Log("leftA");
			
			} else if (Time.time - animationTimer >= animationSpeed ){
			
				// Change material
				if (animNum == -1){
					
					animationObj.renderer.material = leftB;
					animNum = -2;
					animationTimer = Time.time;
					Debug.Log("leftB");
					
				} else if (animNum == -2){
				
					animationObj.renderer.material = leftC;
					animNum = -3;
					animationTimer = Time.time;
					Debug.Log("leftC");
				
				} else if (animNum == -3){
				
					animationObj.renderer.material = leftD;
					animNum = -4;
					animationTimer = Time.time;
					Debug.Log("leftD");
				
				} else if (animNum == -4){
				
					animationObj.renderer.material = leftE;
					animNum = -5;
					animationTimer = Time.time;
					Debug.Log("leftE");
				
				} else if (animNum == -5){
				
					animationObj.renderer.material = leftA;
					animNum = -1;
					animationTimer = Time.time;
					Debug.Log("leftA");
				
				}
			
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
	}
	
	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVector;
	//motor.inputJump = Input.GetButton(jump);
	motor.inputJump = serverScript.getJump(playerNumber);

}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")
