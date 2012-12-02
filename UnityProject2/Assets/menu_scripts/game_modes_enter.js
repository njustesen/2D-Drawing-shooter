#pragma strict

function OnMouseDown ()
	{
	iTween.MoveTo(Camera.main.gameObject, Vector3(-5, 25, -30), 4.0);
	iTween.RotateTo(Camera.main.gameObject, Vector3(28, 341, 0), 4.0);
	}