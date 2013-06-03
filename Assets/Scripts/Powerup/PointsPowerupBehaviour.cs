using UnityEngine;
using System.Timers;

/// <summary>
/// Implements the behaviour of the points powerup
/// </summary>
public class PointsPowerupBehaviour : MonoBehaviour, IPointsPowerupBehaviour
{
    /// <summary>
    /// The PointsPowerup associated with this behaviour
    /// </summary>
    protected PointsPowerup powerup;

    /// <summary>
    /// The audio manager (used for sound effect after collision)
    /// </summary>
    private AudioBehaviour audioManager;

    /// <summary>
    /// Initializes the instance variables. (This function is automatically called by Unity)
    /// </summary>
    void Start()
    {
        powerup = new PointsPowerup();
        this.audioManager = GameObject.Find("2DAudio").GetComponent<AudioBehaviour>();
    }

    /// <summary>
    /// Increases the score and shows it clearly on the screen for 2 seconds
    /// </summary>
    /// <param name="collision">The collision belonging to this game object</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Avatar")
        {
            StateManager.Instance.NewMoneyPowerup = true;
            Timer scoreBetterVisible = new Timer();
            scoreBetterVisible.Interval = 2000;
            scoreBetterVisible.Elapsed += ResetScoreVisibility;
            scoreBetterVisible.Start();

            powerup.Collision();
            this.audioManager.Play("money");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Resets the score label to its default look after the timer is expired.
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">The arguments belonging to the event</param>
    private void ResetScoreVisibility(object sender, ElapsedEventArgs e)
    {
        if (sender.GetType() == typeof(System.Timers.Timer))
        {
            ((System.Timers.Timer)sender).Dispose();
        }
        StateManager.Instance.NewMoneyPowerup = false;
    }

}
