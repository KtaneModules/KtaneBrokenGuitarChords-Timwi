using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

using Rnd = UnityEngine.Random;

public class BrokenGuitarChordsModule : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMBombModule Module;
    public KMAudio Audio;
    public KMSelectable[] FretSelectables;
    public KMSelectable[] MuteSelectables;
    public MeshRenderer[] FretRenderers;
    public MeshRenderer[] MuteRenderers;
    public KMSelectable PlayButton;
    public AudioClip[] Strings;
    public TextMesh ChordDisplay;

    private bool[] _fretStatus;
    private bool[] _muteStatus;
    private static int _moduleIdCounter = 1;
    private int _moduleId;
    private int _rootNote;
    private ChordQuality _chordQuality;
    private bool _isSolved;
    private int _brokenString;

    sealed class ChordQuality
    {
        public string Name { get; private set; }
        public int[] Semitones { get; private set; }
        public ChordQuality(string name, params int[] semitones)
        {
            Name = name;
            Semitones = semitones;
        }
    }

    private static readonly string[][] _noteNames = new[] { new[] { "C" }, new[] { "C#", "Db" }, new[] { "D" }, new[] { "D#", "Eb" }, new[] { "E" }, new[] { "F" }, new[] { "F#", "Gb" }, new[] { "G" }, new[] { "G#", "Ab" }, new[] { "A" }, new[] { "A#", "Bb" }, new[] { "B" } };
    private static readonly ChordQuality[] _chordQualities = new[] {
        new ChordQuality("", 0, 4, 7),
        new ChordQuality("m", 0, 3, 7),
        new ChordQuality("6", 0, 4, 7, 9),
        new ChordQuality("7", 0, 4, 7, 10),
        new ChordQuality("9", 0, 2, 4, 10),
        new ChordQuality("add9", 0, 2, 4, 7),
        new ChordQuality("m6", 0, 3, 7, 9),
        new ChordQuality("m7", 0, 3, 7, 10),
        new ChordQuality("maj7", 0, 4, 7, 11),
        new ChordQuality("dim", 0, 3, 6),
        new ChordQuality("dim7", 0, 3, 6, 9),
        new ChordQuality("+", 0, 4, 8),
        new ChordQuality("sus", 0, 5, 7)
    };
    private static readonly int[] _stringNotes = new[] { 4, 9, 2, 7, 11, 4 };
    private static readonly string[] _stringNames = new[] { "bass E", "A", "D", "G", "B", "treble E" };

    void Start()
    {
        _moduleId = _moduleIdCounter++;
        _isSolved = false;

        var sb = new StringBuilder();
        for (int i = 0; i < _chordQualities.Length; i++)
            sb.AppendFormat("<tr><th>C{0}</th><td>{1}</td></tr>\n", _chordQualities[i].Name, _chordQualities[i].Semitones.Select(sm => _noteNames[sm][0]).Join(", "));
        Debug.Log(sb.ToString());

        for (int i = 0; i < FretSelectables.Length; i++)
            FretSelectables[i].OnInteract = selectFret(i);
        for (int i = 0; i < MuteSelectables.Length; i++)
            MuteSelectables[i].OnInteract = selectMute(i);
        PlayButton.OnInteract = submit;

        _fretStatus = new bool[FretSelectables.Length];
        _muteStatus = new bool[MuteSelectables.Length];

        _brokenString = Rnd.Range(0, 6);

        _rootNote = Rnd.Range(0, _noteNames.Length);
        var rootNames = _noteNames[_rootNote];
        var rootName = rootNames[Rnd.Range(0, rootNames.Length)];
        _chordQuality = _chordQualities[Rnd.Range(0, _chordQualities.Length)];
        ChordDisplay.text = rootName + _chordQuality.Name;
        Debug.LogFormat(@"[Broken Guitar Chords #{0}] Please play me a {1} chord.", _moduleId, ChordDisplay.text);
        Debug.LogFormat(@"[Broken Guitar Chords #{0}] I expect these notes: {1}", _moduleId, _chordQuality.Semitones.Select(sm => (_rootNote + sm) % 12).OrderBy(n => n).Select(n => _noteNames[n][0]).Join(", "));
        Debug.LogFormat(@"[Broken Guitar Chords #{0}] The {1} string is broken.", _moduleId, _stringNames[_brokenString]);
    }

    KMSelectable.OnInteractHandler selectFret(int index)
    {
        return delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, FretSelectables[index].transform);
            FretSelectables[index].AddInteractionPunch(.25f);
            if (_isSolved || index % 6 == _brokenString)
                return false;
            _fretStatus[index] = !_fretStatus[index];
            FretRenderers[index].enabled = _fretStatus[index];
            if (_fretStatus[index])
                Audio.PlaySoundAtTransform(Strings[Rnd.Range(0, 12)].name, transform);
            return false;
        };
    }

    KMSelectable.OnInteractHandler selectMute(int index)
    {
        return delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, MuteSelectables[index].transform);
            MuteSelectables[index].AddInteractionPunch(.25f);
            if (_isSolved || index == _brokenString)
                return false;
            _muteStatus[index] = !_muteStatus[index];
            MuteRenderers[index].enabled = _muteStatus[index];
            return false;
        };
    }

    bool submit()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        PlayButton.AddInteractionPunch();
        if (_isSolved)
            return false;

        var notesPlayed = new HashSet<int>();
        for (int strng = 0; strng < 6; strng++)
        {
            if (strng == _brokenString)
                continue;
            var selectedFrets = Enumerable.Range(0, 13).Where(fret => _fretStatus[6 * fret + strng]).ToArray();
            if (selectedFrets.Length > 1)
            {
                Debug.LogFormat(@"[Broken Guitar Chords #{0}] You selected more than one fret on the {1} string. Strike.", _moduleId, _stringNames[strng]);
                Audio.PlaySoundAtTransform("wrong", transform);
                Module.HandleStrike();
                return false;
            }
            if (selectedFrets.Length > 0 && _muteStatus[strng])
            {
                Debug.LogFormat(@"[Broken Guitar Chords #{0}] You selected a fret on the {1} string while also muting that string. Strike.", _moduleId, _stringNames[strng]);
                Audio.PlaySoundAtTransform("wrong", transform);
                Module.HandleStrike();
                return false;
            }

            if (_muteStatus[strng])
                continue;

            var notePlayed = (_stringNotes[strng] + (selectedFrets.Length == 0 ? 0 : selectedFrets[0] + 1)) % 12;
            if (!_chordQuality.Semitones.Any(sm => (_rootNote + sm) % 12 == notePlayed))
            {
                Debug.LogFormat(@"[Broken Guitar Chords #{0}] On the {1} string, you selected {2}, which makes it a {3}, which is not part of a {4} chord. Strike.", _moduleId, _stringNames[strng], selectedFrets.Length == 0 ? "no fret" : "fret " + (selectedFrets[0] + 1), _noteNames[notePlayed][0], ChordDisplay.text);
                Audio.PlaySoundAtTransform("wrong", transform);
                Module.HandleStrike();
                return false;
            }
            notesPlayed.Add(notePlayed);
        }

        Debug.LogFormat(@"[Broken Guitar Chords #{0}] You played these notes: {1}", _moduleId, notesPlayed.OrderBy(n => n).Select(n => _noteNames[n][0]).Join(", "));

        for (int i = 0; i < _chordQuality.Semitones.Length; i++)
        {
            var expectedNote = (_rootNote + _chordQuality.Semitones[i]) % 12;
            if (!notesPlayed.Contains(expectedNote))
            {
                Debug.LogFormat(@"[Broken Guitar Chords #{0}] The {1} chord requires a {2}, which you didn’t play. Strike.", _moduleId, ChordDisplay.text, _noteNames[expectedNote][0]);
                Audio.PlaySoundAtTransform("wrong", transform);
                Module.HandleStrike();
                return false;
            }
        }

        Debug.LogFormat(@"[Broken Guitar Chords #{0}] Beautiful.", _moduleId);
        Audio.PlaySoundAtTransform("chord" + Rnd.Range(1, 4), transform);
        Module.HandlePass();
        _isSolved = true;
        return false;
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} mute [mutes all strings and resets all frets] | !{0} play x 0 0 1 0 2 [play a chord; x means muted; 0 means no fret; otherwise, frets are numbered from 1]";
#pragma warning restore 414

    private IEnumerable<KMSelectable> ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*(?:mute|reset)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            return Enumerable.Range(0, 6).Where(ix => !_muteStatus[ix]).Select(ix => MuteSelectables[ix]).Concat(Enumerable.Range(0, FretSelectables.Length).Where(ix => _fretStatus[ix]).Select(ix => FretSelectables[ix]));

        var m = Regex.Match(command, @"^\s*(?:play|submit)\s*([x,\d\s]+?)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!m.Success)
            return null;

        var instructions = m.Groups[1].Value.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (instructions.Length != 6)
            return null;

        var kmsel = new List<KMSelectable>();
        kmsel.AddRange(Enumerable.Range(0, 6).Where(ix => _muteStatus[ix]).Select(ix => MuteSelectables[ix]));
        kmsel.AddRange(Enumerable.Range(0, FretSelectables.Length).Where(ix => _fretStatus[ix]).Select(ix => FretSelectables[ix]));
        for (int strng = 0; strng < 6; strng++)
        {
            int fret;
            if (instructions[strng].Equals("x", StringComparison.InvariantCultureIgnoreCase))
                kmsel.Add(MuteSelectables[strng]);
            else if (int.TryParse(instructions[strng], out fret) && fret >= 0 && fret <= 13)
            {
                if (fret != 0)
                    kmsel.Add(FretSelectables[(fret - 1) * 6 + strng]);
            }
            else
                return null;
        }
        kmsel.Add(PlayButton);
        return kmsel;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        for (var str = 0; str < 6; str++)
        {
            if (_muteStatus[str])
            {
                MuteSelectables[str].OnInteract();
                yield return new WaitForSeconds(.1f);
            }
        }

        for (var fret = 0; fret < _fretStatus.Length; fret++)
        {
            if (_fretStatus[fret])
            {
                FretSelectables[fret].OnInteract();
                yield return new WaitForSeconds(.1f);
            }
        }

        var notesNeeded = _chordQuality.Semitones.ToList();
        var curStr = 0;
        while (notesNeeded.Count > 0)
        {
            if (curStr == _brokenString)
                curStr++;
            var fret = (notesNeeded[0] + _rootNote - _stringNotes[curStr] + 12) % 12;
            Debug.LogFormat(@"String {0}, trying to play {1}, fret {2}", _stringNotes[curStr], notesNeeded[0], fret);
            if (fret != 0)
                FretSelectables[6 * (fret - 1) + curStr].OnInteract();
            yield return new WaitForSeconds(.1f);
            curStr++;
            notesNeeded.RemoveAt(0);
        }

        for (; curStr < 6; curStr++)
        {
            MuteSelectables[curStr].OnInteract();
            yield return new WaitForSeconds(.1f);
        }

        PlayButton.OnInteract();
        yield return new WaitForSeconds(.1f);
    }
}
