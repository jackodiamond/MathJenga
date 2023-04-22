using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaBlockPrefab : MonoBehaviour, IGameModeListener
{
    private DataModel _myData;

    public DataModel myData
    {
        get { return _myData; }
        set
        {
            _myData = value;
            Instantiate();
        }
    }

    [SerializeField]
    private Material glassMaterial, woodMaterial, stoneMaterial;

    public void Instantiate()
    {
        switch(_myData.mastery)
        {
            case 0:
                GetComponent<Renderer>().material = glassMaterial;
                break;
            case 1:
                GetComponent<Renderer>().material = woodMaterial;
                break;
            case 2:
                GetComponent<Renderer>().material = stoneMaterial;
                break;
        }
    }

    private void Start()
    {
        GameManager.Instance.RegisterListener(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisterListener(this);
    }

    void IGameModeListener.onTestStack()
    {
        if (_myData.mastery == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
