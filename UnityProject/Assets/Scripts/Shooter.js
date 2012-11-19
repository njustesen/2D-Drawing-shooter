var prefabBullet:Transform;
var shootForce:float;
var cursor:Transform;
var control:String;
var magicNumber:int;

function Start () {
	
}

function Update () {

	var player:Player = gameObject.GetComponent("Player");

	if (Input.GetButtonDown(control) && player.currentInk >= player.shootCost && !player.dead){
	
		Shoot();
	
		player.currentInk -= player.shootCost;
		
		player.updateInkBar();
	
	}
	
}

function Shoot () {

	var mousePos = Input.mousePosition;
	mousePos.x -= Screen.width/2;
	mousePos.y -= Screen.height/2;

	var mousePosition = Vector3 (cursor.transform.position.x, cursor.transform.position.y, 0);

	var direction:Vector3 = mousePosition - transform.position;

	direction = direction.normalized;
	
	var bullet:Transform = Instantiate(prefabBullet, transform.position + (direction * 1), Quaternion.identity);
	
	bullet.rotation = Quaternion.LookRotation(direction);;
	
	bullet.gameObject.rigidbody.AddForce(direction * shootForce);
	
}

