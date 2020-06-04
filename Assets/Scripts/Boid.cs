using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    
    List <Boid> allBoids = new List <Boid>();

    static float comWeight = .01f;
    static float velWeight = .04f;
    static float colWeight = .005f;
    static float wallWeight = 100.0f;

    static int totalBoids = 200;

    static float maxSpeed = 5.0f;

    Vector2 pos;
    Vector2 vel;

    public Boid(float startX, float startY){
        allBoids.Add(this);
        pos = new Vector2(startX,startY);
        vel = new Vector2(0,0);
    }

    void Update(){
        float distance;


        Vector2 com = new Vector2(0,0);

        Vector2 avgVel = new Vector2(0,0);

        Vector2 totalCollisionAvoidance = new Vector2(0,0);
        Vector2 collisionAvoidance;

        int inRange = 0;
        foreach(Boid i in allBoids){
            distance = Vector2.Subtract(i.pos,this.pos).Length;
            if (distance > viewDist) continue;
            inRange++;
            com = Vector2.Add(i.pos);

            avgVel = Vector2.Add(avgVel,i.vel);
            if (distance == 0) continue;
            collisionAvoidance = Vector2.Subtract(this.pos,i.pos);
            collisionAvoidance = Vector2.Multiply(1-(distance/viewDist));
            totalCollisionAvoidance = Vector2.Add(totalCollisionAvoidance, collisionAvoidance);

        }
        com = Vector2.Divide(com,inRange);

        avgVel = Vector2.Divide(avgVel,i.vel);

        Vector2 comDispalcement = Vector2.Subtract(com,this.pos);

        //FINAL STEPS
        Vector2 acc = new Vector2(0,0);
        acc = Vector2.Add(acc,Vector2.Multiply(comDispalcement,comWeight));
        acc = Vector2.Add(acc,Vector2.Multiply(avgVel,velWeight));
        acc = Vector2.Add(acc,Vector2.Multiply(totalCollisionAvoidance,colWeight));
        //add wall addition here

        this.vel = Vector2.Add(Vector2.Multiply(acc,Time.deltaTime),this.vel));
        this.pos = Vector2.Add(this.pos, Vector2.Multiple(this.vel,Time.deltaTime));


    }

}