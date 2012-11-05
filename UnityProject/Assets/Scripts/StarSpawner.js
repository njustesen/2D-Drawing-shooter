var prefabStar:Transform;

function Start () {
	SpawnStar();
}

function SpawnStar () {
	
	var star = Instantiate(prefabStar, transform.position, Quaternion.identity);
		
	var x = Random.Range(-10, 10);
	var y = Random.Range(-7, 7);

	star.transform.position = Vector3 (x, y, 0);
	
}

