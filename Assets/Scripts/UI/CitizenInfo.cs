using TMPro;
using UnityEngine;

public class CitizenInfo : MonoBehaviour
{
    [Header("Unemployed Citizens")]
    public TextMeshProUGUI unemployedCitizenText;
    void Update()
    {
        //Unemployed Citizens
        unemployedCitizenText.text = "Unemployed Citizens Count: " + CitizenController.Instance.unemployedRecords.Count + "/" + CitizenController.Instance.records.Count;
    }
}
