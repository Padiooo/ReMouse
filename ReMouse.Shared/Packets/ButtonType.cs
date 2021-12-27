using System.ComponentModel;

namespace ReMouse.Shared.Packets
{
    public enum ButtonType
    {
        // Fake ButtonBehaviour.DEFAULT
        [Description("Action 0")]
        Button_0 = 1,                   // Special button 0 behavior set by user
        [Description("Action 1")]
        Button_1,                       // Special button 1 behavior set by user
        [Description("Action 2")]
        Button_2,                       // Special button 2 behavior set by user
        [Description("Action 3")]
        Button_3,                       // Special button 3 behavior set by user
        [Description("Action 4")]
        Button_4,                       // Special button 4 behavior set by user
        [Description("Action 5")]
        Button_5,                       // Special button 5 behavior set by user
        [Description("Action 6")]
        Button_6,                       // Special button 6 behavior set by user
        [Description("Action 7")]
        Button_7,                       // Special button 7 behavior set by user
        [Description("Action 8")]
        Button_8,                       // Special button 8 behavior set by user
        [Description("Action 9")]
        Button_9,                       // Special button 9 behavior set by user

        [Description("Copy")]
        Copy,                           // CTRL + C
        [Description("Paste")]
        Paste,                          // CTRL + V

        Middle_Click,                   // Mouse wheel click
        Middle_Down,                    // Mouse wheel scroll down
        Middle_Up,                      // Mouse wheel scroll up

        // Real ButtonBehaviour.DEFAULT
        [Description("Sytem volum MUTE")]
        System_Volume_Mute = 173,       // System volume mute
        [Description("Sytem volum DOWN")]
        System_Volume_Down = 174,       // System volume down
        [Description("Sytem volum UP")]
        System_Volume_Up = 175,         // System volume up

        [Description("Media player forward")]
        Player_Control_Forward = 39,    // Media player control forward
        [Description("Media player backward")]
        Player_Control_Backward = 37,   // Media player control backward
        [Description("Media player play/pause")]
        Player_Control_PlayPause = 179, // Media player control play or pause
        [Description("Media player volume up")]
        Player_Control_Volume_Up = 38,  // Media player control volume up
        [Description("Media player volume down")]
        Player_Control_Volume_Down = 40,// Media player control volume down

        // SPECIALS
        [Description("Mouse move")]
        MoveMouse = 42,                 // Move mouse toward direction sent
        [Description("Mouse left")]
        Left,                           // Mouse left button (click or hold)
        [Description("Mouse Right")]
        Right,                          // Mouse right button (click)

        [Description("Keyboard input")]
        Keyboard_Input,                 // Keyboard input (letter or more)
    }
}
