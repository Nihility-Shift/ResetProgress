using CG.Client.Player.Input;
using UnityEngine;
using VoidManager.CustomGUI;

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
            GUILayout.Label("Reset Profile Menu");
            GUILayout.Label("There will be no going back.");
            if (GUILayout.Button("Reset Progress?"))
            {
                FirstAsk = true;
            }

            if (FirstAsk && GUILayout.Button("Are you sure?"))
            {
                SecondAsk = true;
            }

            if (SecondAsk && GUILayout.Button("Are you REALLY sure?!?"))
            {
                ThirdAsk = true;
            }

            if (ThirdAsk && GUILayout.Button("This is the final button. There is no going back."))
            {
                DebugInput.CloudProfileReset();
            }
        }
    }
}
