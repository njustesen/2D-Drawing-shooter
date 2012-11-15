var nuke:Transform;
var spawnTime:float;

function Start () {

}

function Update () {
	
	if(Random.Range(0.0, 100.0) < spawnTime){
		var powerUp = Instantiate(nuke, transform.position, Quaternion.identity);
		
		var x = Random.Range(-10, 10);
		var y = Random.Range(-7, 7);

		powerUp.transform.position = Vector3 (x, y, 0);
	}
	
}

