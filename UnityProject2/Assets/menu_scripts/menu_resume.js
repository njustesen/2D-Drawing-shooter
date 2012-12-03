#pragma strict
public var gameArea:Transform;

function Start () {

	

}

function Update () {

	

}

function OnMouseDown(){

	var level:LevelMover = gameArea.gameObject.GetComponent("LevelMover");

	level.resumeGame();

}