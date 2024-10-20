using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretMover : IDisposable
{
    private InputHandler inputHandler;
    private ITurretFactory turretFactory;

    private Camera camera;
    private Turret movingTurret;
    private TurretTile startTurretTile;
    private ParticleSystem sparks;

    private Plane plane;
    private Vector3 offset;

    private bool isDragAndDrop;
    private bool canDragAndDrop;
    public TurretMover(InputHandler inputHandler, ITurretFactory turretFactory, ParticleSystem sparks)
    {
        this.inputHandler = inputHandler;
        this.turretFactory = turretFactory;
        this.sparks = GameObject.Instantiate(sparks);

        inputHandler.startDragAndDrop += StartDragAndDrop;
        inputHandler.endDragAndDrop += EndtDragAndDrop;
        inputHandler.dragAndDrop += DragAndDropUpdate;

        camera = Camera.main;
    }

    public void Dispose()
    {
        inputHandler.startDragAndDrop -= StartDragAndDrop;
        inputHandler.endDragAndDrop -= EndtDragAndDrop;
        inputHandler.dragAndDrop -= DragAndDropUpdate;
    }

    private void StartDragAndDrop(Vector2 touchPosition)
    {
        Debug.Log("Start");
        Ray ray = camera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,100) && hit.transform.TryGetComponent(out TurretTile turretTile) && turretTile.TryGetTurret(out Turret turret))
        {
            isDragAndDrop = true;
            movingTurret = turret;
            movingTurret.isMoving = true;

            if (movingTurret == null) return;

            startTurretTile = turretTile;
            plane = new Plane(turretTile.transform.up, turretTile.transform.position);

            float planeDist;
            plane.Raycast(ray, out planeDist);
            offset = (movingTurret.transform.position - ray.GetPoint(planeDist)) + new Vector3(0,1,0);
        }
    }
    private void DragAndDropUpdate(Vector2 touchPosition)
    {
        if (!isDragAndDrop || movingTurret == null) return;

        Ray ray = camera.ScreenPointToRay(touchPosition);


        float planeDist;
        plane.Raycast(ray, out planeDist);
        movingTurret.transform.position = ray.GetPoint(planeDist) + offset;

    }
    private void EndtDragAndDrop(Vector2 touchPosition)
    {
        Debug.Log("End");
        if (movingTurret == null) return;

        Ray ray = camera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100) && hit.transform.TryGetComponent(out TurretTile turretTile))
        {
            if(turretTile.TryGetTurret(out Turret turret) && startTurretTile != turretTile && turret.level != 5 && turret.level == startTurretTile.curentTurret.level)
            {
                int level = turret.level;
                level++;

                UnityEngine.Object.Destroy(startTurretTile.curentTurret.gameObject);
                UnityEngine.Object.Destroy(turretTile.curentTurret.gameObject);

                Turret newTurret = turretFactory.CreateTurret(level);
                newTurret.transform.position = turretTile.transform.position;
                turretTile.curentTurret = newTurret;

                sparks.gameObject.SetActive(true);
                sparks.transform.position = newTurret.transform.position;
                sparks.Play();

                newTurret.isMoving = false;
                isDragAndDrop = false;
                movingTurret = null;
                startTurretTile.curentTurret = null;
                return;

            }
        }

        startTurretTile.curentTurret.isMoving = false;
        movingTurret.transform.position = startTurretTile.transform.position;
        movingTurret = null;
        isDragAndDrop = false;
    }
}


