using System.Collections;
using System;
using UnityEngine;

namespace Mod
{
    public class Mod : MonoBehaviour
    {
        public static string ModTag = " [Liquid Dynamics]";

        public static void Main()
        {
            ModAPI.RegisterCategory("Liquid Dynamics", "", ModAPI.LoadSprite("Sprites/Category.png"));
            ModAPI.RegisterLiquid(HeatLiquid.ID, new HeatLiquid());
            ModAPI.Register(
            new Modification()
            {
                OriginalItem = ModAPI.FindSpawnable("Flask"),
                NameOverride = "Heat" + ModTag,
                DescriptionOverride = "It just heats up",
                CategoryOverride = ModAPI.FindCategory("Liquid Dynamics"),
                ThumbnailOverride = ModAPI.LoadSprite("sprites/gasoline-view.png"),
                AfterSpawn = (Instance) =>
                {
                    BloodContainer.SerialisableDistribution StartLiquid = new BloodContainer.SerialisableDistribution();
                    StartLiquid.LiquidID = HeatLiquid.ID;
                    StartLiquid.Amount = 2.5f;
                    Instance.GetComponent<FlaskBehaviour>().StartLiquid =StartLiquid;
                }
            });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Empty Syringe"),
                    NameOverride = "Heat Syringe" + ModTag,
                    DescriptionOverride = "It just heats up",
                    CategoryOverride = ModAPI.FindCategory("Liquid Dynamics"),
                    //ThumbnailOverride = ModAPI.LoadSprite("sprites/gasoline-view.png"),
                    AfterSpawn = (Instance) =>
                    {
                        UnityEngine.Object.Destroy(Instance.GetComponent<SyringeBehaviour>());
                        Instance.GetOrAddComponent<HeatSyringe>();
                    }
                });
        }
    }
    public class HeatSyringe : SyringeBehaviour
    {
        public override string GetLiquidID() => HeatLiquid.ID;
    }
    public class HeatLiquid : Liquid
    {
        public const string ID = "HEAT LIQUID";
        public override string GetDisplayName() => "Heat Liquid";

        public HeatLiquid()
        {
            Color = new UnityEngine.Color(227, 102, 0);
        }

        public override void OnEnterContainer(BloodContainer container)
        {

        }
        public override void OnEnterLimb(LimbBehaviour limb)
        {
        }
        public override void OnUpdate(BloodContainer c)
        {
            ModAPI.Notify(1);
            c.GetComponent<PhysicalBehaviour>().Temperature+=90;
        }

        public override void OnExitContainer(BloodContainer container)
        {

        }
    }
}