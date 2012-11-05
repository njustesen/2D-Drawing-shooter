function Start () {

}

function Update () {

}

function OnTriggerEnter (other:Collider) {

	if (other.gameObject.CompareTag("Player")){
		
		var player:Player = other.gameObject.GetComponent("Player");
		
		player.powerUp();
		
		Destroy (gameObject);
		
	}
}
