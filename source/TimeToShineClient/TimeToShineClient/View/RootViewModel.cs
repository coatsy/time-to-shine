using XamlingCore.UWP.Contract;
using XamlingCore.UWP.Navigation.MasterDetail;

namespace TimeToShineClient.View
{
    public class RootViewModel : XUWPMasterDetailViewModel
    {
        public RootViewModel(IUWPViewResolver viewResolver) : base(viewResolver)
        {
        }

        public override void OnInitialise()
        {
            AddPackage<HomeViewModel>();

            SetMaster(CreateContentModel<MenuMasterViewModel>());

            Build();

            base.OnInitialise();
        }
    }
}
