﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jamma : MonoBehaviour
{
    [Header("ライフが変更できるよ")]
    public int Life_Zyama;

    [Header("移動速度が変更できるよ")]
    public float Speed;

    [Header("低速時の速度が変更できるよ")]
    [SerializeField]
    private float SlowSpeed;

    [Header("ボールのプレハブを代入する変数")]
    public GameObject ball;

    [Header("(スキル)カウントダウンの速度")]
    [SerializeField]
    public float Skill_Count;

    [Header("(スキル)加速スキルの効果時間")]
    [SerializeField]
    private float Time_Over;

    [Header("GameDirector")]
    [SerializeField]
    public GameObject GameDirector;

    [SerializeField]
    GameObject[] Life;

    private Rigidbody rb;

    //スティック入力を格納する変数
    float Horizontal;

    Vector3 direction;//移動量を格納する変数

    public bool Frieze = false;//操作を停止させるフラグ(ポーズ等)

    public bool Shot = false;//玉を射出しているかの判定

    public bool Skill_3 = false;//時間を加速するスキル
    float Timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //ポーズ中は操作不可
        if (Frieze == false)
        {
            //入力受付
            Horizontal = Input.GetAxis("Horizontal_Ja");

            //移動量計算
            if(Input.GetKey(KeyCode.LeftShift))
            {
                direction = new Vector3(Horizontal, 0, 0).normalized * SlowSpeed;
            }
            else
            {
                direction = new Vector3(Horizontal, 0, 0).normalized * Speed;
            }

            //発射処理
            if (Input.GetButtonDown("〇_Button_2"))
            {
                if (Shot == false && Life_Zyama > 0)
                {
                    Vector3 tmp = this.gameObject.transform.position;
                    tmp.z = this.gameObject.transform.position.z + 1.4f;
                    tmp.y = 0.5f;
                    Instantiate(ball,tmp, Quaternion.identity);
                    ball.GetComponent<Ball>().Zyamama = this.gameObject;
                    Shot = true;
                }
            }
        }

        //あとでここの処理まとめよう
        if(Life_Zyama == 1)
        {
            Destroy(Life[1]);
        }
        if(Life_Zyama == 0)
        {
            Destroy(Life[0]);
        }

        if (Skill_3 == true)
        {
            Skill_Move_3();
        }
    }

    void FixedUpdate()
    {
        if (Frieze == false)
        {
            //移動量を振り当てる(実際に移動させる)処理
            rb.velocity = direction;
        }
        else
        {
            rb.velocity = new Vector3 (0,0,0);
        }
    }

    //ジャママーの移動が速くなるスキル
    private void Skill_Move_1()
    {

    }

    //ボールが2つになるスキル
    private void Skill_Move_2()
    {

    }

    //制限時間が短くなる
    private void Skill_Move_3()
    {
        //ローカル変数に保存して書きやすくする
        bool Skill = GameDirector.GetComponent<Game_Director>().Time_SKill = true;

        //ゲームの制限時間を取得
        Timer = GameDirector.GetComponent<Game_Director>().Timer;

        Timer -= Skill_Count;//制限時間のカウントダウン

        Time_Over -= Time.deltaTime;//スキル継続時間のカウントダウン

        //スキルの時間が切れたらフラグを元に戻す
        if(Time_Over <= 0)
        {
            Skill = false;
            Skill_3 = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //クローンのオブジェクトなので(Clone)の文字を空白に変えてパーツ名と同じにする
        string chackname = collision.gameObject.name.Replace("(Clone)", "");
        if (chackname == "Ball")
        {
            Sound_Manager.Instance.PlaySE(SE.Bounce,0.5f,5.3f);
        }
    }
}
