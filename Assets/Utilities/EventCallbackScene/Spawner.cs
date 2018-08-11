using UnityEngine;

namespace EventCallbacks
{
    public class Spawner : MonoBehaviour
    {
        public GameObject unitPrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnUnit();
            }
        }

        private void SpawnUnit()
        {
            GameObject go = Instantiate(unitPrefab);
            go.name = "unit";
        }
    }
}