var value:int = 1;
var gameArea:Transform;

function Start () {
	
}

function Update () {

}

function OnTriggerEnter (other:Collider) {

	if (other.gameObject.CompareTag("Playa")){
		
		var player:Player = other.gameObject.GetComponent("Player");
		
		player.score += value;
		
		var x = Random.Range(-10, 10);
		var y = Random.Range(-7, 7);

		gameObject.transform.position = Vector3 (x + gameArea.transform.position.x, y + gameArea.transform.position.y, 0);
		
	} else if (other.gameObject.CompareTag("WaterCollider")){
	
		
	}
}

