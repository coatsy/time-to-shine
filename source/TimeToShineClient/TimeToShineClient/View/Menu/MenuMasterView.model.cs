using System.Collections.ObjectModel;
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
