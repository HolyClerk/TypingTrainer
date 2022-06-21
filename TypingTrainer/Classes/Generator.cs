using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingTrainer.Classes;

internal sealed class Generator
{
    private static Difficulty s_DefaultDif = Difficulty.Normal;

    private Text _bufferText;

    public Generator() { }

    public Generator(Difficulty difficulty) => s_DefaultDif = difficulty;

    private async void CreateRandomTextAsync(Difficulty difficulty)
    {
        await Task.Run(() =>
        {
            var random = new Random();
            var buffer = "Какой-то текст для проверки.";

            // Логика рандомизации.

            _bufferText = new Text()
            {
                Value = buffer,
                Diff = difficulty,
                Length = buffer.Length
            };
        });
    }

    public Text GenerateTextAsync()
    {
        CreateRandomTextAsync(s_DefaultDif);

        return _bufferText;
    }

    public Text GenerateTextAsync(Difficulty difficulty)
    {
        CreateRandomTextAsync(difficulty);

        return _bufferText;
    }
}

