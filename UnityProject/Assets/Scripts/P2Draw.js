var prefabDot:Transform;
var shootForce:float;

function Start () {

}

function Update () {
	
	if(Input.GetButton("FireC2")){
		var instanceDot = Instantiate(prefabDot, transform.position, Quaternion.identity);

		var mousePos = Input.mousePosition;
		mousePos.x -= Screen.width/2;
		mousePos.y -= Screen.height/2;

		instanceDot.transform.position = Vector3 (mousePos.x / 36, mousePos.y / 36, 0);
	}
}