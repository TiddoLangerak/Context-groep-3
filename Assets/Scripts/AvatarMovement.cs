using UnityEngine;
using System.Collections;

/// <summary>
/// This class is responsible for keeping track of the
/// avatar movement.
/// </summary>
public class AvatarMovement : MonoBehaviour
{
	public float moveSpeed = 1;
	public int track = 2;

	/// <summary>
    /// Used for initialization. The Start method is called just
    /// before any of the Update methods is called the first time.
	/// </summary>
	void Start ()
    {
		StartCoroutine(sideMovement());
		StateManager.Instance.pauseOrUnpause();
	}
	
	/// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
	/// </summary>
	void Update ()
    {
        if (!StateManager.Instance.isPausing())
		    transform.Translate(Vector3.forward * 4 * Time.smoothDeltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -2);
    }

    /// <summary>
    /// A coroutine responsible for moving the avatar. Yields a
    /// WaitForSeconds to pause execution and prevent moving
    /// over multiple tracks at a time.
    /// </summary>
	IEnumerator sideMovement()
    {
		while (true) {
			if (Input.GetKey(KeyCode.A) && track > 1) {
				track--;
				transform.Translate(Vector3.left * 5 * StateManager.Instance.right());
				yield return new WaitForSeconds(0.2f);
			} else if (Input.GetKey(KeyCode.D) && track < 3) {
				track++;
				transform.Translate(Vector3.left * 5 * StateManager.Instance.left());
				yield return new WaitForSeconds(0.2f);
			} else {
				yield return 0;
			}
		}
	}
}
