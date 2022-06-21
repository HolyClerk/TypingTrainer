using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingTrainer.Classes;

internal sealed class GeneratedText
{
    private static Difficulty s_DefaultDif = Difficulty.Normal;

    private Text _savedText;

    public GeneratedText() { }

    public GeneratedText(Difficulty difficulty)
    {
        s_DefaultDif = difficulty;
    }

    private Text GenerateText(Difficulty difficulty)
    {
        var bufferText = "Современные компании часто делают ставку на комплексное продвижение: " +
        "размещают рекламу на офлайн и онлайн носителях, привлекают ТВ и радио, " +
        "публикуют посты в блоге и соцсетях. Это правильный и логичный подход, который однако требует больших финансовых вложений. " +
        "Стартапы с ограниченным бюджетом не способны на такой масштабный охват и зачастую сосредотачиваются только на интернет-продвижении. " +
        "На фоне многих вариантов рекламы они останавливаются именно на наполнении сайта полезным контентом.";

        var text = new Text()
        {
            Diff = difficulty,
            Value = bufferText,
            Length = bufferText.Length
        };

        _savedText = text;

        return text;
    }

    public Text GenerateNewText()
    {
        return GenerateText(s_DefaultDif);
    }

    public Text GenerateNewText(Difficulty difficulty)
    {
        return GenerateText(difficulty);
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