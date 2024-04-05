using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public AudioClip hurtClip, dieClip;

    public int HP;
    public PlayerInputManager input;
    public PlayerMovement movement;
    public PlayerStateManager state;
    public PlayerAnimator anim;
    public PlayerHit hit;
    public PlayerHP health;
    public Utils utils;
    public TMP_Text Message;
    internal void Hurt(Vector2 direction)
    {
        if(movement.knockBack <= 0 && state.currentState != PlayerState.DEAD){
            if(HP == 0){
                Die();
                return;
            }
            GM.Audio.SFX(hurtClip);
            movement.KnockBack(Mathf.Sign(direction.x));
            anim.HurtAnim();
            HP--;
            health.UpdateHP(HP);
        }
    }

    public void Die(){
        GM.Audio.SFX(dieClip);
        GM.Audio.SetMusic(null);
        anim.DieAnim();
        state.UpdateState(PlayerState.DEAD);
        movement.enabled = false;
        if(Models.UserId == null)
        {
            Message.text = "You're Not Logged In. Please Log In to save record and Claim Token";
        }
        else
        {
            utils = gameObject.AddComponent<Utils>();
            utils.EndGameSession( Credentials.GameId, Models.UserId, Models.Score);
        }
        
    }
}
