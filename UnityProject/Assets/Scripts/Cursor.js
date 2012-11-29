var eraseTime = 0.2;
private var lastEraseTime;

function Start () {
	lastEraseTime = - 1000;
}

function Update () {
	
}

function erase(){

	lastEraseTime = Time.time;

}


function OnTriggerEnter (other:Collider) {

	if (lastEraseTime + eraseTime >= Time.time && other.gameObject.CompareTag("Dot")){
		
		var dot:Dot = other.gameObject.GetComponent("Dot");
		
		dot.hit();
		
	}
}