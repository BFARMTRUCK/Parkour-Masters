using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class PointCollectorScript : MonoBehaviour
{
    public Text player1PointsText; // Text object for Player 1 points
    public Text player2PointsText; // Text object for Player 2 points
    public Text winnerText; // Text object for the winner
    public int player1Points = 0; // Points for the parent object of the PointCollector
    public int player2Points = 0; // Points for the parent object of the PointCollector
    private bool isCooldownPlayer1 = false;
    private bool isCooldownPlayer2 = false;

    void Update(){
        CheckForWinner();
    }

    void CheckForWinner(){
        if (player1Points >= 4){
            DisplayWinner("Player 1");
        }
        else if (player2Points >= 4){
            DisplayWinner("Player 2");
        }
    }
   void DisplayWinner(string winner){
    winnerText.text = winner + " wins!";
    StartCoroutine(ExitAfterDelay(5));
}

private IEnumerator ExitAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
}

    // OnTriggerEnter is called when the Collider other enters the trigger
// OnTriggerEnter is called when the Collider other enters the trigger
private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PointCollider")
        {
            if (transform.parent.name == "Player1" && !isCooldownPlayer1)
            {
                player1Points++;
                player1PointsText.text = "Player 1 Points: " + player1Points;
                StartCoroutine(CooldownPlayer1());
            }
            else if (transform.parent.name == "Player2" && !isCooldownPlayer2)
            {
                player2Points++;
                player2PointsText.text = "Player 2 Points: " + player2Points;
                StartCoroutine(CooldownPlayer2());
            }
        }
        else if (other.gameObject.tag == "Gold")
        {
            if (transform.parent.name == "Player1")
            {
                player1Points += 5;
                player1PointsText.text = "Player 1 Points: " + player1Points;
            }
            else if (transform.parent.name == "Player2")
            {
                player2Points += 5;
                player2PointsText.text = "Player 2 Points: " + player2Points;
            }
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