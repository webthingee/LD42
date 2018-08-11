using UnityEngine;

namespace EventCallbacks
{
    public class Health : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Die();
            }
        }

        private void Die()
        {
            // I am dying for some reason.

            UnitDeathEvent udei = new UnitDeathEvent();
            udei.description = "Unit "+ gameObject.name +" has died.";
            udei.unitGO = gameObject;
            udei.FireEvent();

            Destroy(gameObject);
        }
    }
}