var prefabBullet:Transform;
var shootForce:float;
var cursor:Transform;
var control:String;
var magicNumber:int;
public var playerNumber:int;

function Start () {
	
}

function Update () {
    var serverScript = GameObject.Find("GameArea").GetComponent("Server");
	var player:Player = gameObject.GetComponent("Player");

	if (serverScript.getShoot(playerNumber) && player.currentInk >= player.shootCost && !player.dead){
	
		Shoot();
	
		player.currentInk -= player.shootCost;
		
		player.updateInkBar();
	
	}
	
}

function Shoot () {

	var mousePosition = Vector3 (cursor.transform.position.x, cursor.transform.position.y, 0);

	var direction:Vector3 = mousePosition - transform.position;

	direction = direction.normalized;
	
	var bullet:Transform = Instantiate(prefabBullet, transform.position + (direction * 1.2), Quaternion.identity);
	
	bullet.rotation = Quaternion.LookRotation(direction);;
	
	bullet.gameObject.rigidbody.AddForce(direction * shootForce);
	
}

