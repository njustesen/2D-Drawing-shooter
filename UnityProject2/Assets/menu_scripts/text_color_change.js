#pragma strict

function OnMouseEnter() 
	{ //Change text color into green 
	renderer.material.color = Color.green; 
	}
 
function OnMouseExit() 
	{ //Change text color back into white 
	renderer.material.color = Color.white; 
	}
