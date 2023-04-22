using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IGameModeListener
{
    void onTestStack();
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private List<IGameModeListener> listeners = new List<IGameModeListener>();

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    instance = new GameObject("EventManager").AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    Camera mainCamera;

    [SerializeField]
    private GameObject jengaDataPanal;
    public TMP_Text grade, domain, cluster, standardID, standardDescription;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.collider.gameObject;

                if(hit.transform.GetComponent<JengaBlockPrefab>())
                {
                    showJengaBlockData(hit.transform.GetComponent<JengaBlockPrefab>().myData);
                }
            }
        }
    }

    private void showJengaBlockData(DataModel objectData)
    {
        StopAllCoroutines();
        grade.text = objectData.grade;
        domain.text = objectData.domain;
        cluster.text = objectData.cluster;
        standardID.text = objectData.standardid;
        standardDescription.text = objectData.standarddescription;
        jengaDataPanal.SetActive(true);
        StartCoroutine(hideJengaBlockPanal());
    }

    private IEnumerator hideJengaBlockPanal()
    {
        yield return new WaitForSeconds(7f);
        jengaDataPanal.SetActive(false);
    }

    public void RegisterListener(IGameModeListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(IGameModeListener listener)
    {
        listeners.Remove(listener);
    }

    public void triggerOnTestStack()
    {
        foreach (IGameModeListener listener in listeners)
        {
            listener.onTestStack();
        }
    }

}
