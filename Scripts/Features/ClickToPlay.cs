using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;
public class ClickToPlay : MonoBehaviour
{
    [SerializeField] VideoPlayer _videoPlayer;
    private bool _canPlay;
    void Update()
    {
        if (_canPlay) return;
        if (Input.GetMouseButtonDown(0))
        {
            DOVirtual.DelayedCall(25.22f, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
            _videoPlayer.Play();
            _canPlay = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
