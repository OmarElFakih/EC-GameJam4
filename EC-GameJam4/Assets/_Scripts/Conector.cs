using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conector : MonoBehaviour
{
    private GameManager GM = null;


    [SerializeField]
    private LayerMask mask = default;


    [SerializeField]
    private float _t = 0f;

    
    private Color _normalColor = Color.white;

    [SerializeField]
    private Color _missColor = Color.white;
    [SerializeField]
    private Color _clickColor = Color.white;

    [SerializeField]
    private SpriteRenderer[] _renderers = null;



    private void OnEnable()
    {
        EventManager.OnTimer += TryClick;
    }

    private void OnDisable()
    {
        EventManager.OnTimer -= TryClick;
    }


    private void Start()
    {
        GM = GameManager.manager;
        _normalColor = GetComponent<SpriteRenderer>().color;
    }


    private void Update()
    {
        foreach (SpriteRenderer r in _renderers)
        {
            r.color = Color.Lerp(r.color, _normalColor, _t * Time.deltaTime);
        }
    }



    public void TryClick()
    {
        StartCoroutine(RayCaster());
    }

    IEnumerator RayCaster()
    {
        yield return new WaitForSeconds(.2f);

        Vector3 _dir = transform.TransformDirection(Vector3.up);

        if (Physics2D.Raycast(transform.position, _dir, Mathf.Infinity, mask))
        {
            GM.AddScore(100);
            foreach (SpriteRenderer r in _renderers)
            {
                r.color = _clickColor;
            }
        }
        else
        {
            GM.Miss();
            foreach (SpriteRenderer r in _renderers)
            {
                r.color = _missColor;
            }
        }
    }
}
