using System.Collections;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite[] diceFaces;
    private bool isRolling = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        diceFaces = Resources.LoadAll<Sprite>("dice sides/dadospritesheet24x24");
        if (diceFaces.Length == 0)
        {
            Debug.LogError("Error: No sprites found.");
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            if (hitCollider != null && hitCollider.gameObject == this.gameObject && !isRolling)
            {
                StartCoroutine(RollRoutine());
            }
        }
    }

    private IEnumerator RollRoutine()
    {
        isRolling = true;

        int finalFace = 0;
        for (int i = 0; i <= 20; i++)
        {
            int randomIndex = Random.Range(0, diceFaces.Length);
            spriteRenderer.sprite = diceFaces[randomIndex];
            finalFace = randomIndex + 1;
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log(finalFace);
        isRolling = false;
    }
}