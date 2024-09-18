using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFreezable
{
    bool IsFreezable { get; }  // Propiedad que indica si el enemigo puede ser congelado
    void Freeze(float duration);  // M�todo para congelar al enemigo
}
