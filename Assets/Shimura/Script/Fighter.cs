using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Macine
{
    [SerializeField] float wingsArea;     //翼の面積
    [SerializeField] float airDensity;    //空気密度
    [SerializeField] float speed;         //速度
    [SerializeField] float cl;            //揚力係数

    [SerializeField] protected Rigidbody Rb;


    // Start is called before the first frame update
    public void Start()
    {
        Rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Accelerate();
    }

    public Fighter(int num, float Attack, float Acceleration)
    {

    }

    void Accelerate()
    {
        var forces = Vector3.zero;
        forces = speed * transform.forward;
        Debug.Log("transform.forward"+ transform.forward);
        Debug.Log("forces" + forces);
        //揚力の方向
        Vector3 liftDirection = Vector3.Cross(Rb.velocity, transform.forward).normalized;
        var liftPower = lift(airDensity, wingsArea, speed, cl) / 1000 * Time.deltaTime;
        forces += liftPower * liftDirection;
        //MaxSpeedよりも速度が小さければ加速（等速を維持したい）
        if(Rb.velocity.magnitude<10)Rb.AddForce(forces);
    }

    //揚力を取得
    float lift(float air, float wing, float spd, float cl)
    {
        //揚力  =空気密度*翼の面積*速度の二乗*揚力係数/ 2
        float l =   air  *   wing *  spd*spd *   cl   / 2;
        return l;
    }

    //参考　https://teratail.com/questions/331607

    public void Rotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //機体を傾けて右へ曲がる
        }
        if (Input.GetKey(KeyCode.A))
        {
            //機体を傾けて左へ曲がる
        }
    }
}
