using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cinematic")]
    [SerializeField] GameObject TimeLine;
    [SerializeField] GameObject SceneFade;
    [SerializeField] fadeinout fadeinout;
    [SerializeField] GameObject vCam1;

    [Header("Player")]
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Transform StartSpawnPos;

    public enum GMState
    {
        Cinematic,
        InGame
    }
    public GMState gmstate;

    private void Awake()
    {
        fadeinout = GetComponentInChildren<fadeinout>();
    }
    private void OnEnable()
    {
        
    }
    void Start()
    {
        SceneFade.SetActive(true);
        StartCoroutine("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartScene()
    {
        fadeinout.FadeIn();
        yield return new WaitForSeconds(1f);
        TimeLine.SetActive(true);
        yield return new WaitForSeconds(6f);
        vCam1.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        fadeinout.FadeOut();
        yield return new WaitForSeconds(1.5f);
        fadeinout.CoverOn();
    }

    public void StartGame()
    {
        Instantiate(PlayerPrefab,StartSpawnPos);
    }
   
}
