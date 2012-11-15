var smooth = 2.0;
var speed = 5;

function Start () {
  

}

function Update () {

	var tiltAroundX = speed;
    var target = Quaternion.Euler (tiltAroundX, 0, 0);
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
		
	}
}
