using System.Collections.ObjectModel;
using TimeToShineClient.Model.Messages;
using XamlingCore.Portable.Messages.XamlingMessenger;
using XamlingCore.Portable.View.ViewModel;
using XamlingCore.Portable.View.ViewModel.Base;

namespace TimeToShineClient.View.Menu
{
    public class MenuMasterViewModel : DisplayListViewModel<MenuOptionViewModel, XViewModel>
    {
        public override void OnActivated()
        {
            _refresh();
            base.OnActivated();
        }

        public void ResetClick()
        {
            new ResetMessage().Send();
        }

        void _refresh()
        {
            //if (DataList == null)
            //{
            //    return;
            //}

            //Items = new ObservableCollection<MenuOptionViewModel>();

            //foreach (var item in DataList)
            //{
            //    var i = CreateContentModel<MenuOptionViewModel>();

            //    i.Item = item;
            //    i.Title = item.Title;

            //    Items.Add(i);
            //}

            //UpdateItemCount();
        }
    }
}
