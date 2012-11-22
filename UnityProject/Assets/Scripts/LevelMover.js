public var speed:float = 0.01;
public var maxSpeed:float = 0.7;
public var acceleration:float = 0.0001;
public var timeBeforeRaise = 10;
public var minHeight:float;
public var maxHeight:float;
public var minWidth:float;
public var maxWidth:float;
private var started:int;
private var movingDirection:Vector3 = Vector3(0,1,0);

function Start(){

	started = Time.time;
	
}


function Update(){

	// Move
	if (Time.time > started + timeBeforeRaise){
		gameObject.transform.position += speed * movingDirection;
		
		// Accelerate
		if (speed < maxSpeed){
			speed += acceleration;
		}
		
	}
	
	// Change direction?
	if (movingDirection == Vector3(0,1,0) && gameObject.transform.position.y >= maxHeight){
		movingDirection = Vector3(1,0,0);
		speed = 0;
		started = Time.time;
	} else if (movingDirection == Vector3(1,0,0) && gameObject.transform.position.x >= maxWidth){
		movingDirection = Vector3(0,-1,0);
		speed = 0;
		started = Time.time;
	} else if (movingDirection == Vector3(0,-1,0) && gameObject.transform.position.y <= minHeight){
		movingDirection = Vector3(-1,0,0);
		speed = 0;
		started = Time.time;
	}else if (movingDirection == Vector3(-1,0,0) && gameObject.transform.position.x <= minWidth){
		speed = 0;
		started = Time.time;
		// END GAME!
	}
	
}
