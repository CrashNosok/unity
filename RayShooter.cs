using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>(); // доступ к другим компонентам, присоединенных к этому же объекту

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        // Input.GetMouseButtonDown(0) - если нажата ЛКМ, будет true, в ином случае false
        if (Input.GetMouseButtonDown(0))
        {
            // pixelWidth - ширина экрана в пикселях
            // pixelHeight - высота экрана в пикселях
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            // создание луча методом ScreenPointToRay()
            // в скобки передаём Vector3 и из вектора будет выходить наш луч
            Ray ray = _camera.ScreenPointToRay(point);

            // переменная для хранения того, с чем пересекся луч:
            RaycastHit hit;

            // Physics.Raycast(луч, информация_о_луче)
            // Physics.Raycast() возвращает true, если луч с чем-то столкнулся, в ином случае false
            // передаем hit как ссылку с помощью out
            if (Physics.Raycast(ray, out hit))
            {
                // пишем в консоль координаты пересечения луча с препятствием
                //Debug.Log("hit " + hit.point);

                // полкчаем объект, в который попали
                GameObject hitObject = hit.transform.gameObject;
                // получаем компонент ReactiveTarget, который висит на враге
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                // если у врага есть компонент ReactiveTarget, пишем в консоль "target hit"
                // иначе, делаем сферу
                if (target != null)
                {
                    //Debug.Log("target hit");
                    target.ReactToHit();
                }
                else
                    StartCoroutine(SphereIndicator(hit.point));

            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(2);

        Destroy(sphere);
    }
}
