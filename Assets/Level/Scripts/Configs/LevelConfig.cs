using System;

[Serializable]
public class LevelConfig
{
    // is the initial level start animation enabled ?
    public bool startAnimationEnabled = true;

    // use desktop controls or mobile controls ?
    public bool desktopControls = false;

    public ColorsConfig colors;
}
