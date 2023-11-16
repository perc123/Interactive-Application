using UnityEngine;

public class MoveButtonHandler : MonoBehaviour
{
    public Transform objectToMove; // Reference to the object you want to move
    //private bool isMoving = false; // Flag to track if the object is currently moving
    private bool moveButtonClicked;
    public float moveSpeed = 1f;
    public Vector3 targetPosition;
    public GameObject animationPanelManager;
    
    

    private void Update()
    {
        if (moveButtonClicked)
        {
            Vector3 targetPosition = GetUserInput();
            this.targetPosition = targetPosition;
            animationPanelManager.GetComponent<AnimationPanelManager>().currentObject.GetComponent<AnimationHandler>()
                .targetPosition = targetPosition;
        }
    }

    public void OnMoveButtonClicked()
    {
        moveButtonClicked = true;
        //isMoving = true;
    }

    public Vector3 GetUserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z; // Set the desired z distance from the camera

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPosition.z = 0;
            moveButtonClicked = false;
            return targetPosition;
        }
        
        return Vector3.zero; // Return a default value if no valid input is received
    }

}
