using System;
using System.Collections;

public interface Fader
{
    bool IsFadedOut { get; }

    void SetAlpha(float alpha);

    void Show();

    void Hide();

    void FadeIn();

    void FadeOut();

}