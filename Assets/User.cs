using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Fungus;
using System;

public class User : MonoBehaviour {

    public int money = 0;
    public int level = 0;
    public int experience = 0;
    public int signed = 0;
    public int date = 0;

    public Text levelText;
    public Text moneyText;
    public Text signedText;
    public Text dateText;
    public Slider experienceSlider;

    public Text treshText_1, treshText_2, lampText_1, lampText_2, chairText_1, chairText_2;

    string tresh1, tresh2, lamp1, lamp2, chair1, chair2;

    // Use this for initialization
    void Start () {
        date = System.DateTime.Now.Day;
        Debug.Log("load");
        load();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void save() {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("experience", experience);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("signed", signed);
        PlayerPrefs.SetInt("date", date);

        PlayerPrefs.SetInt("treshText_1",   Convert.ToInt32(treshText_1.text));
        PlayerPrefs.SetInt("treshText_2", Convert.ToInt32(treshText_2.text));
        PlayerPrefs.SetInt("lampText_1", Convert.ToInt32(lampText_1.text));
        PlayerPrefs.SetInt("lampText_2", Convert.ToInt32(lampText_2.text));
        PlayerPrefs.SetInt("chairText_1", Convert.ToInt32(chairText_1.text));
        PlayerPrefs.SetInt("chairText_2", Convert.ToInt32(chairText_2.text));
    }

    public void load() {

        Debug.Log("load");
        level = (PlayerPrefs.HasKey("level")) ? PlayerPrefs.GetInt("level") : 0;
        experience = (PlayerPrefs.HasKey("experience")) ? PlayerPrefs.GetInt("experience") : 0;
        money = (PlayerPrefs.HasKey("money")) ? PlayerPrefs.GetInt("money") : 0;
        signed = (PlayerPrefs.HasKey("signed")) ? PlayerPrefs.GetInt("signed") : 0;
        int date_temp = (PlayerPrefs.HasKey("date")) ? PlayerPrefs.GetInt("date") : 0;

        tresh1 = (PlayerPrefs.HasKey("treshText_1")) ? Convert.ToString(PlayerPrefs.GetInt("treshText_1")) : "1";
        tresh2 = (PlayerPrefs.HasKey("treshText_2")) ? Convert.ToString(PlayerPrefs.GetInt("treshText_2")) : "0";
        lamp1 = (PlayerPrefs.HasKey("lampText_1")) ? Convert.ToString(PlayerPrefs.GetInt("lampText_1")) : "1";
        lamp2= (PlayerPrefs.HasKey("lampText_2")) ? Convert.ToString(PlayerPrefs.GetInt("lampText_2")) : "0";
        chair1 = (PlayerPrefs.HasKey("chairText_1")) ? Convert.ToString(PlayerPrefs.GetInt("chairText_1")) : "1";
        chair2 = (PlayerPrefs.HasKey("chairText_2")) ? Convert.ToString(PlayerPrefs.GetInt("chairText_2")) : "0";

        /*if (level == 0) {
            control temp = gameObject.GetComponent<control>();
            temp.movie();
        }*/


        if (signed == 0)
            increase_signed();
    }
    

    public void increase_signed(){
        signed++;
        increase_experience();
        increase_money();
        display();
    }

    public void increase_experience() {
        experience += 25;
        if (experience == 100) {
            level++;
            experience = 0;
        }
        display();
        
    }

    public void decrease_money(int num)
    {
        money -= num;
        display();
    }

    public void increase_money()
    {
        money +=50;
        display();
    }

    public void display() {
        levelText.text = level.ToString();
        moneyText.text = money.ToString();
        signedText.text = signed.ToString();
        dateText.text = date.ToString();
        experienceSlider.value = experience;
       // treshText_1.text = tresh1;
       // treshText_2.text = tresh2;
       // lampText_1.text = lamp1;
       // lampText_2.text = lamp2;
        //chairText_1.text = chair1;
        //chairText_2.text = chair2;
        save();
    }

    public void reset_level() {
        level = 0;
        display();
    }

    public void reset_experience() {
        experience = 0;
        display();
    }

    public void reset_money()
    {
        money =0;
        display();   
    }

    public void reset()
    {
        level = 0;
        experience = 0;
        money = 0;
        signed = 0;
        date = 0;
        increase_signed();
        PlayerPrefs.DeleteAll();
        display();
    }

    public void date_check() {
        int date_temp = System.DateTime.Now.Day;
        if (date_temp != date)
        {
            increase_signed();
        }
        date = date_temp;
        display();
    }

    public void decrease_money_50() {
        money -= 50;
        display();
    }

    public void decrease_money_70()
    {
        money -= 70;
        display();
    }

    public void increase_money_25()
    {
        money += 25;
        display();
    }

    public void increase_money_35()
    {
        money += 35;
        display();
    }


}
