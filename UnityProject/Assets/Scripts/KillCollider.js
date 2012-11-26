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

	}

}
