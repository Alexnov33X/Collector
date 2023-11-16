using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endText;

    [SerializeField] private TMP_Text rankPointsPlayer;
    [SerializeField] private float speedGetPoints;
    [SerializeField] private Image iconRank;
    
    [SerializeField] private TMP_Text textNewPoints;
    [SerializeField] private float timeMoveTextNewPoints, delayShowNewPoints;
    [SerializeField] private Vector3 offsetTextNewPoints;
    [SerializeField] private Color colorLoseTextNewPoints;
    [SerializeField] private Color colorVictoryTextNewPoints;
    // В зависимости от победы или поражения меняем текст и показываем его

    [SerializeField] private Sounder sounder;

    private bool result;
    public void EndGame(bool isVictory)
    {
        result = isVictory;
        Window currentWindow = GetComponent<Window>();
        rankPointsPlayer.text = PlayerStats.RankPoints.ToString();
        iconRank.sprite = SystemRank.SpritesRank[PlayerStats.rank];
        gameObject.SetActive(true);
        currentWindow.OpenNextWindow(currentWindow);
        if(isVictory)
        {
            endText.text = "VICTORY!";
            textNewPoints.text = "+" + SystemRank.victoryPoints;
            textNewPoints.color = colorVictoryTextNewPoints;
            sounder.PlaySound("victory");
        }
        else
        {
            endText.text = "DEFEAT";
            textNewPoints.text = "-" + SystemRank.losePoints;
            textNewPoints.color = colorLoseTextNewPoints;
        }
        Invoke("ShowPoints", delayShowNewPoints);
        StartCoroutine(GetPoints(result));
    }
    public void ShowPoints()
    {
        Vector3 basePosition = textNewPoints.transform.position;
        textNewPoints.transform.position -= offsetTextNewPoints;
        Color baseColor = textNewPoints.color;
        baseColor.a = 1;
        var fadeoutcolor = baseColor; 
        fadeoutcolor.a = 0; 
        LeanTween.value(gameObject, UpdateValueExampleCallback,fadeoutcolor, baseColor, timeMoveTextNewPoints);
        LeanTween.move(textNewPoints.gameObject, basePosition, timeMoveTextNewPoints);

    }

    private  void UpdateValueExampleCallback(Color val)
    {
        textNewPoints.color = val;
    }
    public IEnumerator GetPoints(bool isVictory)
    {
        yield return  new WaitForSecondsRealtime(timeMoveTextNewPoints + delayShowNewPoints + 0.2f);
        int currentPointsRank = PlayerStats.RankPoints;
        int sumPoints;
        if (isVictory)
        {
            sumPoints  = PlayerStats.RankPoints + SystemRank.victoryPoints;
            PlayerStats.RankPoints = sumPoints;
            for (int i = currentPointsRank + 1; i <= sumPoints; i++ )
            {
                rankPointsPlayer.text = i.ToString();
                yield return  new WaitForSecondsRealtime(speedGetPoints);
            }
        }
        else
        {
            sumPoints = PlayerStats.RankPoints - SystemRank.losePoints;
            PlayerStats.RankPoints = sumPoints;
            for (int i = currentPointsRank - 1; i > sumPoints; i--)
            {
                rankPointsPlayer.text = i.ToString();
                yield return  new WaitForSecondsRealtime(speedGetPoints);
            }
        }
        iconRank.sprite = SystemRank.SpritesRank[PlayerStats.rank];
    }

}
