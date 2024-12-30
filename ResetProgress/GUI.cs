using CG.Client.Player.Input;
using CG.Profile;
using UnityEngine;
using VoidManager.CustomGUI;
using static UnityEngine.GUILayout;

namespace ResetProgress
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name()
        {
            return MyPluginInfo.USERS_PLUGIN_NAME;
        }

        public override void OnOpen()
        {
            FirstAsk = false;
            SecondAsk = false;
            ThirdAsk = false;
        }

        bool FirstAsk;
        bool SecondAsk;
        bool ThirdAsk;

        public override void Draw()
        {
            Label("Reset xp Menu");
            BeginHorizontal();
            Label($"Current xp: {PlayerProfile.Instance.Profile.Xp}");
            if (Button("Reset")) PlayerProfile.Instance.Profile.AddXp((long)Mathf.Max(0, (0 - PlayerProfile.Instance.Profile.Xp)));
            EndHorizontal();
            HorizontalSlider(0, 100, 100);
            
            Label("Reset Profile Menu");
            Label("Levels, cosmetics, and achievements will all be reset. There will be no going back.");
            if (Button("Reset Account?"))
            {
                FirstAsk = true;
            }

            if (FirstAsk && Button("Are you sure?"))
            {
                SecondAsk = true;
            }

            if (SecondAsk && Button("Are you REALLY sure?!?"))
            {
                ThirdAsk = true;
            }

            if (ThirdAsk && Button("This is the final button. There is no going back."))
            {
                DebugInput.CloudProfileReset();
                DebugInput.AchievementsReset();
            }
        }
    }
}
