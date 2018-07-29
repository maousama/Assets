using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AttackCalculator : MonoBehaviour
{
    ICharacter owner;

    private void Awake()
    {
        owner = GetComponent<ICharacter>();
    }


    private void OnTriggerEnter(Collider other)
    {
        AttackInfo info;

        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        //nearAttack
        if (layerName.Contains("Weapon"))
        {
            IWeapon weapon = other.GetComponent<IWeapon>();
            ICharacter owner  = weapon.owner;
            info = (owner.CurrentState as IAttackState).CurrentSkillInfo;

        }//Magic
        else
        {
            IMagic magic = other.GetComponent<IMagic>();
            CalculateMagicAttack(magic, magic.owner, magic.info,magic.weapon);
        }
        
    }



    private void CalculateNearAttack(ICharacter attacker,IWeapon weapon,AttackInfo skillInfo)
    {
        owner.HP -= (attacker.ATK + weapon.ATK) * skillInfo.DamageRate;
    }

    private void CalculateMagicAttack(IMagic magic,ICharacter attacker,AttackInfo skillInfo,IWeapon weapon)
    {
        owner.HP -= (attacker.ATK + magic.atk+(weapon==null?0:weapon.ATK)) * skillInfo.DamageRate;
    }


    public void AttackedByRangeAttack(AttackInfo info, IWeapon weapon)
    {
        owner.HP -= (info.Owner.ATK + weapon.ATK) * info.DamageRate;
    }

    
    public void AttackedByRangeAttack(AttackInfo skillInfo)
    {
        owner.HP -= skillInfo.Owner.ATK * skillInfo.DamageRate;
    }



}
