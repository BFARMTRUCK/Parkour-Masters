using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCollectorScript : MonoBehaviour
{
    public Text player1PointsText; // Text object for Player 1 points
    public Text player2PointsText; // Text object for Player 2 points
    public int points = 0; // Points for the parent object of the PointCollector
    private bool isCooldownPlayer1 = false;
    private bool isCooldownPlayer2 = false;

    // OnTriggerEnter is called when the Collider other enters the trigger
// OnTriggerEnter is called when the Collider other enters the trigger
private void OnTriggerEnter(Collider other)
{
    // Check if the collider belongs to a PointCollider
    if (other.gameObject.name == "PointCollider")
    {
        // Check which player scored and if they are not in cooldown
        if (transform.parent.name == "Player1" && !isCooldownPlayer1)
        {
            // Increment points
            points++;
            player1PointsText.text = "Player 1 Points: " + points;
            StartCoroutine(CooldownPlayer1());
        }
        else if (transform.parent.name == "Player2" && !isCooldownPlayer2)
        {
            points++;
            player2PointsText.text = "Player 2 Points: " + points;
            StartCoroutine(CooldownPlayer2());
        }

        Debug.Log(transform.parent.name + " has " + points + " points.");
    }
    else if (other.gameObject.tag == "Gold")
        {
            // Increment points by 5
            points += 5;
            // Update the points text for the correct player
            if (transform.parent.name == "Player1")
            {
                player1PointsText.text = "Player 1 Points: " + points;
            }
            else if (transform.parent.name == "Player2")
            {
                player2PointsText.text = "Player 2 Points: " + points;
            }
            // Destroy the gold object
            Destroy(other.gameObject);
        }
}

private IEnumerator CooldownPlayer1()
{
    isCooldownPlayer1 = true;
    yield return new WaitForSeconds(5);
    isCooldownPlayer1 = false;
}

private IEnumerator CooldownPlayer2()
{
    isCooldownPlayer2 = true;
    yield return new WaitForSeconds(5);
    isCooldownPlayer2 = false;
}
}