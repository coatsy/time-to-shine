using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToShineClient.View;
using TimeToShineClient.View.Home;
using XamlingCore.UWP.Glue;

namespace TimeToShineClient.Glue
{
    public class ProjectGlue : UWPGlue
    {
        public override void Init()
        {
            base.Init();

            XUWPCoreAutoRegistration.RegisterAssembly(Builder, typeof(MainHomeViewModel));
            Container = Builder.Build();
        }
    }
}
