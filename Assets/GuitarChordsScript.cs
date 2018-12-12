using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KModkit;
using UnityEngine;

public class GuitarChordsScript : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public Fret[] frets;
    public Material unselected;
    public Material selected;
    public KMSelectable playBut;
    public AudioClip[] strings;
    public AudioClip[] chords;
    private int stringIndex;

    public TextMesh level1Chord;
    public TextMesh level2Chord;
    public TextMesh level3Chord;
    public string[] chordOptions;
    public Renderer[] levelIndicators;
    public Material[] indicatorOptions;

    public string level1Target;
    public string level2Target;
    public string level3Target;
    public int capoTarget = 0;
    private int cycle = 0;

    public string programmedChord;
    public int programmedCapo;
    public int stage = 1;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;

    bool IsCorrect(int[] expectedIndices)
    {
        for (int boolIndex = 0; boolIndex < frets.Length; ++boolIndex)
        {
            if (frets[boolIndex].Status != expectedIndices.Contains(boolIndex))
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        if (IsCorrect(new int[] { 0, 4, 5, 9, 13, 14 }))
        {
            programmedChord = "Ab"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 2, 3, 4, 11 }))
        {
            programmedChord = "Ab7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 2, 3, 11 }))
        {
            programmedChord = "Abm7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 8, 9, 10 }))
        {
            programmedChord = "A"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 4, 8, 9 }))
        {
            programmedChord = "Am"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 8, 9, 10, 17 }))
        {
            programmedChord = "A7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 4, 8, 9, 17 }))
        {
            programmedChord = "Am7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 1, 5, 14, 15, 16 }))
        {
            programmedChord = "Bb"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 1, 5, 10, 14, 15 }))
        {
            programmedChord = "Bbm"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 10, 14, 15, 23 }))
        {
            programmedChord = "Bbm7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 7, 11, 20, 21, 22 }))
        {
            programmedChord = "B"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 7, 11, 16, 20, 21 }))
        {
            programmedChord = "Bm"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 2, 7, 9, 11 }))
        {
            programmedChord = "B7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 1, 3, 5, 10, 14 }))
        {
            programmedChord = "Bm7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 4, 8, 12, 13 }))
        {
            programmedChord = "C"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 4, 8, 13, 15 }))
        {
            programmedChord = "C7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 2, 4, 15, 17 }))
        {
            programmedChord = "Cm7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 3, 5, 10, 14 }))
        {
            programmedChord = "C#"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 3, 8, 10 }))
        {
            programmedChord = "C#m"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 10, 14, 21, 23 }))
        {
            programmedChord = "C#7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 8, 10, 21, 23 }))
        {
            programmedChord = "C#m7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 9, 11, 16 }))
        {
            programmedChord = "D"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 5, 9, 16 }))
        {
            programmedChord = "Dm"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 4, 9, 11 }))
        {
            programmedChord = "D7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 4, 5, 9 }))
        {
            programmedChord = "Dm7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 11, 15, 20, 22 }))
        {
            programmedChord = "Ebm"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 2, 10, 15, 17 }))
        {
            programmedChord = "Eb7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 2, 10, 11, 15 }))
        {
            programmedChord = "Ebm7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 3, 7, 8 }))
        {
            programmedChord = "E"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 7, 8 }))
        {
            programmedChord = "Em"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 3, 7, 8, 16 }))
        {
            programmedChord = "E7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 7 }))
        {
            programmedChord = "Em7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 0, 2, 4, 5, 9, 13 }))
        {
            programmedChord = "F7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 0, 2, 3, 4, 5, 13 }))
        {
            programmedChord = "Fm7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 6, 10, 11, 15, 19, 20 }))
        {
            programmedChord = "F#"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 6, 9, 10, 11, 19, 20 }))
        {
            programmedChord = "F#m"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 10, 15, 20 }))
        {
            programmedChord = "F#7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 8, 9, 10, 11 }))
        {
            programmedChord = "F#m7"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 7, 12, 17 }))
        {
            programmedChord = "G"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 0, 3, 4, 5, 13, 14 }))
        {
            programmedChord = "Gm"; programmedCapo = 0;
        }
        else if (IsCorrect(new int[] { 5, 7, 12 }))
        {
            programmedChord = "G7"; programmedCapo = 0;
        }


        else if (IsCorrect(new int[] { 0 + 18, 4 + 18, 5 + 18, 9 + 18, 13 + 18, 14 + 18 }))
        {
            programmedChord = "Ab"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 2 + 18, 3 + 18, 4 + 18, 11 + 18 }))
        {
            programmedChord = "Ab7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 2 + 18, 3 + 18, 11 + 18 }))
        {
            programmedChord = "Abm7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 8 + 18, 9 + 18, 10 + 18 }))
        {
            programmedChord = "A"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 4 + 18, 8 + 18, 9 + 18 }))
        {
            programmedChord = "Am"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 8 + 18, 9 + 18, 10 + 18, 17 + 18 }))
        {
            programmedChord = "A7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 4 + 18, 8 + 18, 9 + 18, 17 + 18 }))
        {
            programmedChord = "Am7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 1 + 18, 5 + 18, 14 + 18, 15 + 18, 16 + 18 }))
        {
            programmedChord = "Bb"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 1 + 18, 5 + 18, 10 + 18, 14 + 18, 15 + 18 }))
        {
            programmedChord = "Bbm"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 10 + 18, 14 + 18, 15 + 18, 23 + 18 }))
        {
            programmedChord = "Bbm7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 7 + 18, 11 + 18, 20 + 18, 21 + 18, 22 + 18 }))
        {
            programmedChord = "B"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 7 + 18, 11 + 18, 16 + 18, 20 + 18, 21 + 18 }))
        {
            programmedChord = "Bm"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 2 + 18, 7 + 18, 9 + 18, 11 + 18 }))
        {
            programmedChord = "B7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 1 + 18, 3 + 18, 5 + 18, 10 + 18, 14 + 18 }))
        {
            programmedChord = "Bm7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 4 + 18, 8 + 18, 12 + 18, 13 + 18 }))
        {
            programmedChord = "C"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 4 + 18, 8 + 18, 13 + 18, 15 + 18 }))
        {
            programmedChord = "C7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 2 + 18, 4 + 18, 15 + 18, 17 + 18 }))
        {
            programmedChord = "Cm7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 3 + 18, 5 + 18, 10 + 18, 14 + 18 }))
        {
            programmedChord = "C#"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 3 + 18, 8 + 18, 10 + 18 }))
        {
            programmedChord = "C#m"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 10 + 18, 14 + 18, 21 + 18, 23 + 18 }))
        {
            programmedChord = "C#7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 8 + 18, 10 + 18, 21 + 18, 23 + 18 }))
        {
            programmedChord = "C#m7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 9 + 18, 11 + 18, 16 + 18 }))
        {
            programmedChord = "D"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 5 + 18, 9 + 18, 16 + 18 }))
        {
            programmedChord = "Dm"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 4 + 18, 9 + 18, 11 + 18 }))
        {
            programmedChord = "D7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 4 + 18, 5 + 18, 9 + 18 }))
        {
            programmedChord = "Dm7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 11 + 18, 15 + 18, 20 + 18, 22 + 18 }))
        {
            programmedChord = "Ebm"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 2 + 18, 10 + 18, 15 + 18, 17 + 18 }))
        {
            programmedChord = "Eb7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 2 + 18, 10 + 18, 11 + 18, 15 + 18 }))
        {
            programmedChord = "Ebm7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 3 + 18, 7 + 18, 8 + 18 }))
        {
            programmedChord = "E"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 7 + 18, 8 + 18 }))
        {
            programmedChord = "Em"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 3 + 18, 7 + 18, 8 + 18, 16 + 18 }))
        {
            programmedChord = "E7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 7 + 18 }))
        {
            programmedChord = "Em7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 0 + 18, 2 + 18, 4 + 18, 5 + 18, 9 + 18, 13 + 18 }))
        {
            programmedChord = "F7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 0 + 18, 2 + 18, 3 + 18, 4 + 18, 5 + 18, 13 + 18 }))
        {
            programmedChord = "Fm7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 6 + 18, 10 + 18, 11 + 18, 15 + 18, 19 + 18, 20 + 18 }))
        {
            programmedChord = "F#"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 6 + 18, 9 + 18, 10 + 18, 11 + 18, 19 + 18, 20 + 18 }))
        {
            programmedChord = "F#m"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 10 + 18, 15 + 18, 20 + 18 }))
        {
            programmedChord = "F#7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 8 + 18, 9 + 18, 10 + 18, 11 + 18 }))
        {
            programmedChord = "F#m7"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 7 + 18, 12 + 18, 17 + 18 }))
        {
            programmedChord = "G"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 0 + 18, 3 + 18, 4 + 18, 5 + 18, 13 + 18, 14 + 18 }))
        {
            programmedChord = "Gm"; programmedCapo = 3;
        }
        else if (IsCorrect(new int[] { 5 + 18, 7 + 18, 12 + 18 }))
        {
            programmedChord = "G7"; programmedCapo = 3;
        }


        else if (IsCorrect(new int[] { 0 + 30, 4 + 30, 5 + 30, 9 + 30, 13 + 30, 14 + 30 }))
        {
            programmedChord = "Ab"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 2 + 30, 3 + 30, 4 + 30, 11 + 30 }))
        {
            programmedChord = "Ab7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 2 + 30, 3 + 30, 11 + 30 }))
        {
            programmedChord = "Abm7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 8 + 30, 9 + 30, 10 + 30 }))
        {
            programmedChord = "A"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 4 + 30, 8 + 30, 9 + 30 }))
        {
            programmedChord = "Am"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 8 + 30, 9 + 30, 10 + 30, 17 + 30 }))
        {
            programmedChord = "A7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 4 + 30, 8 + 30, 9 + 30, 17 + 30 }))
        {
            programmedChord = "Am7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 1 + 30, 5 + 30, 14 + 30, 15 + 30, 16 + 30 }))
        {
            programmedChord = "Bb"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 1 + 30, 5 + 30, 10 + 30, 14 + 30, 15 + 30 }))
        {
            programmedChord = "Bbm"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 10 + 30, 14 + 18, 15 + 30, 23 + 30 }))
        {
            programmedChord = "Bbm7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 7 + 30, 11 + 30, 20 + 30, 21 + 30, 22 + 30 }))
        {
            programmedChord = "B"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 7 + 30, 11 + 30, 16 + 30, 20 + 30, 21 + 30 }))
        {
            programmedChord = "Bm"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 2 + 30, 7 + 30, 9 + 30, 11 + 30 }))
        {
            programmedChord = "B7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 1 + 30, 3 + 30, 5 + 30, 10 + 30, 14 + 30 }))
        {
            programmedChord = "Bm7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 4 + 30, 8 + 30, 12 + 30, 13 + 30 }))
        {
            programmedChord = "C"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 4 + 30, 8 + 30, 13 + 30, 15 + 30 }))
        {
            programmedChord = "C7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 2 + 30, 4 + 30, 15 + 30, 17 + 30 }))
        {
            programmedChord = "Cm7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 3 + 30, 5 + 30, 10 + 30, 14 + 30 }))
        {
            programmedChord = "C#"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 3 + 30, 8 + 30, 10 + 30 }))
        {
            programmedChord = "C#m"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 10 + 30, 14 + 30, 21 + 30, 23 + 30 }))
        {
            programmedChord = "C#7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 8 + 30, 10 + 30, 21 + 30, 23 + 30 }))
        {
            programmedChord = "C#m7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 9 + 30, 11 + 30, 16 + 30 }))
        {
            programmedChord = "D"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 5 + 30, 9 + 30, 16 + 30 }))
        {
            programmedChord = "Dm"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 4 + 30, 9 + 30, 11 + 30 }))
        {
            programmedChord = "D7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 4 + 30, 5 + 30, 9 + 30 }))
        {
            programmedChord = "Dm7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 11 + 30, 15 + 30, 20 + 30, 22 + 30 }))
        {
            programmedChord = "Ebm"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 2 + 30, 10 + 30, 15 + 30, 17 + 30 }))
        {
            programmedChord = "Eb7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 2 + 30, 10 + 30, 11 + 30, 15 + 30 }))
        {
            programmedChord = "Ebm7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 3 + 30, 7 + 30, 8 + 30 }))
        {
            programmedChord = "E"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 7 + 30, 8 + 30 }))
        {
            programmedChord = "Em"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 3 + 30, 7 + 30, 8 + 30, 16 + 30 }))
        {
            programmedChord = "E7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 7 + 30 }))
        {
            programmedChord = "Em7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 0 + 30, 2 + 30, 4 + 30, 5 + 30, 9 + 30, 13 + 30 }))
        {
            programmedChord = "F7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 0 + 30, 2 + 30, 3 + 30, 4 + 30, 5 + 30, 13 + 30 }))
        {
            programmedChord = "Fm7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 6 + 30, 10 + 30, 11 + 30, 15 + 30, 19 + 30, 20 + 30 }))
        {
            programmedChord = "F#"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 6 + 30, 9 + 30, 10 + 30, 11 + 30, 19 + 30, 20 + 30 }))
        {
            programmedChord = "F#m"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 10 + 30, 15 + 30, 20 + 30 }))
        {
            programmedChord = "F#7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 8 + 30, 9 + 30, 10 + 30, 11 + 30 }))
        {
            programmedChord = "F#m7"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 7 + 30, 12 + 30, 17 + 30 }))
        {
            programmedChord = "G"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 0 + 30, 3 + 30, 4 + 30, 5 + 30, 13 + 30, 14 + 30 }))
        {
            programmedChord = "Gm"; programmedCapo = 5;
        }
        else if (IsCorrect(new int[] { 5 + 30, 7 + 30, 12 + 30 }))
        {
            programmedChord = "G7"; programmedCapo = 5;
        }


        else if (IsCorrect(new int[] { 0 + 42, 4 + 42, 5 + 42, 9 + 42, 13 + 42, 14 + 42 }))
        {
            programmedChord = "Ab"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 2 + 42, 3 + 42, 4 + 42, 11 + 42 }))
        {
            programmedChord = "Ab7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 2 + 42, 3 + 42, 11 + 42 }))
        {
            programmedChord = "Abm7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 8 + 42, 9 + 42, 10 + 42 }))
        {
            programmedChord = "A"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 4 + 42, 8 + 42, 9 + 42 }))
        {
            programmedChord = "Am"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 8 + 42, 9 + 42, 10 + 42, 17 + 42 }))
        {
            programmedChord = "A7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 4 + 42, 8 + 42, 9 + 42, 17 + 42 }))
        {
            programmedChord = "Am7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 1 + 42, 5 + 42, 14 + 42, 15 + 42, 16 + 42 }))
        {
            programmedChord = "Bb"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 1 + 42, 5 + 42, 10 + 42, 14 + 42, 15 + 42 }))
        {
            programmedChord = "Bbm"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 10 + 42, 14 + 18, 15 + 42, 23 + 42 }))
        {
            programmedChord = "Bbm7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 7 + 42, 11 + 42, 20 + 42, 21 + 42, 22 + 42 }))
        {
            programmedChord = "B"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 7 + 42, 11 + 42, 16 + 42, 20 + 42, 21 + 42 }))
        {
            programmedChord = "Bm"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 2 + 42, 7 + 42, 9 + 42, 11 + 42 }))
        {
            programmedChord = "B7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 1 + 42, 3 + 42, 5 + 42, 10 + 42, 14 + 42 }))
        {
            programmedChord = "Bm7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 4 + 42, 8 + 42, 12 + 42, 13 + 42 }))
        {
            programmedChord = "C"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 4 + 42, 8 + 42, 13 + 42, 15 + 42 }))
        {
            programmedChord = "C7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 2 + 42, 4 + 42, 15 + 42, 17 + 42 }))
        {
            programmedChord = "Cm7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 3 + 42, 5 + 42, 10 + 42, 14 + 42 }))
        {
            programmedChord = "C#"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 3 + 42, 8 + 42, 10 + 42 }))
        {
            programmedChord = "C#m"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 10 + 42, 14 + 42, 21 + 42, 23 + 42 }))
        {
            programmedChord = "C#7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 8 + 42, 10 + 42, 21 + 42, 23 + 42 }))
        {
            programmedChord = "C#m7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 9 + 42, 11 + 42, 16 + 42 }))
        {
            programmedChord = "D"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 5 + 42, 9 + 42, 16 + 42 }))
        {
            programmedChord = "Dm"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 4 + 42, 9 + 42, 11 + 42 }))
        {
            programmedChord = "D7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 4 + 42, 5 + 42, 9 + 42 }))
        {
            programmedChord = "Dm7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 11 + 42, 15 + 42, 20 + 42, 22 + 42 }))
        {
            programmedChord = "Ebm"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 2 + 42, 10 + 42, 15 + 42, 17 + 42 }))
        {
            programmedChord = "Eb7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 2 + 42, 10 + 42, 11 + 42, 15 + 42 }))
        {
            programmedChord = "Ebm7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 3 + 42, 7 + 42, 8 + 42 }))
        {
            programmedChord = "E"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 7 + 42, 8 + 42 }))
        {
            programmedChord = "Em"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 3 + 42, 7 + 42, 8 + 42, 16 + 42 }))
        {
            programmedChord = "E7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 7 + 42 }))
        {
            programmedChord = "Em7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 0 + 42, 2 + 42, 4 + 42, 5 + 42, 9 + 42, 13 + 42 }))
        {
            programmedChord = "F7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 0 + 42, 2 + 42, 3 + 42, 4 + 42, 5 + 42, 13 + 42 }))
        {
            programmedChord = "Fm7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 6 + 42, 10 + 42, 11 + 42, 15 + 42, 19 + 42, 20 + 42 }))
        {
            programmedChord = "F#"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 6 + 42, 9 + 42, 10 + 42, 11 + 42, 19 + 42, 20 + 42 }))
        {
            programmedChord = "F#m"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 10 + 42, 15 + 42, 20 + 42 }))
        {
            programmedChord = "F#7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 8 + 42, 9 + 42, 10 + 42, 11 + 42 }))
        {
            programmedChord = "F#m7"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 7 + 42, 12 + 42, 17 + 42 }))
        {
            programmedChord = "G"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 0 + 42, 3 + 42, 4 + 42, 5 + 42, 13 + 42, 14 + 42 }))
        {
            programmedChord = "Gm"; programmedCapo = 7;
        }
        else if (IsCorrect(new int[] { 5 + 42, 7 + 42, 12 + 42 }))
        {
            programmedChord = "G7"; programmedCapo = 7;
        }

        else if (IsCorrect(new int[] { 0 + 54, 4 + 54, 5 + 54, 9 + 54, 13 + 54, 14 + 54 }))
        {
            programmedChord = "Ab"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 2 + 54, 3 + 54, 4 + 54, 11 + 54 }))
        {
            programmedChord = "Ab7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 2 + 54, 3 + 54, 11 + 54 }))
        {
            programmedChord = "Abm7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 8 + 54, 9 + 54, 10 + 54 }))
        {
            programmedChord = "A"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 4 + 54, 8 + 54, 9 + 54 }))
        {
            programmedChord = "Am"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 8 + 54, 9 + 54, 10 + 54, 17 + 54 }))
        {
            programmedChord = "A7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 4 + 54, 8 + 54, 9 + 54, 17 + 54 }))
        {
            programmedChord = "Am7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 1 + 54, 5 + 54, 14 + 54, 15 + 54, 16 + 54 }))
        {
            programmedChord = "Bb"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 1 + 54, 5 + 54, 10 + 54, 14 + 54, 15 + 54 }))
        {
            programmedChord = "Bbm"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 10 + 54, 14 + 18, 15 + 54, 23 + 54 }))
        {
            programmedChord = "Bbm7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 7 + 54, 11 + 54, 20 + 54, 21 + 54, 22 + 54 }))
        {
            programmedChord = "B"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 7 + 54, 11 + 54, 16 + 54, 20 + 54, 21 + 54 }))
        {
            programmedChord = "Bm"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 2 + 54, 7 + 54, 9 + 54, 11 + 54 }))
        {
            programmedChord = "B7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 1 + 54, 3 + 54, 5 + 54, 10 + 54, 14 + 54 }))
        {
            programmedChord = "Bm7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 4 + 54, 8 + 54, 12 + 54, 13 + 54 }))
        {
            programmedChord = "C"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 4 + 54, 8 + 54, 13 + 54, 15 + 54 }))
        {
            programmedChord = "C7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 2 + 54, 4 + 54, 15 + 54, 17 + 54 }))
        {
            programmedChord = "Cm7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 3 + 54, 5 + 54, 10 + 54, 14 + 54 }))
        {
            programmedChord = "C#"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 3 + 54, 8 + 54, 10 + 54 }))
        {
            programmedChord = "C#m"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 10 + 54, 14 + 54, 21 + 54, 23 + 54 }))
        {
            programmedChord = "C#7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 8 + 54, 10 + 54, 21 + 54, 23 + 54 }))
        {
            programmedChord = "C#m7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 9 + 54, 11 + 54, 16 + 54 }))
        {
            programmedChord = "D"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 5 + 54, 9 + 54, 16 + 54 }))
        {
            programmedChord = "Dm"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 4 + 54, 9 + 54, 11 + 54 }))
        {
            programmedChord = "D7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 4 + 54, 5 + 54, 9 + 54 }))
        {
            programmedChord = "Dm7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 11 + 54, 15 + 54, 20 + 54, 22 + 54 }))
        {
            programmedChord = "Ebm"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 2 + 54, 10 + 54, 15 + 54, 17 + 54 }))
        {
            programmedChord = "Eb7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 2 + 54, 10 + 54, 11 + 54, 15 + 54 }))
        {
            programmedChord = "Ebm7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 3 + 54, 7 + 54, 8 + 54 }))
        {
            programmedChord = "E"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 7 + 54, 8 + 54 }))
        {
            programmedChord = "Em"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 3 + 54, 7 + 54, 8 + 54, 16 + 54 }))
        {
            programmedChord = "E7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 7 + 54 }))
        {
            programmedChord = "Em7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 0 + 54, 2 + 54, 4 + 54, 5 + 54, 9 + 54, 13 + 54 }))
        {
            programmedChord = "F7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 0 + 54, 2 + 54, 3 + 54, 4 + 54, 5 + 54, 13 + 54 }))
        {
            programmedChord = "Fm7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 6 + 54, 10 + 54, 11 + 54, 15 + 54, 19 + 54, 20 + 54 }))
        {
            programmedChord = "F#"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 6 + 54, 9 + 54, 10 + 54, 11 + 54, 19 + 54, 20 + 54 }))
        {
            programmedChord = "F#m"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 10 + 54, 15 + 54, 20 + 54 }))
        {
            programmedChord = "F#7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 8 + 54, 9 + 54, 10 + 54, 11 + 54 }))
        {
            programmedChord = "F#m7"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 7 + 54, 12 + 54, 17 + 54 }))
        {
            programmedChord = "G"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 0 + 54, 3 + 54, 4 + 54, 5 + 54, 13 + 54, 14 + 54 }))
        {
            programmedChord = "Gm"; programmedCapo = 9;
        }
        else if (IsCorrect(new int[] { 5 + 54, 7 + 54, 12 + 54 }))
        {
            programmedChord = "G7"; programmedCapo = 9;
        }

        else
        {
            programmedChord = "Invalid chord"; programmedCapo = 0;
        }
    }

    void Awake()
    {
        moduleId = moduleIdCounter++;
        foreach (Fret fret in frets)
        {
            Fret trueFret = fret;
            fret.Selectable.OnInteract += delegate () { fretSelect(trueFret); return false; };
        }
        playBut.OnInteract += delegate () { OnPlayBut(); return false; };
    }

    void Start()
    {
        foreach (Renderer indicator in levelIndicators)
        {
            indicator.material = indicatorOptions[0];
        }
        level2Chord.text = "";
        level3Chord.text = "";
        StartCoroutine(level1Select());
    }

    void fretSelect(Fret fret)
    {
        fret.Selectable.AddInteractionPunch(.25f);
        if (fret.Renderer.sharedMaterial == unselected)
        {
            fret.Renderer.sharedMaterial = selected;
            fret.Status = true;
            stringIndex = UnityEngine.Random.Range(0, 12);
            Audio.PlaySoundAtTransform(strings[stringIndex].name, transform);
        }
        else if (fret.Renderer.sharedMaterial == selected)
        {
            fret.Renderer.sharedMaterial = unselected;
            fret.Status = false;
        }
    }

    void OnPlayBut()
    {
        playBut.AddInteractionPunch();
        if (stage > 3)
        {
            return;
        }
        if (stage == 1 && programmedChord == level1Target && programmedCapo == capoTarget)
        {
            stage++;
            StartCoroutine(level2Select());
            levelIndicators[0].material = indicatorOptions[2];
            Audio.PlaySoundAtTransform(chords[0].name, transform);
            Debug.LogFormat("[Guitar Chords #{0}] You played {1} in capo position {2}. That is correct.", moduleId, programmedChord, programmedCapo);
        }
        else if (stage == 2 && programmedChord == level2Target && programmedCapo == capoTarget)
        {
            stage++;
            StartCoroutine(level3Select());
            levelIndicators[1].material = indicatorOptions[2];
            Audio.PlaySoundAtTransform(chords[1].name, transform);
            Debug.LogFormat("[Guitar Chords #{0}] You played {1} in capo position {2}. That is correct.", moduleId, programmedChord, programmedCapo);
        }
        else if (stage == 3 && programmedChord == level3Target && programmedCapo == capoTarget)
        {
            stage++;
            levelIndicators[2].material = indicatorOptions[2];
            Audio.PlaySoundAtTransform(chords[2].name, transform);
            GetComponent<KMBombModule>().HandlePass();
            Debug.LogFormat("[Guitar Chords #{0}] You played {1} in capo position {2}. That is correct. Module disarmed.", moduleId, programmedChord, programmedCapo);
        }
        else if (stage == 1)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Audio.PlaySoundAtTransform(chords[3].name, transform);
            Debug.LogFormat("[Guitar Chords #{0}] Strike! You played {1} in capo position {2}. I was expecting {3} in capo position {4}.", moduleId, programmedChord, programmedCapo, level1Target, capoTarget);
            stage = 1;
            Start();
        }
        else if (stage == 2)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Audio.PlaySoundAtTransform(chords[3].name, transform);
            Debug.LogFormat("[Guitar Chords #{0}] Strike! You played {1} in capo position {2}. I was expecting {3} in capo position {4}.", moduleId, programmedChord, programmedCapo, level2Target, capoTarget);
            stage = 1;
            Start();
        }
        else if (stage == 3)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Audio.PlaySoundAtTransform(chords[3].name, transform);
            Debug.LogFormat("[Guitar Chords #{0}] Strike! You played {1} in capo position {2}. I was expecting {3} in capo position {4}.", moduleId, programmedChord, programmedCapo, level3Target, capoTarget);
            stage = 1;
            Start();
        }
    }

    IEnumerator level1Select()
    {
        cycle = 0;
        while (cycle < 20)
        {
            int index = UnityEngine.Random.Range(0, 41);
            yield return new WaitForSeconds(0.1f);
            Audio.PlaySoundAtTransform(chords[4].name, transform);
            level1Chord.text = chordOptions[index];
            cycle++;
            if (cycle == 20)
            {
                levelIndicators[0].material = indicatorOptions[1];
                level1Target = level1Chord.text;
            }
        }
        cycle = 0;

        if (Bomb.IsIndicatorOn("BOB"))
        {
            capoTarget = 9;
        }
        else if (Bomb.GetPortCount(Port.Parallel) >= 1 && Bomb.GetPortCount(Port.RJ45) >= 1)
        {
            capoTarget = 7;
        }
        else if (Bomb.GetBatteryCount() < 3)
        {
            capoTarget = 5;
        }
        else if (Bomb.GetSerialNumberNumbers().Last() % 2 == 1)
        {
            capoTarget = 3;
        }
        else
        {
            capoTarget = 0;
        }
        Debug.LogFormat("[Guitar Chords #{0}] The level 1 chord is {1} in capo position {2}.", moduleId, level1Target, capoTarget);
    }

    IEnumerator level2Select()
    {
        cycle = 0;
        while (cycle < 20 || level2Chord.text == level1Target)
        {
            int index = UnityEngine.Random.Range(0, 41);
            yield return new WaitForSeconds(0.1f);
            Audio.PlaySoundAtTransform(chords[4].name, transform);
            level2Chord.text = chordOptions[index];
            cycle++;
            if (cycle > 19 && level2Chord.text != level1Target)
            {
                levelIndicators[1].material = indicatorOptions[1];
                level2Target = level2Chord.text;
            }
        }
        cycle = 0;

        if (Bomb.GetPortCount(Port.PS2) >= 1 || Bomb.GetPortCount(Port.Serial) >= 1)
        {
            capoTarget = 5;
        }
        else if (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U'))
        {
            capoTarget = 0;
        }
        else if (Bomb.GetBatteryCount() > 5)
        {
            capoTarget = 9;
        }
        else if (Bomb.IsIndicatorOff("SIG"))
        {
            capoTarget = 7;
        }
        else
        {
            capoTarget = 3;
        }
        Debug.LogFormat("[Guitar Chords #{0}] The level 2 chord is {1} in capo position {2}.", moduleId, level2Target, capoTarget);
    }

    IEnumerator level3Select()
    {
        cycle = 0;
        while (cycle < 20 || level3Chord.text == level1Target || level3Chord.text == level2Target)
        {
            int index = UnityEngine.Random.Range(0, 41);
            yield return new WaitForSeconds(0.1f);
            Audio.PlaySoundAtTransform(chords[4].name, transform);
            level3Chord.text = chordOptions[index];
            cycle++;
            if (cycle > 19 && level3Chord.text != level1Target && level3Chord.text != level2Target)
            {
                levelIndicators[2].material = indicatorOptions[1];
                level3Target = level3Chord.text;
            }
        }
        cycle = 0;

        if (Bomb.GetBatteryCount() < 1)
        {
            capoTarget = 3;
        }
        else if (Bomb.GetSerialNumberNumbers().Sum() < 10)
        {
            capoTarget = 5;
        }
        else if (Bomb.IsIndicatorOn("FRQ") || Bomb.IsIndicatorOff("FRQ"))
        {
            capoTarget = 7;
        }
        else if (Bomb.GetPortCount(Port.StereoRCA) >= 1 || Bomb.GetPortCount(Port.DVI) >= 1)
        {
            capoTarget = 0;
        }
        else
        {
            capoTarget = 9;
        }
        Debug.LogFormat("[Guitar Chords #{0}] The level 3 chord is {1} in capo position {2}.", moduleId, level3Target, capoTarget);
    }

    private string TwitchHelpMessage = "Submit a chord using '!{0} play 4,3,-,4,3,3'. You may also toggle all strings using '!{0} toggle -,0,0,0,-,1'. Each string requires an input, but you may skip strings by inputting a '-' or space in its slot.";

    private IEnumerator ProcessTwitchCommand(string twitchCommand)
    {
        var command = twitchCommand.ToLowerInvariant();
        var match = Regex.Match(command, "^(?:play|submit|toggle) ([- 0-9]+|),([- 0-9]+|),([- 0-9]+|),([- 0-9]+|),([- 0-9]+|),([- 0-9]+|)$");
        if (!match.Success) yield break;
        List<KMSelectable> selectables = new List<KMSelectable>();
        while (frets.Select(x => x.Status).Contains(true))
        {
            yield return null;
            yield return new KMSelectable[] { frets.First(x => x.Status).Selectable };
        }
        Debug.LogFormat("Test");
        for (int i = 1; i < match.Groups.Count; i++)
        {
            var fret = match.Groups[i];
            int result = 0;
            if (!int.TryParse(fret.Value, out result)) continue;
            else if (result < 0 || result > 12)
            {
                yield return "sendtochaterror Selected capo is invalid. Please select a capo between 0 and 12.";
                yield break;
            }
            selectables.Add(frets[(6 - i) + result * 6].Selectable);
        }
        yield return null;
        var play = match.Groups[0].Value;
        if (play.StartsWith("play") || play.StartsWith("submit")) yield return selectables.Concat(new[] { playBut }).ToArray();
        else yield return selectables.ToArray();
    }
}
