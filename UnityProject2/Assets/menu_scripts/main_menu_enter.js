#pragma strict

function OnMouseDown ()
	{
	iTween.MoveTo(Camera.main.gameObject, Vector3(-190, 15, -47), 4.0);
	iTween.RotateTo (Camera.main.gameObject, Vector3(10, 0, 0), 4.0);
	}