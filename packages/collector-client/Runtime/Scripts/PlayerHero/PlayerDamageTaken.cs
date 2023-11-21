using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDamageTaken : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthDamageText;

    void Start()
    {
        healthDamageText.gameObject.SetActive(false);
    }
    public void takeDamage(string health)
    {
        healthDamageText.text = health;
        healthDamageText.gameObject.SetActive(true);
        StartCoroutine(AnimateDamage());
    }
    private IEnumerator AnimateDamage()
    {
        LeanTween.moveLocal(gameObject, transform.localPosition + new Vector3(0, 100, 0), AnimationAndDelays.instance.heroDamageTakenAnimation);
        yield return new WaitForSeconds(AnimationAndDelays.instance.heroDamageTakenAnimation);
        healthDamageText.gameObject.SetActive(false);
        transform.localPosition = transform.localPosition + new Vector3(0, -100, 0);
    }
}
