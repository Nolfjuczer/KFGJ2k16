using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Vector2 _referenceResolution;
    [SerializeField]
    private List<Text> _damageTexts;
    [SerializeField]
    private Text _damageTextTemplate;
    [SerializeField]
    private List<Text> _expTexts;
    [SerializeField]
    private Text _expTextTemplate;
    [SerializeField]
    private float _textMovement;
    [SerializeField]
    private GameObject _start;
    [SerializeField]
    private GameObject _choose;
    [SerializeField]
    private GameObject _stage;
    [SerializeField]
    private GameObject _inGame;

    public void OnGameStateChange()
    {
        switch (GameController.Me.GameState)
        {
            case EGameState.START:
                _start.SetActive(true);
                _choose.SetActive(false);
                _stage.SetActive(false);
                _inGame.SetActive(false);
                break;
            case EGameState.CHOOSE:
                _start.SetActive(false);
                _choose.SetActive(true);
                break;
            case EGameState.STAGE:
                _inGame.SetActive(false);
                _choose.SetActive(false);
                _stage.SetActive(true);
                break;
            case EGameState.GAME:
                _stage.SetActive(false);
                _inGame.SetActive(true);
                break;
        }
    }

    public void UseDamage(Vector3 position, string value)
    {
        Text current = null;
        foreach (Text text in _damageTexts)
        {
            if (!text.gameObject.activeSelf)
            {
                current = text;
                break;
            }
        }
        if (current == null)
        {
            GameObject textObject = Instantiate(_damageTextTemplate.gameObject);
            current = textObject.GetComponent<Text>();
        }
        current.text = value;
        current.rectTransform.localPosition = WorldToScreenPosition(position + Vector3.up * 0.5f);
        StartCoroutine(AnimatedText(current));
    }

    private Vector2 WorldToScreenPosition(Vector3 worldPosition)
    {
        Vector2 ViewportPosition = _mainCamera.WorldToViewportPoint(worldPosition);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * _referenceResolution.x) - (_referenceResolution.x * 0.5f)),
            ((ViewportPosition.y * _referenceResolution.y) - (_referenceResolution.y * 0.5f)));
        return WorldObject_ScreenPosition;
    }

    private IEnumerator AnimatedText(Text text)
    {
        text.gameObject.SetActive(true);
        float timer = 0.0f;
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            text.rectTransform.localPosition += Vector3.up * _textMovement;
            yield return null;
        }
        text.gameObject.SetActive(false);
    } 

    public void UseExp(Vector3 position, string value)
    {
        Text current = null;
        foreach (Text text in _expTexts)
        {
            if (!text.gameObject.activeSelf)
            {
                current = text;
                break;
            }
        }
        if (current == null)
        {
            GameObject textObject = Instantiate(_expTextTemplate.gameObject);
            current = textObject.GetComponent<Text>();
        }
        current.text = value;
        current.rectTransform.localPosition = WorldToScreenPosition(position + Vector3.up * 0.5f);
        StartCoroutine(AnimatedText(current));
    }

}
