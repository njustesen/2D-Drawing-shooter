#pragma strict
var gameArea:Transform;
var prefabStar:Transform;
private var startingPos:Vector3;

function Start () {

}

function update (){
	
	
}

function OnTriggerEnter (other:Collider) {

	if (other.gameObject.CompareTag("Playa")){
		
		var player:Player = other.gameObject.GetComponent("Player");
		
		player.die();
		
	} else if (other.gameObject.CompareTag("PowerUp")){
	
		Destroy (other.gameObject);
	
	} else if (other.gameObject.CompareTag("Dot")){
	
		Destroy (other.gameObject);
	
	} else if (other.gameObject.CompareTag("Star")){
	
		var x = Random.Range(-8, 8);
		var y = Random.Range(-8, 8);
		
		Debug.Log("Water-Star-Collision");

		other.gameObject.transform.position = Vector3 (x + gameArea.transform.position.x, y + gameArea.transform.position.y, 0);
		
	}

}
