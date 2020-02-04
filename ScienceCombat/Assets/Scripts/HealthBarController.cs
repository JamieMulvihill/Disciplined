using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;
    public float health;
    public float maxHealth;

   public void onTakeDamage(int damage){
       health -= damage;
       healthBar.fillAmount = health / maxHealth;
   }
}
