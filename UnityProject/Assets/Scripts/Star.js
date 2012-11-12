var value:int = 1;

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

		gameObject.transform.position = Vector3 (x, y, 0);
		
	}
}

