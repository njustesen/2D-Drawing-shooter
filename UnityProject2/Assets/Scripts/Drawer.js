var prefabDot:Transform;
var parentObj:Transform;
var cursor:Transform;
var control:String;
var magicNumber:int;
var dotDistance:float = 0.01;
public var playerNumber:int;
private var lastCursorPos:Vector3;
private var teleportEnabled:boolean = false;

function Start () {
	
}

function Update () {
	var serverScript = GameObject.Find("GameArea").GetComponent("Server");
	// Record mouse position
	var mousePos = Input.mousePosition;
		mousePos.x -= Screen.width/2;
		mousePos.y -= Screen.height/2;
	
	var floatX: float = -serverScript.getAimX(playerNumber);
	var floatY: float = -serverScript.getAimY(playerNumber);
	// Move cursor
	cursor.transform.localPosition = new Vector3 ( (floatX/ magicNumber)+15,  (floatY/ magicNumber)+10, 0);
	mouseX = cursor.transform.position.x;
	mouseY = cursor.transform.position.y;
		
	var player:Player = gameObject.GetComponent("Player");
	
	if (serverScript.getDraw(playerNumber) && teleportEnabled){
	
		player.transform.position = new Vector3(mouseX, mouseY, 0);
		
		disableTeleport();
	
	} else if(serverScript.getDraw(playerNumber) && player.currentInk >= player.drawCost && !player.dead){
	
		spawnDot(player, new Vector3(mouseX, mouseY, 0), lastCursorPos);
		
	}
	
	lastCursorPos = cursor.transform.position;
	
}

function enableTeleport(){

	teleportEnabled = true;

}

function disableTeleport(){

	teleportEnabled = false;

}

function spawnDot(player, before, after){

	distance = getDistance(after, before);

	if (player.currentInk >= player.drawCost * distance){
		// Paint before
		var beforeDot = Instantiate(prefabDot, transform.position, Quaternion.identity);
		//beforeDot.transform.parent = parent.transform;
		//beforeDot.transform.localPosition = new Vector3 (before.x / magicNumber, before.y / magicNumber, 0);
		beforeDot.transform.position = new Vector3 ((before.x), (before.y), 0);
		
		// Paint after
		var afterDot = Instantiate(prefabDot, transform.position, Quaternion.identity);
		//afterDot.transform.parent = parent.transform;
		//afterDot.transform.localPosition = new Vector3 (after.x / magicNumber, after.y / magicNumber, 0);
		afterDot.transform.position = new Vector3 ((after.x), (after.y), 0);
		
		spawnDotBetween(player, before, after);
		
		player.currentInk -= player.drawCost * getDistance(after, before);
		
		if(player.currentInk < 0){
			player.currentInk = 0;
		}
		
		player.updateInkBar();
	
	}
}

function spawnDotBetween(player, before, after){

	distance = getDistance(after, before);	

	if (player.currentInk >= player.drawCost() * distance){

		var middle = new Vector3((before.x + after.x) / 2, (before.y + after.y) / 2, 0);
		
		if (distance > dotDistance){		
			
			spawnDotBetween(player, before, middle);
			spawnDotBetween(player, after, middle);
	
			// Paint middle
			var middleDot = Instantiate(prefabDot, transform.position, Quaternion.identity);
			//middleDot.transform.parent = parent.transform;
			//middleDot.transform.localPosition = new Vector3 (middle.x / magicNumber, middle.y / magicNumber, 0);
			middleDot.transform.position = new Vector3 ((middle.x), (middle.y), 0);
			
			player.currentInk -= player.drawCost * distance;
			
			if(player.currentInk < 0){
				player.currentInk = 0;
			}
			
		}
		
	}

}

function getDistance(a:Vector3, b:Vector3){

	var distance:float = 	(a.x - b.x) * (a.x - b.x) +
							(a.y - b.y) * (a.y - b.y);
							
	return Mathf.Sqrt(distance);
}