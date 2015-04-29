using UnityEngine;
using System.Collections;

public class ChamberMove : MonoBehaviour {
	
	Ray2D ray;
	RaycastHit2D rayHit;

	public Transform LevelObject;

	private Vector2 initPos;
	private Vector2 pos;

	private enum MovementDir 
	{
		Left,
		Right,
		Up,
		Down,
		current
	}

	private MovementDir movementDir;



	// Update is called once per frame
	void Update () 
	{

		///////////////////////////////////////////////////////////////////////////////////
		// When the mouse button is first pressed down
		///////////////////////////////////////////////////////////////////////////////////
		if (Input.GetMouseButtonDown (0)) 
		{
			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);
			
			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{
					//define intial chamber position
					initPos = hit.collider.gameObject.transform.position;

					initPos.x = Mathf.Round(initPos.x);
					initPos.y = Mathf.Round(initPos.y);

					Debug.Log(" inital position : x :" + initPos.x + " y " + initPos.y);

				}
			}
		}

		///////////////////////////////////////////////////////////////////////////////////
		// If left mouse is down
		///////////////////////////////////////////////////////////////////////////////////
		if (Input.GetMouseButton (0)) 
		{
			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);

			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{

					// define chamber position
					pos = hit.collider.gameObject.transform.position;
					// round the postion so it is only moves by one unit
					pos.x = Mathf.Round(pos.x);
					pos.y = Mathf.Round(pos.y);


					///////////////
					// DIRECTION //
					///////////////
					if (pos.x == initPos.x && pos.y == initPos.y) {
						movementDir = MovementDir.current;
					} else if (pos.x  > initPos.x) {
						movementDir = MovementDir.Right;
					} else if ( pos.x  < initPos.x) {
						movementDir = MovementDir.Left;
					} else if (pos.y  > initPos.y ) {
						movementDir = MovementDir.Up;
					} else if (pos.y  < initPos.y ) {
						movementDir = MovementDir.Down;
					}

					// find out the distance that the chamber has moved
					float differencePos = Vector2.Distance (initPos, pos);

			


							//}
						//}
				//	}

					// move the chamber with the new round position
					//hit.collider.gameObject.transform.position = new Vector2( pos.x , pos.y );
				}
				
			}
		}

		///////////////////////////////////////////////////////////////////////////////////
		// if left mouse Button is up
		///////////////////////////////////////////////////////////////////////////////////
		if (Input.GetMouseButtonUp (0)) 
		{

			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);
			
			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{
					// update the chamber array with the new content
					// move left
					if (movementDir == MovementDir.Left){
						//debug
						Debug.Log("left");
						//move chambers method
						moveChambers(MovementDir.Left, pos, true);
					} else if (movementDir == MovementDir.Right) {
						//debug
						Debug.Log("right");
						//move chambers
						moveChambers(MovementDir.Right, pos, false);
					} else if (movementDir == MovementDir.Up) {
						Debug.Log("up");
						//move chambers
						moveChambers(MovementDir.Up, pos, true);
						
					} else if (movementDir == MovementDir.Down) {
						Debug.Log("down");
						//move chambers
						moveChambers(MovementDir.Down, pos, false);
					}
					// check if the chambers fall inside the bounds defined.
					// Find the null locations in the chamber array and fill with new chambers
				}
			}
		}
	


	
	}

	/// <summary>
	/// Moves the chambers.
	/// </summary>
	/// <param name="direction">Direction.</param>
	/// <param name="currentChamber">Current chamber.</param>
	/// <param name="positive">If set to <c>true</c> positive.</param>
	void moveChambers(MovementDir direction, Vector2 currentChamber, bool positive)
	{
		for (int x = 0; x < 8; x++) 
		{
			for (int y = 0; y < 8; y++) 
			{
				//define the chamber
				Transform ChambersToMove = CreateChamberArray.ChamberArray[x,y].Object.transform;

				// define the direction
				float changedPos;

				// change the value based on the direction
				if (direction == MovementDir.Left || direction == MovementDir.Right)
				{
					if (currentChamber.y == y)
					{
						if (positive){
							changedPos = ChambersToMove.position.x - 1;
						} else {
							changedPos = ChambersToMove.position.x + 1;
						}

						// and move the chambers
						ChambersToMove.position = new Vector2(changedPos,currentChamber.y);
					}
				} else if (direction == MovementDir.Down || direction == MovementDir.Up){
					if (currentChamber.x == x)
					{
						if (positive){
							changedPos = ChambersToMove.position.y + 1;
						} else {
							changedPos = ChambersToMove.position.y - 1;
						}
						// and move the chambers
						ChambersToMove.position = new Vector2(currentChamber.x, changedPos);
					}	
				}
			}

		}
	}

	/// <summary>
	/// Updates the grid.
	/// </summary>
	void UpdateGrid (Transform[] changedItems)
	{
		// remove all old children from the grid that have changed
		for (int x = 0; x < 8; x++) {
			for (int y = 0; y < 8; y++) {
				// if the chamber in the array has value (it could be empty....)
				if (CreateChamberArray.ChamberArray[x,y] != null){

					// check if the chamber has been defined to change

					// make the value null so it can be edited later

				}
			}
		}


		// work though all the children of the level gameobject.. 
		foreach (Transform child in LevelObject)
		{
			// if statement to define if its one of the chambers that has changed postion


			// round the positions of the 
			float PosX  = Mathf.Round(child.position.x);
			float PosY  = Mathf.Round(child.position.y);
			// vector 2 for each child
			Vector2 v = new Vector2(PosX,PosY);
			
			// make the chamber positon the chamber number in the array
			CreateChamberArray.ChamberArray[(int)v.x,(int)v.y].Object = child;
			

		}



		// add the childern to the grid 


	}
}
