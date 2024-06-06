using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameManager gameManager;

    private GameObject player;

    private PlayerMovement playerMovement;


    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }

        gameManager.onInit += GameStarted;
    }

    private void GameStarted()
    {
        // Get instances
        player = gameManager.player;
        playerMovement = player.GetComponent<PlayerMovement>();

        // Enable UI action map ("Gameplay" action map enabled by default)
        playerInput.actions.FindActionMap("UI").Enable();

        // Assign input events
        playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        InputAction action = context.action;

        switch (action.name)
        {
            case "Movement":
                switch(action.phase)
                {
                    case InputActionPhase.Performed:
                        Vector2 moveCommand = action.ReadValue<Vector2>();
                        playerMovement.SetMoveInput(moveCommand);
                        break;
                    case InputActionPhase.Canceled:
                        playerMovement.SetMoveInput(Vector2.zero);
                        break;
                }
                break;

            case "Pause":
                gameManager.PauseOnTriggered();
                break;
        }
    }
}
