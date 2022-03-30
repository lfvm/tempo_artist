using System.Collections.Generic;

public class Beatmap
{
    public string artist;
    public int difficulty;
    public int maxCombo;
    public int maxNotes;

    private List<Note> notes;
    public string title;
}