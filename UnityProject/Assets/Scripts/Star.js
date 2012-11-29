var value:int = 1;
var gameArea:Transform;

function Start () {
	
}

function Update () {

}

function OnTriggerEnter (other:Collider) {

	Debug.Log("collision");

	if (other.gameObject.CompareTag("Playa")){
		
		var player:Player = other.gameObject.GetComponent("Player");
		
		player.score += value;
		
		var x = Random.Range(-8, 8);
		var y = Random.Range(-8, 8);

		gameObject.transform.position = Vector3 (x + gameArea.transform.position.x, y + gameArea.transform.position.y, 0);
		
	} else if (other.gameObject.CompareTag("KillCollider")){
	
		x = Random.Range(-8, 8);
		y = Random.Range(-8, 8);

		gameObject.transform.position = Vector3 (x + gameArea.transform.position.x, y + gameArea.transform.position.y, 0);
		
	}
}

