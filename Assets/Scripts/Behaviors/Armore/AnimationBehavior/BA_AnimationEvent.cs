using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_AnimationEvent : MonoBehaviour
{
    public GameObject holderProyectile1, holderProyectile2, holderImpactProyectile,weaponPoint,player,alturaMarker;
    public Vector3 proyectilePos;

    public GameObject VfxSmokeExplosionWave, VfxExplosionWave, VfxExplosionBars, VfxSmokeExplosionPieces, VfxSmokeExplosionFount, VfxSmokeSweepWave, VfxSmokeSweepPieces, VfxSmokeWalk, VfxSlashAdd, VfxSlashAlp, VfxSlashTrail, VfxShieldSecondary;
    public Transform SmokeExplosionPosition, SmokeWavePosition, SmokeWalkPositionLeft, SmokeWalkPositionRight, SlashAttackPosition, VfxShieldPosition;
    public Animator animShield;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //////ANIMATION EVENTS////////
    
    public void proyectileCombo()
    {
        // Puede dar problemas segun la posicion del arma.
        //Crear y colocar los holders y poner el punto del arma en el rig como punto de spawn.
        proyectilePos = new Vector3(weaponPoint.transform.position.x, alturaMarker.transform.position.y, weaponPoint.transform.position.z);
        GameObject toInstantiate1 = Instantiate(holderProyectile1, proyectilePos, Quaternion.identity);
        toInstantiate1.transform.LookAt(player.transform.position);
        toInstantiate1.GetComponent<Holder>().SetGun(2, 0.5f, 5, false);
        GameObject toInstantiate2 = Instantiate(holderProyectile2, proyectilePos, Quaternion.identity);
        toInstantiate2.transform.LookAt(player.transform.position);
        toInstantiate2.GetComponent<Holder>().SetGun(2, 1, 5, false);

    }

    public void ImpactProyectile()
    {
        proyectilePos = new Vector3(weaponPoint.transform.position.x, alturaMarker.transform.position.y, weaponPoint.transform.position.z);
        weaponPoint.GetComponent<Gunner_Test>().proyectilePos = proyectilePos;
        //Crear vector 3 para subir 0,5 unidades el punto de instancia

        //GameObject toInstantiate = Instantiate(holderImpactProyectile, weaponPoint.transform.position, Quaternion.identity);
        weaponPoint.GetComponent<Gunner_Test>().CircleGun();

    }
    

    public void CounterDamage()
    {
        player.GetComponent<PlayerStats>().TakeDamage(30);
    }


    public void playAnimationProtectilAttack()
    {
        MiFmod.Instance.Play("SFX_2d/SonidoProyectilesBoss");
    }
    public void playAnimationHorizontalAttack()
    {
        MiFmod.Instance.Play("SFX_2d/MazazoBoss");
    }
    public void playAnimationVerticalAttack()
    {
        MiFmod.Instance.Play("SFX_2d/ContraataqueBoss");
    }
    public void playAnimationShieldRelease()
    {
        MiFmod.Instance.Play("SFX_2d/ShieldReleaseBoss");
    }
    public void playAnimationTaparseEscudo()
    {
        MiFmod.Instance.Play("SFX_2d/TaparseConEscudoBoss");
    }


    public void PlayParticleExplosion()
    {
        Instantiate(VfxSmokeExplosionWave, SmokeExplosionPosition); 
        Instantiate(VfxSmokeExplosionPieces, SmokeExplosionPosition); 
        Instantiate(VfxSmokeExplosionFount, SmokeExplosionPosition);
        Instantiate(VfxExplosionWave, SmokeExplosionPosition);
        Instantiate(VfxExplosionBars, SmokeExplosionPosition);

    }
    public void PlayParticleSweep()
    {
        
        Instantiate(VfxSmokeSweepPieces, SmokeWavePosition);
        Instantiate(VfxSmokeSweepWave, SmokeWavePosition);

        Instantiate(VfxSlashAlp, SlashAttackPosition);
        Instantiate(VfxSlashAdd, SlashAttackPosition);

    }
    public void PlayParticleWalkRight()
    {
        Instantiate(VfxSmokeWalk, SmokeWalkPositionRight);
    }
    public void PlayParticleWalkLeft()
    {
        Instantiate(VfxSmokeWalk, SmokeWalkPositionLeft);
    }
    public void StartTrail()
    {
        VfxSlashTrail.SetActive(true);
    }
    public void StopTrail()
    {
        VfxSlashTrail.SetActive(false);
    }
    public void ShieldOn()
    {
        //Instantiate(VfxShield, ShieldDefensePosition);
        animShield.Play("ShieldOn");
        Instantiate(VfxShieldSecondary, VfxShieldPosition);
    }
    public void ShieldOff()
    {
        Debug.Log("Off se llama");
        animShield.Play("ShieldOff");
    }
}