using UnityEngine;

namespace EventCallbacks
{
    public class DeathListener : MonoBehaviour
    {
        private void Start()
        {
            UnitDeathEvent.RegisterListener(OnUnitDied);
        }

        private void OnDestroy() {
            UnitDeathEvent.UnregisterListener(OnUnitDied);
        }

        private void OnUnitDied(UnitDeathEvent unitDeath)
        {
            Debug.Log("Alerted about unit death: " + unitDeath.unitGO.name);
        }
    }
}