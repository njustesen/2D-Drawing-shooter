public var speed:float = 0.01;
public var maxSpeed:float = 0.7;
public var acceleration:float = 0.0001;
public var timeBeforeRaise = 10;
private var started:int;

function Start(){

	started = Time.time;
	
}


function Update(){

	Debug.Log(speed);

	if (Time.time > started + timeBeforeRaise){
		gameObject.transform.position.y += speed;
		if (speed < maxSpeed){
			speed += acceleration;
		}
	}
	
}
