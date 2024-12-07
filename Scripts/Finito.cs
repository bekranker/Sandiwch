using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Finito : MonoBehaviour
{
    [SerializeField] private Image _Panel;

    void Start()
    {
        _Panel.DOFade(0, 1);
    }
    void OnTriggerEnter2D(Collider2D cols)
    {
        if (cols.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            _Panel.DOFade(1, 1).OnComplete(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
        }
    }
}
