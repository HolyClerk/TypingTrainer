using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingTrainer.Classes;

internal sealed class GeneratedText
{
    private static string s_textsPath = @"C:\Users\PHPpr\Documents\Dev\TypingTrainer\TypingTrainer\Samples\";

    private static Difficulty s_DefaultDif = Difficulty.Normal;

    private Text _savedText;

    public GeneratedText() { }

    public GeneratedText(Difficulty difficulty)
    {
        s_DefaultDif = difficulty;
    }

    private Text GenerateText(Difficulty difficulty)
    {
        var randNum = new Random().Next(0, 2);
        
        var bufferText = File.ReadAllText($"{s_textsPath}text_{randNum}.txt");

        var text = new Text()
        {
            Diff = difficulty,
            Value = bufferText,
            Length = bufferText.Length
        };

        _savedText = text;

        return text;
    }

    public Text GetNewGeneratedText()
    {
        return GenerateText(s_DefaultDif);
    }

    public Text GetNewGeneratedText(Difficulty difficulty)
    {
        return GenerateText(difficulty);
    }

    public Text GetActualText()
    {
        return _savedText;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(_savedText.Value))
        {
            _savedText = GenerateText(s_DefaultDif);
        }

        return _savedText.Value;
    }
}