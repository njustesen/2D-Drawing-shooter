var xPos:float = -410;
var yPos:float = 25;
var zPos:float = -40;
var xRot:float = 20;
var yRot:float = 20;
var zRot:float = 0;
var speed:float = 4.0;

function OnMouseDown ()
	{        
	iTween.MoveTo(Camera.main.gameObject, Vector3(xPos, yPos, zPos), speed);
	iTween.RotateTo(Camera.main.gameObject, Vector3(xRot, yRot, zRot), speed);
	}