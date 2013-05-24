using UnityEngine;
using System.Timers;
using System.Collections;

public class PointsPowerupBehaviour : MonoBehaviour, IPointsPowerupBehaviour
{
	protected PointsPowerup powerup;

	// Use this for initialization
	void Start ()
	{
		powerup = new PointsPowerup();
	}
	
	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShoppingCart")
        {
            StateManager.Instance.NewMoneyPowerup = true;
            Timer scoreBetterVisible = new Timer();
            scoreBetterVisible.Interval = 2000;
            scoreBetterVisible.Elapsed += ResetScoreVisibility;
            scoreBetterVisible.Start();

            powerup.Collision();
			Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Resets the score label to its default look.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResetScoreVisibility(object sender, ElapsedEventArgs e)
    {
        if (sender.GetType() == typeof(System.Timers.Timer))
        {
            ((System.Timers.Timer)sender).Dispose();
        }
        StateManager.Instance.NewMoneyPowerup = false;
    }

}
