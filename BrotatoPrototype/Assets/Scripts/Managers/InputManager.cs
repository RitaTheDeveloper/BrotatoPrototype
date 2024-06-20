using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private readonly IItemFactory itemFactory = new ItemFactory();
    public List<Item> baseItemList = new List<Item>();
    public List<StandartItem> ItemList = new List<StandartItem>();

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

        CreateItems();
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

    private void CreateItems()
    {
        foreach (Item item in baseItemList)
        {
            Item itemT1 = itemFactory.CreateItem(item, TierType.FirstTier);
            itemT1.SynchronizeAllCharacteristics();
            ItemList.Add(itemT1.GetComponent<StandartItem>());
            Item itemT2 = itemFactory.CreateItem(item, TierType.SecondTier);
            itemT2.SynchronizeAllCharacteristics();
            ItemList.Add(itemT2.GetComponent<StandartItem>());
            Item itemT3 = itemFactory.CreateItem(item, TierType.ThirdTier);
            itemT3.SynchronizeAllCharacteristics();
            ItemList.Add(itemT3.GetComponent<StandartItem>());
            Item itemT4 = itemFactory.CreateItem(item, TierType.FourthTier);
            itemT4.SynchronizeAllCharacteristics();
            ItemList.Add(itemT4.GetComponent<StandartItem>());
        }
    }
}
