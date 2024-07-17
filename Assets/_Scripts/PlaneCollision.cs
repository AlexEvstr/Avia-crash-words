using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    [SerializeField] private WordGameController _wordGameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("letter"))
        {
            string originalName = collision.gameObject.name;
            string cleanName = originalName.Replace("(Clone)", "").Trim();
            PlayerPrefs.SetString("CurrentLetter", cleanName);
            _wordGameController.UnlockNewLetter();
            Destroy(collision.gameObject);
        }
    }
}
