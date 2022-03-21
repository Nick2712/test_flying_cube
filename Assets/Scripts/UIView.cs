using UnityEngine;
using UnityEngine.UI;

namespace TestFlyingCube
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private InputField _spawnTime;
        [SerializeField] private InputField _speed;
        [SerializeField] private InputField _distance;
        [SerializeField] private Button _start;

        [SerializeField] private Text _spawnTimeInfo;
        [SerializeField] private Text _speedInfo;
        [SerializeField] private Text _distanceInfo;

        public InputField SpawnTime => _spawnTime;
        public InputField Speed => _speed;
        public InputField Distance => _distance;
        public Button Start => _start;

        public Text SpawnTimeInfo => _spawnTimeInfo;
        public Text SpeedInfo => _speedInfo;
        public Text DistanceInfo => _distanceInfo;
    }
}