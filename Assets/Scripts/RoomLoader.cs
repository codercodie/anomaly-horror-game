using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RoomLoader : MonoBehaviour
{

    // This is attached to the Player and determines which room to load depending on the collision on the doors

    public GameObject starterBedroom, bedroom, hallwayUpstairs, hallwayDownstairs, bathroom, office, utilityRoom, lounge, kitchen, outside, basement;
    public GameObject currentRoom;

    // Player starting positions when entering rooms
    // Rooms with only one enterance
    public Vector3 startingPosition, enterBedroom, enterLounge, enterOffice, enterBasement, enterUtilityRoom, enterBathroom;
    // Upstairs Hallway positions
    public Vector3 hallwayFromBedroom, hallwayFromOffice, hallwayFromBathroom, hallwayFromUtility, hallwayFromDownstairs;
    // Downstairs Hallway positions
    public Vector3 hallwayFromLounge, hallwayFromBasement, hallwayFromUpstairs, hallwayFromKitchen, hallwayFromFrontDoor;
    // Outside positions
    public Vector3 outsideFrontDoor, outsideBackDoor;
    // kitchen positions
    public Vector3 kitchenFromHallway, kitchenFromOutside;

    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        // Start in (starter) bedroom
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        foreach (GameObject room in rooms)
        {
            room.SetActive(false);
        }

        starterBedroom.SetActive(true);
        SetPositions();
        currentRoom = starterBedroom;
        transform.position = startingPosition;
    }

    private void SetPositions()
    {
        // all to be declared soon

        startingPosition = new Vector3(0, 0.75f, 0);
        enterBedroom = new Vector3(0, -2, 0);
        enterLounge = new Vector3(-3.56f, -0.58f, 0);
        enterOffice = new Vector3(-1.02f, 1.38f, 0);
        enterBasement = new Vector3(1, -0.55f, 0);
        enterUtilityRoom = new Vector3(-1, 0.25f, 0);
        enterBathroom = new Vector3(-1, 1.23f, 0);

        hallwayFromBedroom = new Vector3(-1.98f, -0.75f, 0);
        hallwayFromOffice = new Vector3(-3.01f, -1.27f, 0);
        hallwayFromBathroom = new Vector3(2.02f, -0.75f, 0);
        hallwayFromUtility = new Vector3(0.99f, -1.27f, 0);
        hallwayFromDownstairs = new Vector3(4, 1.27f, 0);

        hallwayFromLounge = new Vector3(2.48f, -0.58f, 0);
        hallwayFromBasement = new Vector3(-1.02f, 1.38f, 0);
        hallwayFromUpstairs = new Vector3(1, 0.14f, 0);
        hallwayFromKitchen = new Vector3(-2.48f, -0.58f, 0);
        hallwayFromFrontDoor = new Vector3(0, -2.14f, 0f);

        outsideBackDoor = new Vector3(-1.43f, 0.25f, 0);
        outsideFrontDoor = new Vector3(2, -1.88f, 0);

        kitchenFromHallway = new Vector3(3.61f, -0.58f, 0);
        kitchenFromOutside = new Vector3(-3.65f, - 0.58f, 0);

    }

    public void OnTriggerEnter2D(Collider2D trigger)
    {
        Debug.Log("Collided with: " + trigger.name);
        // Check if the collider is a trigger
        if (!trigger.isTrigger)
        {
            // exit the method if the collider is not a trigger
            return;
        }
        string roomPlayerIsEntering = trigger.name;

        StartCoroutine(LoadRoomAndSetPosition(currentRoom.name, roomPlayerIsEntering));
    }

        private void DisableRenderersInChildren()
        {
            foreach (Transform child in transform)
            {
                SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();

                if (childRenderer != null)
                {
                    childRenderer.enabled = false;
                }
            }
        }

        private void EnableRenderersInChildren()
        {
            foreach (Transform child in transform)
            {
                SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();

                if (childRenderer != null)
                {
                    childRenderer.enabled = true;
                }
            }
        }



        IEnumerator LoadRoomAndSetPosition(string roomLeaving, string room)
        {

            // disable the player's renderer components
            DisableRenderersInChildren();
        // hide all UI here todo
        Debug.Log("Setting " + currentRoom.name + " inactive");
            currentRoom.SetActive(false);
            
            yield return new WaitForEndOfFrame();

            // set the player position depending on the scene
            switch (room)
        {
            case "Bathroom":
                position = enterBathroom;
                bathroom.SetActive(true);
                currentRoom = bathroom;
                break;
            case "Bedroom":
                position = enterBedroom;
                bedroom.SetActive(true);
                currentRoom = bedroom;
                break;
            case "Office":
                position = enterOffice;
                office.SetActive(true);
                currentRoom = office;
                break;
            case "Utility Room":
                position = enterUtilityRoom;
                utilityRoom.SetActive(true);
                currentRoom = utilityRoom;
                break;
            case "Lounge":
                position = enterLounge;
                lounge.SetActive(true);
                currentRoom = lounge;
                break;
            case "Basement":
                position = enterBasement;
                basement.SetActive(true);
                currentRoom = basement;
                break;
            case "Upstairs Hallway":
                if (roomLeaving == "Bedroom")
                {
                    position = hallwayFromBedroom;
                }
                if (roomLeaving == "Utility Room")
                {
                    position = hallwayFromUtility;
                }
                if (roomLeaving == "Bathroom")
                {
                    position = hallwayFromBathroom;
                }
                if (roomLeaving == "Office")
                {
                    position = hallwayFromOffice;
                }
                if (roomLeaving == "Downstairs Hallway")
                {
                    position = hallwayFromDownstairs;
                }
                
                hallwayUpstairs.SetActive(true);
                currentRoom = hallwayUpstairs;
                break;
            case "Dowstairs Hallway":
                if (roomLeaving == "Lounge")
                {
                    position = hallwayFromLounge;
                }
                if (roomLeaving == "Kitchen")
                {
                    position = hallwayFromKitchen;
                }
                if (roomLeaving == "Basement")
                {
                    position = hallwayFromBasement;
                }
                if (roomLeaving == "Upstairs Hallway")
                {
                    position = hallwayFromUpstairs;
                }
                if (roomLeaving == "Outside")
                {
                    position = hallwayFromFrontDoor;
                }
                hallwayDownstairs.SetActive(true);
                currentRoom = hallwayDownstairs;
                break;
            case "Kitchen":
                if (roomLeaving == "Downstairs Hallway")
                {
                    position = kitchenFromHallway;
                }
                if (roomLeaving == "Outside")
                {
                    position = kitchenFromOutside;
                }
                currentRoom = kitchen;
                kitchen.SetActive(true);
                break;
            case "Outside":
                if (roomLeaving == "Downstairs Hallway")
                {
                    position = outsideFrontDoor;
                }
                if (roomLeaving == "Kitchen")
                {
                    position = outsideBackDoor;
                }
                outside.SetActive(true);
                currentRoom = outside;
                break;
        }  

        transform.position = position;
        Debug.Log("Setting " + currentRoom.name + " active");
        

            // enable the renderer and collider component
            EnableRenderersInChildren();

        }
    }
