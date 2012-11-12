var prefabDot:Transform;
var control:String;
var magicNumber:int;

function Start () {

}

function Update () {

	var player:Player = gameObject.GetComponent("Player");
	
	if(Input.GetButton(control) && player.currentInk >= player.drawCost){
	
		var instanceDot = Instantiate(prefabDot, transform.position, Quaternion.identity);

		var mousePos = Input.mousePosition;
		mousePos.x -= Screen.width/2;
		mousePos.y -= Screen.height/2;
	
		instanceDot.transform.position = Vector3 (mousePos.x / magicNumber, mousePos.y / magicNumber, 0);
	
		player.currentInk -= player.drawCost;
	
	}
}

