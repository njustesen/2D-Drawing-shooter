function Start () {

}

function Update () {

}

function OnTriggerEnter (other:Collider) {

	if (other.gameObject.CompareTag("Playa")){
		
		var player:Player = other.gameObject.GetComponent("Player");
		
	} else if (other.gameObject.CompareTag("Wall")){
	
		Destroy (gameObject);
	
	} else if (other.gameObject.CompareTag("PowerUp")){
	
		Destroy (gameObject);
	
	} else if (other.gameObject.CompareTag("Star")){
	
		Destroy (gameObject);
	
	} else if (other.gameObject.CompareTag("Dot")){
	
		Destroy (gameObject);
		
		var dot:Dot = other.gameObject.GetComponent("Dot");
	
		dot.hit();
	
	}
	
	
}
