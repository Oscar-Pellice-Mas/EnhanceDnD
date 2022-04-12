using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        // Activar focus entity
            // Mostrar UI amb info del personatge.

        // Activar modo jugador.


        offset = transform.position - MapSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        // Nomes en moment de modo jugador

        Vector3 pos = MapSystem.GetMouseWorldPosition() + offset;
        transform.position = MapSystem.instance.SnapCoordinateToGrid(pos);
    }

    private void OnMouseOver()
    {
        MapSystem.instance.entityInfoHover.transform.position = Input.mousePosition;
        // Contar que pasin X segons.
        // Activar hover UI amb info del personatje.
        // Desactivar quan surti
    }

    private void OnMouseEnter()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            MapSystem.instance.entityInfoHover.GetComponent<EntityInfoHover>().Character = raycastHit.collider.gameObject.GetComponent<PlayerCharacter>();
        }
        MapSystem.instance.entityInfoHover.SetActive(true);
        MapSystem.instance.entityInfoHover.GetComponent<EntityInfoHover>().EntityRefresh();
    }

    private void OnMouseExit()
    {
        MapSystem.instance.entityInfoHover.SetActive(false);
    }

}
