﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeCutscene : MonoBehaviour {

    public Image CutsceneImage;
    public TextScroller TextScroller;
    public Sprite[] Sprites;
    public Image FadeToWhite;

    private int _sceneNumber = 1;

    private void OnEnable()
    {
        CutsceneImage.gameObject.SetActive(true);
        CutsceneImage.canvasRenderer.SetAlpha(0.01f);
        CutsceneImage.CrossFadeAlpha(1.0f, 2.0f, false);
        Invoke("ActivateFirstScene", 2);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {

            switch (_sceneNumber)
            {
                case 1:
                    SingleScene("", Sprites[1]);
                    break;
                case 2:
                    SingleScene("", Sprites[2]);
                    break;
                case 3:
                    SingleScene("", Sprites[3]);
                    break;
                case 4:
                    TextScroller.DisplayText("");
                    FadeToWhite.gameObject.SetActive(true);
                    FadeToWhite.canvasRenderer.SetAlpha(0.01f);
                    FadeToWhite.CrossFadeAlpha(1.0f, 2.0f, false);
                    break;
                case 5:
                    SceneManager.LoadScene(0);
                    break;
                default:
                    break;
            }

            _sceneNumber++;

            if (_sceneNumber > 6)
            {
                CutsceneImage.sprite = null;
                CutsceneImage.gameObject.SetActive(false);
                TextScroller.DisplayText("");
                Destroy(gameObject);
            }
        }
    }


    private void SingleScene(string text, Sprite sprite)
    {
        CutsceneImage.sprite = sprite;
        TextScroller.DisplayText(text);
    }

    public void ActivateFirstScene()
    {
        SingleScene("", Sprites[0]);
    }
}
