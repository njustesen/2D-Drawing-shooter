public var speed:float = 0.01;
public var maxSpeed:float = 0.7;
public var acceleration:float = 0.0001;
public var timeBeforeRaise = 10;
public var minHeight:float;
public var maxHeight:float;
public var minWidth:float;
public var maxWidth:float;
public var gameOver:GUIText;
public var blackPlane:Transform;
public var menu:Transform;
private var started:int;
private var movingDirection:Vector3 = Vector3(1,0,0);

function Start(){

	started = Time.time;
	blackPlane.renderer.enabled = false;
	gameOver.text = " ";
	menu.transform.localPosition = Vector3(-1000, 0, 0);
	
}

function Update(){

	// Pause?
	if (Input.GetKeyDown("p")){
		pauseGame();
	}

	// Move
	if (Time.time > started + timeBeforeRaise){
		gameObject.transform.position += speed * movingDirection  * Time.deltaTime * 42;
		
		// Accelerate
		if (speed < maxSpeed){
			speed += acceleration * Time.deltaTime * 42;
		}
		
	}
	
	// Change direction?
	if (movingDirection == Vector3(1,0,0) && gameObject.transform.position.x >= maxWidth){
		movingDirection = Vector3(0,-1,0);
		speed = 0;
		started = Time.time;
	} else if (movingDirection == Vector3(0,-1,0) && gameObject.transform.position.y <= minHeight){
		movingDirection = Vector3(-1,0,0);
		speed = 0;
		started = Time.time;
	}else if (movingDirection == Vector3(-1,0,0) && gameObject.transform.position.x <= minWidth){
		movingDirection = Vector3(0,1,0);
		speed = 0;
		started = Time.time;
	} else if (movingDirection == Vector3(0,1,0) && gameObject.transform.position.y >= maxHeight){
		movingDirection = Vector3(0,0,0);
		speed = 0;
		started = Time.time;
	} else if (movingDirection == Vector3(0,0,0) && Time.time > started + timeBeforeRaise){
		speed = 0;
		started = Time.time;
		movingDirection = Vector3(10,10,10);
		blackPlane.renderer.enabled = true;
		gameOver.text = "GAME OVER";
		pauseGame();
	}
	
}

function pauseGame(){

	menu.transform.localPosition = Vector3(0, 0, 0);
	blackPlane.renderer.enabled = true;
	Time.timeScale = 0;
	
}

function resumeGame(){

	menu.transform.localPosition = Vector3(-1000, 0, 0);
	blackPlane.renderer.enabled = false;
	Time.timeScale = 1;
	
}

function quitGame (){
	
	menu.transform.localPosition = Vector3(-1000, 0, 0);
	Time.timeScale = 1;
	Application.LoadLevel ("start_menu");

}

function restartGame(){

	menu.transform.localPosition = Vector3(-1000, 0, 0);
	Time.timeScale = 1;
	Application.LoadLevel(Application.loadedLevel);
	
}
