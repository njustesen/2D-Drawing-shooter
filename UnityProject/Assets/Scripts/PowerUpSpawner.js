var nuke:Transform;
var invisibility:Transform;
var spawnTime:float;

function Start () {

}

function Update () {
	
	if(Random.Range(0.0, 100.0) < spawnTime){
		
		var ran = Random.Range(0.0,2.0);
		
		if (ran < 1.0){
			spawnNuke();
		} else if (ran < 2.0){
			spawnInvisibility();
		}
		
	}
	
}


function spawnNuke(){

	var powerUp = Instantiate(nuke, transform.position, Quaternion.identity);
		
	var x = Random.Range(-10, 10);
	var y = Random.Range(-10, 10);

	powerUp.transform.position = Vector3 (x + gameObject.transform.position.x, y + gameObject.transform.position.y, 0);

}

function spawnInvisibility(){

	var powerUp = Instantiate(invisibility, transform.position, Quaternion.identity);
		
	var x = Random.Range(-10, 10);
	var y = Random.Range(-10, 10);

	powerUp.transform.position = Vector3 (x + gameObject.transform.position.x, y + gameObject.transform.position.y, 0);

}
