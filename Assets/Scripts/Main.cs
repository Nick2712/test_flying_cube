using UnityEngine;
using UnityEngine.UI;

namespace TestFlyingCube
{
    public class Main : MonoBehaviour
    {
        private const float MIN_SPEED = 0.1f;
        private const float MAX_SPEED = 50;
        private const float MIN_SPAWN_TIME = 0.01f;
        private const float MAX_SPAWN_TIME = 10;
        private const float MIN_DISTANCE = 1;
        private const float MAX_DISTANCE = 20;

        [SerializeField] private UIView _uiView;
        [SerializeField] private GameObject _cube;
        [SerializeField] private Camera _camera;

        private float _spawnTime = 0.5f;
        private float _speed = 10;
        private float _distance = 10;
        private bool _isActionStart;
        private float _timeToSpawn;

        private void Start()
        {
            _uiView.SpawnTime.text = _spawnTime.ToString();
            _uiView.Speed.text = _speed.ToString();
            _uiView.Distance.text = _distance.ToString();
            _uiView.Start.onClick.AddListener(OnStartAction);
            _uiView.SpawnTime.onEndEdit.AddListener(OnSpawnTimeChange);
            _uiView.Speed.onEndEdit.AddListener(OnSpeedChange);
            _uiView.Distance.onEndEdit.AddListener(OnDistanceChange);
            SetInfoField(_uiView.SpeedInfo, MIN_SPEED, MAX_SPEED);
            SetInfoField(_uiView.SpawnTimeInfo, MIN_SPAWN_TIME, MAX_SPAWN_TIME);
            SetInfoField(_uiView.DistanceInfo, MIN_DISTANCE, MAX_DISTANCE);
        }

        private void Update()
        {
            if(_timeToSpawn > 0)
            {
                _timeToSpawn -= Time.deltaTime;
                if(_timeToSpawn <= 0)
                {
                    SetAction(true);
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (_uiView.gameObject.activeInHierarchy) Application.Quit();
                else ResetAction();
            }
        }

        private void FixedUpdate()
        {
            if(_isActionStart)
            {
                MoveCube();
            }
        }

        private void MoveCube()
        {
            _cube.transform.Translate(Vector3.right * _speed * Time.fixedDeltaTime);
            if(_cube.transform.position.x > _distance)
            {
                _timeToSpawn = _spawnTime;
                DestroyCube();
            }
        }

        private void DestroyCube()
        {
            _cube.transform.position = Vector3.zero;
            SetAction(false);
        }

        private void SetAction(bool isAction)
        {
            _cube.SetActive(isAction);
            _isActionStart = isAction;
        }

        private void ResetAction()
        {
            _timeToSpawn = 0;
            DestroyCube();
            _uiView.gameObject.SetActive(true);
        }

        private void OnStartAction()
        {
            SetAction(true);
            _uiView.gameObject.SetActive(false);
            Vector3 cameraPosition = _camera.transform.position;
            cameraPosition.x = _distance / 2;
            _camera.transform.position = cameraPosition;
        }

        private void OnSpeedChange(string value)
        {
            TryChangeValue(value, _uiView.Speed, MIN_SPEED, MAX_SPEED, ref _speed);
        }

        private void OnSpawnTimeChange(string value)
        {
            TryChangeValue(value, _uiView.SpawnTime, MIN_SPAWN_TIME, MAX_SPAWN_TIME, ref _spawnTime);
        }

        private void OnDistanceChange(string value)
        {
            TryChangeValue(value, _uiView.Distance, MIN_DISTANCE, MAX_DISTANCE, ref _distance);
        }

        private void TryChangeValue(string value, InputField inputField, float minValue, float maxValue, ref float field)
        {
            if (float.TryParse(value, out float result))
            {
                if (result < minValue) result = minValue;
                if (result > maxValue) result = maxValue;
                field = result;
            }
            inputField.text = field.ToString();
        }

        private void SetInfoField(Text infoField, float minValue, float maxValue)
        {
            infoField.text = infoField.text + $" {minValue} - {maxValue}";
        }
    }
}