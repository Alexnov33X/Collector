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
        LeanTween.moveLocal(gameObject, transform.localPosition + new Vector3(0, 100, 0), 1);
        yield return new WaitForSeconds(1f);
        //LeanTween.moveLocal(gameObject, transform.position + new Vector3(0, -100, 0), 0.01f);
        healthDamageText.gameObject.SetActive(false);
        transform.localPosition = transform.localPosition + new Vector3(0, -100, 0);
        //StartingPhase();
        //yield return playerHand.ExecuteHandPhases();
        //yield return new WaitForSeconds(delayBetweenPlayerAndEnemy);
        //yield return enemyHand.ExecuteHandPhases();
        //yield return new WaitForSeconds(AnimationAndDelays.instance.delayBetweenTurns);
        //EndingPhase();
    }
}
