using UnityEngine;

public class LootPositionCorrector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Loot loot))
            if (loot != null)
                Changeposition(loot.gameObject);

        if (other.gameObject.TryGetComponent(out Currency currency))
            if (currency != null)
                Changeposition(currency.gameObject);
    }

    private void Changeposition(GameObject gameObject)
    {
        Vector3 direction = new Vector3(0, gameObject.transform.position.y, 0) - gameObject.transform.position; 
        gameObject.transform.position += direction.normalized * transform.localScale.x;
    }
}
