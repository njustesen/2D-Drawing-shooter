var smooth = 2.0;
var angle = 5;

function Start () {
  

}

function Update () {

	angle += 2;
	var tiltAroundY = angle;
    var target = Quaternion.Euler (0, tiltAroundY, 0);
    // Dampen towards the target rotation
    transform.rotation = Quaternion.Slerp(transform.rotation, target,
                                   Time.deltaTime * smooth);

}

function OnTriggerEnter (other:Collider) {

	if (other.gameObject.CompareTag("Playa")){
		
		var player:Player = other.gameObject.GetComponent("Player");
		var objects : GameObject[];
		objects = GameObject.FindGameObjectsWithTag( "Dot" );
		
		for (var object : GameObject in objects)  { 
			Destroy(object);
		}
		
		Destroy(gameObject);
		
	} else if (other.gameObject.CompareTag("KillCollider")){
	
		Destroy(gameObject);
		
	}
}
