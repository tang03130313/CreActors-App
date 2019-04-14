using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class control : MonoBehaviour {
    public GameObject cam1, cam2; //兩個不同的攝影機
    public GameObject obj1, obj2; //兩個不同的GameObject
    public VideoPlayer vPlayer;
    public AudioSource music = GameObject.FindGameObjectWithTag("Background Music").GetComponent<AudioSource>();
    // Use this for initialization
    void Start () {
        movie();
    }
	
	// Update is called once per frame
	void Update () {
       /* if (!vPlayer.isPlaying)
            return_main();*/

    }

    public void movie() {
        cam1.SetActive(true);
        cam2.SetActive(false);
        obj1.SetActive(true);
        obj2.SetActive(false);
        music.Stop();
        vPlayer.Play();
    }

    public void return_main() {
        cam1.SetActive(false);
        cam2.SetActive(true);
        obj1.SetActive(false);
        obj2.SetActive(true);
        music.Play();
        vPlayer.Stop();
    }
}
