var prefabDot:Transform;
var parentObj:Transform;
var cursor:Transform;
var control:String;
var magicNumber:int;
var dotDistance:float = 0.01;
private var lastCursorPos:Vector3;

function Start () {
	
}

function Update () {

	// Record mouse position
	var mousePos = Input.mousePosition;
		mousePos.x -= Screen.width/2;
		mousePos.y -= Screen.height/2;
		
	// Move cursor
	cursor.transform.localPosition = new Vector3 (mousePos.x / magicNumber, mousePos.y / magicNumber, 0);
	mouseX = cursor.transform.position.x;
	mouseY = cursor.transform.position.y;
		
	var player:Player = gameObject.GetComponent("Player");
	
	if(Input.GetButton(control) && player.currentInk >= player.drawCost){
	
		spawnDot(player, new Vector3(mouseX, mouseY, 0), lastCursorPos);
		
	}
	
	lastCursorPos = cursor.transform.position;
	
}

function spawnDot(player, before, after){

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

	player.currentInk -= player.drawCost;
	player.updateInkBar();

}

function spawnDotBetween(player, before, after){

	var middle = new Vector3((before.x + after.x) / 2, (before.y + after.y) / 2, 0);

	var distance:float = 	(before.x - after.x) * (before.x - after.x) +
							(before.y - after.y) * (before.y - after.y);
	distance = Mathf.Sqrt(distance);
	
	if (distance > dotDistance){		
		
		spawnDotBetween(player, before, middle);
		spawnDotBetween(player, after, middle);

		// Paint middle
		var middleDot = Instantiate(prefabDot, transform.position, Quaternion.identity);
		//middleDot.transform.parent = parent.transform;
		//middleDot.transform.localPosition = new Vector3 (middle.x / magicNumber, middle.y / magicNumber, 0);
		middleDot.transform.position = new Vector3 ((middle.x), (middle.y), 0);
		
		player.currentInk -= player.drawCost;
		
	}

}