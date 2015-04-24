using UnityEngine;
using System.Collections;

public class ChamberMove : MonoBehaviour {
	
	Ray2D ray;
	RaycastHit2D rayHit;

	private Vector2 initPos;

	private enum MovementDir {

		Left,
		Right,
		Up,
		Down
	}
	private MovementDir movementDir;



	// Update is called once per frame
	void Update () 
	{

		///
		// When the mouse button is first pressed down
		///
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

				}
			}
		}

		///
		// If left mouse is down
		///
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
					Vector2 pos = hit.collider.gameObject.transform.position;
					// round the postion so it is only moves by one unit
					pos.x = Mathf.Round(mousePos.x);
					pos.y = Mathf.Round(mousePos.y);

					// determine which direction the chamber is being moved
					if (pos.x > initPos.x) {
						movementDir = MovementDir.Right;
					} else if ( pos.x < initPos.x) {
						movementDir = MovementDir.Left;
					} else if (pos.y > initPos.y) {
						movementDir = MovementDir.Up;
					} else if (pos.y < initPos.y) {
						movementDir = MovementDir.Down;
					}

					// find out the distance that the chamber has moved
					float differencePos = Vector2.Distance (initPos, pos);

					// go through all of the chambers in the array and move those that should be effected
					//for (int x = 0;  x < 8; x++)
					//{
					//	for (int y = 0; y < 8; y++)
					//	{
						//	if (y == pos.y)
						//	{
								// move left
								if (movementDir == MovementDir.Left){
									//debug
									Debug.Log("left");
									//move chambers method
									moveChambers(MovementDir.Left, pos, differencePos, true);
								} else if (movementDir == MovementDir.Right) {
									//debug
									Debug.Log("right");
									//move chambers
									moveChambers(MovementDir.Right, pos, differencePos, false);
								} else if (movementDir == MovementDir.Up) {
									Debug.Log("up");
									//move chambers
									moveChambers(MovementDir.Up, pos, differencePos, true);

								} else if (movementDir == MovementDir.Down) {
									Debug.Log("down");
									//move chambers
									moveChambers(MovementDir.Down, pos, differencePos, false);
								}

							//}
						//}
				//	}








					// move the chamber with the new round position
					//hit.collider.gameObject.transform.position = new Vector2( pos.x , pos.y );
				}
				
			}
		}

		///
		// if left mouse Button is up
		///
		if (Input.GetMouseButtonUp (0)) 
		{
			// update the chamber array with the new content

			// check if the chambers fall inside the bounds defined.

			// Find the null locations in the chamber array and fill with new chambers


			// 

		}
	
	}


	void moveChambers(MovementDir direction, Vector2 currentChamber, float movementAmount, bool positive)
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
							changedPos = ChambersToMove.position.x - movementAmount;
						} else {
							changedPos = ChambersToMove.position.x + movementAmount;
						}

						// and move the chambers
						ChambersToMove.position = new Vector2(changedPos,currentChamber.y);
					}
				} else if (direction == MovementDir.Down || direction == MovementDir.Up){
					if (currentChamber.x == x)
					{
						if (positive){
							changedPos = ChambersToMove.position.y + movementAmount;
						} else {
							changedPos = ChambersToMove.position.y - movementAmount;
						}
						// and move the chambers
						ChambersToMove.position = new Vector2(currentChamber.x, changedPos);
					}	
				}
			}

		}
	}
}
