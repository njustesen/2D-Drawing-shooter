var hits:int;

function Start () {
	
}

function Update () {
	
}

function hit () {

	hits--;

	if (hits <= 0){
		
		Destroy (gameObject);
		
	}
	
}

