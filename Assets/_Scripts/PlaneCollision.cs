using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    [SerializeField] private WordGameController _wordGameController;
    [SerializeField] private GameObject[] _effects;

    private void Start()
    {
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("letter"))
        {
            Destroy(collision.gameObject);
            if (GameObject.FindGameObjectWithTag("letter") != null)
            {
                int effectIndex = Random.Range(0, _effects.Length);
                GameObject newEffect = Instantiate(_effects[effectIndex]);
                newEffect.transform.position = collision.gameObject.transform.position;
            }

            

            string originalName = collision.gameObject.name;
            string cleanName = originalName.Replace("(Clone)", "").Trim();
            PlayerPrefs.SetString("CurrentLetter", cleanName);
            _wordGameController.UnlockNewLetter();
            
        }
    }
}
