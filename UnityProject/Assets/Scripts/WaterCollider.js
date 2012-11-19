#pragma strict
var gameArea:Transform;
var prefabStar:Transform;

function Start () {

}

function update (){
	
	
	
}

function OnTriggerEnter (other:Collider) {

	if (other.gameObject.CompareTag("Playa")){
		
		var player:Player = other.gameObject.GetComponent("Player");
		
		//Destroy (other.gameObject);
		
		player.die();
		
	} else if (other.gameObject.CompareTag("PowerUp")){
	
		Destroy (other.gameObject);
	
	} else if (other.gameObject.CompareTag("Dot")){
	
		Destroy (other.gameObject);
	
	} else if (other.gameObject.CompareTag("Star")){
	
		var x = Random.Range(-10, 10);
		var y = Random.Range(-7, 7);
		
		Debug.Log("Water-Star-Collision");

		other.gameObject.transform.position = Vector3 (x + gameArea.transform.position.x, y + gameArea.transform.position.y, 0);
		
	}

}
