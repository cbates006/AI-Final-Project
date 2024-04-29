using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum BattleState {START,PLAYER1TURN,PLAYER2TURN,PLAYER1WON,PLAYER2WON}
public enum BattleState2 {DODGEACTIVE,DODGENOTACTIVE}
public enum BattleState3 {RECKLESS2,NOTRECKLESS2}
public enum BattleState4 {RECKLESS1,NOTRECKLESS1}

public class BattleSystem : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform Player1BattleStation;
    public Transform Player2BattleStation;

    public Text dialogueText;

    public BattleHUD player1HUD;
    public BattleHUD player2HUD;

    public Unit player1Unit;
    public Unit player2Unit;

    public BattleState state;
    public BattleState2 state2;
    public BattleState3 state3;
    public BattleState4 state4;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    //setup the battle on start
    IEnumerator SetupBattle() {
        GameObject player1GO = Instantiate(player1Prefab,Player1BattleStation);
        player1Unit = player1GO.GetComponent<Unit>();
        GameObject player2GO = Instantiate(player2Prefab,Player2BattleStation);
        player2Unit = player2GO.GetComponent<Unit>();

        int initiative = Random.Range(1, 3);

        player1HUD.SetHUD(player1Unit);
        player2HUD.SetHUD(player2Unit);

        state2 = BattleState2.DODGENOTACTIVE;
        state3 = BattleState3.NOTRECKLESS2;
        state4 = BattleState4.NOTRECKLESS1;

        if (initiative == 1)
        {
            dialogueText.text = player1Unit.unitName + " goes first!";
            yield return new WaitForSeconds(3f);
            state = BattleState.PLAYER1TURN;
            Player1Turn();
        }
        else if (initiative == 2)
        {
            dialogueText.text = player2Unit.unitName + " goes first!";
            yield return new WaitForSeconds(3f);
            state = BattleState.PLAYER2TURN;
            Player2Turn();
        }
    }

    IEnumerator Player1Attack()
    {
        int attackRoll = Random.Range(1, 21);
        //for if the opponent has dodged or used a reckless attack
        int roll2 = Random.Range(1, 21);
        int disadvantage;
        int advantage;
        if (roll2 > attackRoll)
        {
            disadvantage = attackRoll;
        }
        else
        {
            disadvantage = roll2;
        }

        if (roll2 < attackRoll)
        {
            advantage = attackRoll;
        }
        else
        {
            advantage = roll2;
        }
        int damage = Random.Range(4, 11);

        if (state4 == BattleState4.NOTRECKLESS1) //player 1 did not do a reckless attack
        {
            if (state2 == BattleState2.DODGENOTACTIVE && state3 == BattleState3.NOTRECKLESS2) //if the opponent hasn't dodged or reckless, normal attack
            {
                if (attackRoll > 7)
                {
                    player2Unit.TakeDamage(damage);
                    player2HUD.SetHP(player2Unit.currentHP);
                    dialogueText.text = "AI attacks for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "AI's attack missed!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
            else if (state2 == BattleState2.DODGEACTIVE && state3 == BattleState3.NOTRECKLESS2) //dodged but wasn't reckless, disadvantage
            {

                if (disadvantage > 7)
                {
                    player2Unit.TakeDamage(damage);
                    player2HUD.SetHP(player2Unit.currentHP);
                    dialogueText.text = "AI attacks for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "AI's attack missed!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
                state2 = BattleState2.DODGENOTACTIVE;
            }
            else if (state2 == BattleState2.DODGEACTIVE && state3 == BattleState3.RECKLESS2) //dodged and was reckless, normal attack
            {
                if (attackRoll > 7)
                {
                    player2Unit.TakeDamage(damage);
                    player2HUD.SetHP(player2Unit.currentHP);
                    dialogueText.text = "AI attacks for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "AI's attack missed!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
                state2 = BattleState2.DODGENOTACTIVE;
                state3 = BattleState3.NOTRECKLESS2;
            }
            else if (state2 == BattleState2.DODGENOTACTIVE && state3 == BattleState3.RECKLESS2) //didn't dodge and was reckless, advantage
            {
                if (advantage > 7)
                {
                    player2Unit.TakeDamage(damage);
                    player2HUD.SetHP(player2Unit.currentHP);
                    dialogueText.text = "AI attacks for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "AI's attack missed!";
                }
                state3 = BattleState3.NOTRECKLESS2;
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
        }
        else if (state4 == BattleState4.RECKLESS1) //you were reckless
        {
            if (state2 == BattleState2.DODGEACTIVE) //roll normally if opponent dodged
            {
                if (attackRoll > 7)
                {
                    player2Unit.TakeDamage(damage);
                    player2HUD.SetHP(player2Unit.currentHP);
                    dialogueText.text = "AI reckless for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "AI's reckless attack missed!";
                }
                state2 = BattleState2.DODGENOTACTIVE;
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
            else if (state2 == BattleState2.DODGENOTACTIVE)  //advantage if opponent didn't dodge
            {
                if (advantage > 7)
                {
                    player2Unit.TakeDamage(damage);
                    player2HUD.SetHP(player2Unit.currentHP);
                    dialogueText.text = "AI reckless for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "AI reckless for " + damage + " damage!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
        }


        //check if opponent was killed and change turns or end battle
        yield return new WaitForSeconds(3f);
        bool isDead = player2Unit.CheckDeath();
        if (isDead)
        {
            state = BattleState.PLAYER1WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYER2TURN;
            Player2Turn();
        }
    }

    IEnumerator Player2Attack()
    {
        int attackRoll = Random.Range(1, 21);
        //for if the opponent has dodged or used a reckless attack
        int roll2 = Random.Range(1, 21);
        int disadvantage;
        int advantage;
        if (roll2 > attackRoll)
        {
            disadvantage = attackRoll;
        }
        else
        {
            disadvantage = roll2;
        }

        if (roll2 < attackRoll)
        {
            advantage = attackRoll;
        }
        else
        {
            advantage = roll2;
        }
        int damage = Random.Range(4, 11);

        if (state3 == BattleState3.NOTRECKLESS2) //player 1 did not do a reckless attack
        {
            if (state2 == BattleState2.DODGENOTACTIVE && state4 == BattleState4.NOTRECKLESS1) //if the opponent hasn't dodged or reckless, normal attack
            {
                if (attackRoll > 7)
                {
                    player1Unit.TakeDamage(damage);
                    player1HUD.SetHP(player1Unit.currentHP);
                    dialogueText.text = "Hits for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "The attack missed!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
            else if (state2 == BattleState2.DODGEACTIVE && state4 == BattleState4.NOTRECKLESS1) //dodged but wasn't reckless, disadvantage
            {

                if (disadvantage > 7)
                {
                    player1Unit.TakeDamage(damage);
                    player1HUD.SetHP(player1Unit.currentHP);
                    dialogueText.text = "Hits for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "The attack missed!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
                state2 = BattleState2.DODGENOTACTIVE;
            }
            else if (state2 == BattleState2.DODGEACTIVE && state4 == BattleState4.RECKLESS1) //dodged and was reckless, normal attack
            {
                if (attackRoll > 7)
                {
                    player1Unit.TakeDamage(damage);
                    player1HUD.SetHP(player1Unit.currentHP);
                    dialogueText.text = "Hits for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "The attack missed!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
                state2 = BattleState2.DODGENOTACTIVE;
                state4 = BattleState4.NOTRECKLESS1;
            }
            else if (state2 == BattleState2.DODGENOTACTIVE && state4 == BattleState4.RECKLESS1) //didn't dodge and was reckless, advantage
            {
                if (advantage > 7)
                {
                    player1Unit.TakeDamage(damage);
                    player1HUD.SetHP(player1Unit.currentHP);
                    dialogueText.text = "Hits for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "The attack missed!";
                }
                state4 = BattleState4.NOTRECKLESS1;
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
        }
        else if (state3 == BattleState3.RECKLESS2) //you were reckless
        {
            if (state2 == BattleState2.DODGEACTIVE) //roll normally if opponent dodged
            {
                if (attackRoll > 7)
                {
                    player1Unit.TakeDamage(damage);
                    player1HUD.SetHP(player1Unit.currentHP);
                    dialogueText.text = "Hits for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "The attack missed!";
                }
                state2 = BattleState2.DODGENOTACTIVE;
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
            else if (state2 == BattleState2.DODGENOTACTIVE)  //advantage if opponent didn't dodge
            {
                if (advantage > 7)
                {
                    player1Unit.TakeDamage(damage);
                    player1HUD.SetHP(player1Unit.currentHP);
                    dialogueText.text = "Hits for " + damage + " damage!";
                }
                else
                {
                    dialogueText.text = "The attack missed!";
                }
                Debug.Log("attackroll = " + attackRoll);
                Debug.Log("roll2 = " + roll2);
            }
        }


        //check if opponent was killed and change turns or end battle
        yield return new WaitForSeconds(3f);
        bool isDead = player1Unit.CheckDeath();
        if (isDead)
        {
            state = BattleState.PLAYER2WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYER1TURN;
            Player1Turn();
        }
    }

    IEnumerator Player1Dodge()
    {
        state2 = BattleState2.DODGEACTIVE;
        dialogueText.text = "AI dodged!";
        yield return new WaitForSeconds(3f);
        state = BattleState.PLAYER2TURN;
        Player2Turn();
        
    }

    IEnumerator Player2Dodge()
    {
        dialogueText.text = "Player 2 dodged!";
        yield return new WaitForSeconds(3f);
        state = BattleState.PLAYER1TURN;
        Player1Turn();
    }

    IEnumerator Player1Heal()
    {
        int heal = Random.Range(1, 11);
        bool isDead = player1Unit.CheckDeath();

        if (isDead)
        {
            state = BattleState.PLAYER2WON;
            StartCoroutine(EndBattle());
        }
        else { 
            player1Unit.Heal(heal);
            player1HUD.SetHP(player1Unit.currentHP);
            dialogueText.text = "AI healed " + heal + " HP!";
            yield return new WaitForSeconds(3f);
            state = BattleState.PLAYER2TURN;
            Player2Turn();
        }
        state2 = BattleState2.DODGENOTACTIVE;
    }

    IEnumerator Player2Heal()
    {
        int heal = Random.Range(1, 11);
        bool isDead = player2Unit.CheckDeath();

        if (isDead)
        {
            state = BattleState.PLAYER2WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            player2Unit.Heal(heal);
            player2HUD.SetHP(player2Unit.currentHP);
            dialogueText.text = "Player 2 healed " + heal + " HP!";
            yield return new WaitForSeconds(3f);
            state = BattleState.PLAYER1TURN;
            Player1Turn();
        }
        state2 = BattleState2.DODGENOTACTIVE;
    }

    void Player1Turn()
    {
        //dialogueText.text = "Player 1, choose an action:";
        //the following code is for AI combat only. comment out to run player vs player combat as normal

        //did opponent reckless attack?
        if (state3 == BattleState3.RECKLESS2) //yes
        {
            //is my health <= 10?
            if (player1Unit.currentHP <= 10) //yes
            {
                //is my opponent's health <= 10?
                if (player2Unit.currentHP <= 10) //yes
                {
                    StartCoroutine(Player1Attack());
                }
                else //no
                {
                    int percentRoll1 = Random.Range(1, 101);
                     if (percentRoll1 <= 30)
                    {
                        StartCoroutine(Player1Heal());
                    }
                     else if (percentRoll1 > 30 && percentRoll1 <= 80)
                    {
                        StartCoroutine(Player1Attack());
                    }
                     else
                    {
                        StartCoroutine(Player1Dodge());
                    }
                }
            }
            else //no
            {
                StartCoroutine(Player1Attack());
            }
        }
        else //no
        {
            //did opponent dodge?
            if (state2 == BattleState2.DODGEACTIVE) //yes
            {
                //is my health <= 15?
                if (player1Unit.currentHP <= 15) //yes
                {
                    //is my opponent's health <= 10?
                    if (player2Unit.currentHP <= 10) //yes
                    {
                        int percentRoll2 = Random.Range(1, 101);
                        if (percentRoll2 <= 25)
                        {
                            state4 = BattleState4.RECKLESS1;
                            StartCoroutine(Player1Attack());
                        }
                        else if (percentRoll2 > 25 && percentRoll2 <= 50)
                        {
                            StartCoroutine(Player1Attack());
                        }
                        else if (percentRoll2 >50 && percentRoll2 <= 85)
                        {
                            StartCoroutine(Player1Heal());
                        }
                        else
                        {
                            StartCoroutine(Player1Dodge());
                        }
                    }
                    else //no
                    {
                        int percentRoll3 = Random.Range(1, 101);
                        if (percentRoll3 <= 75)
                        {
                            StartCoroutine(Player1Heal());
                        }
                        else
                        {
                            StartCoroutine(Player1Dodge());
                        }
                    }
                }
                else //no
                {
                    state4 = BattleState4.RECKLESS1;
                    StartCoroutine(Player1Attack());
                }
            }
            else //no
            {
                //is my health <= 10?
                if (player1Unit.currentHP <= 10) //yes
                {
                    //is my opponent's health <= 10?
                    if (player2Unit.currentHP <= 10) //yes
                    {
                        int percentRoll4 = Random.Range(1, 101);
                        if (percentRoll4 <= 40)
                        {
                            StartCoroutine(Player1Attack());
                        }
                        else if (percentRoll4 > 40 && percentRoll4 <= 70)
                        {
                            StartCoroutine(Player1Heal());
                        }
                        else
                        {
                            state4 = BattleState4.RECKLESS1;
                            StartCoroutine(Player1Attack());
                        }
                    }
                    else //no
                    {
                        int percentRoll5 = Random.Range(1, 101);
                        if (percentRoll5 <= 70)
                        {
                            StartCoroutine(Player1Heal());
                        }
                        else
                        {
                            StartCoroutine(Player1Dodge());
                        }
                    }
                }
                else //no
                {
                    //is my opponent's health <= 10?
                    if (player2Unit.currentHP <= 10) //yes
                    {
                        int percentRoll6 = Random.Range(1, 101);
                        if (percentRoll6 <= 70)
                        {
                            state4 = BattleState4.RECKLESS1;
                            StartCoroutine(Player1Attack());
                        }
                        else
                        {
                            StartCoroutine(Player1Attack());
                        }
                    }
                    else //no
                    {
                        StartCoroutine(Player1Attack());
                    }
                }
            }
        }

    }


    void Player2Turn()
    {
        dialogueText.text = "Player 2, choose an action:";
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.PLAYER1WON)
        {
            dialogueText.text = "AI wins!";
        }else if(state == BattleState.PLAYER2WON)
        {
            dialogueText.text = "Player 2 wins!";
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Credits");
    }

    public void OnAttackButton()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            //StartCoroutine(Player1Attack());
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            StartCoroutine(Player2Attack());
        }
        else
        {
            return;
        }
    }

    public void OnHealButton()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            //StartCoroutine(Player1Heal());
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            StartCoroutine(Player2Heal());
        }
        else
        {
            return;
        }
    }

    public void OnDodgeButton()
    {
        state2 = BattleState2.DODGEACTIVE;
        if (state == BattleState.PLAYER1TURN)
        {
            //StartCoroutine(Player1Dodge());
        }
        else if (state == BattleState.PLAYER2TURN)
        {

            StartCoroutine(Player2Dodge());

        }
        else
        {
            return;
        }
    }

    public void OnRecklessButton()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            //state4 = BattleState4.RECKLESS1;
            //StartCoroutine(Player1Attack());
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            state3 = BattleState3.RECKLESS2;
            StartCoroutine(Player2Attack());

        }
        else
        {
            return;
        }
    }
}