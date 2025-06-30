using CG.Client.Player.Input;
using CG.Cloud;
using CG.Profile;
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
            if (Button("Reset XP"))
            {
                if (PlayerProfile.Instance.Profile is CloudPlayerProfileDataSync cloudProfile && cloudProfile.source is PhotonPlayerDataSync photonProfile && photonProfile.source is PlayerProfileData baseProfile)
                {
                    baseProfile.Xp = 0;
                    cloudProfile.AddXp(0);
                }
                else
                {
                    BepinPlugin.log.LogError("Could not reset player XP");
                }
            }

            EndHorizontal();
            HorizontalSlider(0, 100, 100);


            Label("Reset Level Menu");
            BeginHorizontal();
            Label($"Current Level: {PlayerProfile.Instance.Profile.Rank} : {PlayerProfile.Instance.Profile.FavorRank}");
            if (Button("Reset Level"))
            {
                if (PlayerProfile.Instance.Profile is CloudPlayerProfileDataSync cloudProfile && cloudProfile.source is PhotonPlayerDataSync photonProfile && photonProfile.source is PlayerProfileData baseProfile)
                {
                    baseProfile.Rank = 0;
                    baseProfile.FavorRank = 0;
                    cloudProfile.AddXp(0);
                }
                else
                {
                    BepinPlugin.log.LogError("Could not reset player Level");
                }
            }

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
                if (PlayerProfile.Instance.Profile is CloudPlayerProfileDataSync cloudProfile && cloudProfile.source is PhotonPlayerDataSync photonProfile && photonProfile.source is PlayerProfileData baseProfile)
                {
                    baseProfile.Xp = 0;
                    baseProfile.Rank = 0;
                    baseProfile.FavorRank = 0;
                    cloudProfile.AddXp(0);
                }
                else
                {
                    BepinPlugin.log.LogError("Could not reset player Level/xp in total reset");
                }

                DebugInput.AchievementsReset();
                DebugInput.CloudProfileReset();
                OnOpen();
            }
        }
    }
}
