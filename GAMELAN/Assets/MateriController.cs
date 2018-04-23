﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MateriController : MonoBehaviour {
    public static MateriController self;
    public GameObject parent;
    public Image gambar;
    public Text penjelasan;
    public Button next;
    public Button prev;
    private GamesMateri materis;
    private int current = 0;
    private int lastSaw = 0;
    private int last = 0;
    private string materiPath = null;
    private float dt = 0;
    private float time;
    private MiniGameLoaderScript m;
    // Use this for initialization
    void Start() {
        
        self = this;
        parent.SetActive(false);
        next.onClick.AddListener(Next);
        prev.onClick.AddListener(Prev);
      
    }

    private void Update()
    {
        if (Time.time - time > dt || current < lastSaw) {
            next.transform.gameObject.SetActive(true);
        }
    }
    public void startMateri() {
        dt = 0;
        start();
    }
    public void startPreGameMateri(MiniGameLoaderScript m) {
        dt = 2;
        this.m = m;
        start();
    }
    public void setMateriPath(string name) {
        materiPath = name;
    }
    void start() {
        parent.SetActive(true);
        prev.transform.gameObject.SetActive(false);
        materis = GamesMateri.load(materiPath);
        current = 0;
        last = materis.materis.Count;
        time = Time.time;
        showmateri(current);
    }
    void Next() {
        if (current < last-1)
        {
            lastSaw += current == lastSaw ? 1 : 0;
            showmateri(++current);
            time = Time.time;
            next.transform.gameObject.SetActive(false);
            prev.transform.gameObject.SetActive(true);
        }
        else finish();
    }
    void Prev() {
        if (current > 0)
        {
            showmateri(--current);
        }
       
    }

    void showmateri(int i) {
        penjelasan.text = materis.materis[i].Penjelasan;
        gambar.sprite = Resources.Load<Sprite>("Materi/Gambar/" + materis.materis[i].imageName);
        if(current <= 0)
        {
            prev.transform.gameObject.SetActive(false);
        }
        Debug.Log("Materi/Gambar/" + materis.materis[i].imageName);

    } 
    void finish() {
        this.parent.SetActive(false);
        if (m != null)
        {
            m.gotoMiniGames();
        }
        else m = null;
    }
}