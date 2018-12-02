using UnityEngine;

public class StatsManager : MonoBehaviour {
    private static StatsManager _instance;
    public static StatsManager Instance {
        get {
            return _instance;
        }
        set {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }

    public int BodiesLost { get; set; }

    private float _startTime;
    public float StartTime { get { return _startTime; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _startTime = Time.time;
    }
}
