using UnityEngine;

public class TurretFactory
{
    public static GameObject CreateTurret(TurretConfig tutorConf, Vector3 spawnPoint)
    {
        GameObject turret = Object.Instantiate(tutorConf.visualPrefab, spawnPoint, Quaternion.identity);
        // Настраиваем turret

        return null;
    }
}